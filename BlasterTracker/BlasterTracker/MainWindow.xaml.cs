using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BlasterTracker
{
    public enum XGunState
    {
        Idle = 0,
        Left,
        Right
    }

    public enum YGunState
    {
        Idle = 0,
        Down,
        Up
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Initially look for an orange range
        readonly double INITIAL_MIN_HUE = 0;
        readonly double INITIAL_MIN_SAT = 132;
        readonly double INITIAL_MIN_VAL = 177;

        readonly double INITIAL_MAX_HUE = 199;
        readonly double INITIAL_MAX_SAT = 210;
        readonly double INITIAL_MAX_VAL = 255;

        readonly double MINIMUM_BLOB_SIZE_IN_PX = 700;

        readonly double MOVEMENT_THRESHOLD_PERCENTAGE = 0.15;


        Dictionary<YGunState, string> _yGunStateCommands = new Dictionary<YGunState, string>
        {
            {YGunState.Up, "h" },
            {YGunState.Down, "g"},
            {YGunState.Idle, "i" }
        };

        Dictionary<XGunState, string> _xGunStateCommands = new Dictionary<XGunState, string>
        {
            {XGunState.Left, "l" },
            {XGunState.Right, "m" },
            {XGunState.Idle, "n" }
        };


        // Camera feed
        private Capture _capture = null;
        // Indicates if capture is in progress
        private bool _captureInProgress;

        // Test Mode indicator (Show gray image)
        private bool _test = false;

        // If we fail to connect to serial continue
        private bool _serialConnected = false;

        // Keep track of last changed min/max HSV values (for access off UI thread)
        double _maxHue, _maxSat, _maxVal;
        double _minHue, _minSat, _minVal;

        XGunState XGunState { get; set; }
        YGunState YGunState { get; set; }

        XGunState PrevXGunState { get; set; }

        YGunState PrevYGunState { get; set; }

        SerialPort SerialPort { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            _capture = new Capture(0);
            _capture.ImageGrabbed += ProcessFrame;

            if(!TrySerialConnect())
            {
                MessageBox.Show("Failed to connect to serial");
            }
        }

        private bool TrySerialConnect()
        {
            try
            {
                SerialPort = new SerialPort("COM4", 115200);
                SerialPort.Open();
                _serialConnected = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }
            return _serialConnected;
        }

        private void ProcessFrame(object sender, EventArgs arg)
        {
            Image<Bgr, Byte> frame = _capture.RetrieveBgrFrame();

            var minHsv = new Hsv(_minHue, _minSat, _minVal);
            var maxHsv = new Hsv(_maxHue, _maxSat, _maxVal);


            Image<Gray, Byte> grayFrame = frame.Convert<Hsv, Byte>().InRange(minHsv, maxHsv);
            Image<Gray, Byte> smallGrayFrame = grayFrame.PyrDown();
            Image<Gray, Byte> smoothedGrayFrame = smallGrayFrame.PyrUp();
            Image<Gray, Byte> cannyFrame = smoothedGrayFrame.Canny(100, 60);

            // This takes our nice looking input and blows or cuts off values 
            // above or below a bightness threshold 
            Image<Gray, byte> webcamThreshImg = smoothedGrayFrame.ThresholdBinary(new Gray(150), new Gray(255));

            // The magic blob detection code 
            Emgu.CV.Cvb.CvBlobs resultingWebcamBlobs = new Emgu.CV.Cvb.CvBlobs();
            Emgu.CV.Cvb.CvBlobDetector bDetect = new Emgu.CV.Cvb.CvBlobDetector();
            uint numWebcamBlobsFound = bDetect.Detect(webcamThreshImg, resultingWebcamBlobs);

            Emgu.CV.Cvb.CvBlob largestBlob = null;
            foreach (var blob in resultingWebcamBlobs)
            {
                if (blob.Value.Area > MINIMUM_BLOB_SIZE_IN_PX)
                {
                    if (largestBlob == null || (largestBlob.Area < blob.Value.Area))
                    {
                        largestBlob = blob.Value;
                    }
                }
            }

            if (largestBlob != null)
            {
                // Draw bounding box around target
                frame.Draw(largestBlob.BoundingBox, new Bgr(0, 255, 255), 5);

                System.Drawing.Point center = new System.Drawing.Point(frame.Width / 2, frame.Height / 2);

                double thresholdWidth = MOVEMENT_THRESHOLD_PERCENTAGE * frame.Width;
                double thresholdHeight = MOVEMENT_THRESHOLD_PERCENTAGE * frame.Height;

                if (largestBlob.Centroid.X < (center.X - thresholdWidth))
                {
                    XGunState = XGunState.Left;
                }
                else if (largestBlob.Centroid.X > (center.X + thresholdWidth))
                {
                    XGunState = XGunState.Right;
                }
                else
                {
                    XGunState = XGunState.Idle;
                }

                if (largestBlob.Centroid.Y > (center.Y + thresholdHeight))
                {
                    YGunState = YGunState.Down;
                }
                else if (largestBlob.Centroid.Y < (center.Y - thresholdHeight))
                {
                    YGunState = YGunState.Up;
                }
                else
                {
                    YGunState = YGunState.Idle;
                }

                if (YGunState != PrevYGunState && _serialConnected && !_manualMode)
                {
                    PrevYGunState = YGunState;
                    SerialPort.Write(_yGunStateCommands[YGunState]);
                }

                if (XGunState != PrevXGunState && _serialConnected && !_manualMode)
                {
                    PrevXGunState = XGunState;
                    SerialPort.Write(_xGunStateCommands[XGunState]);
                }

            }
            else if (_serialConnected)
            {
                PrevXGunState = XGunState = XGunState.Idle;
                PrevYGunState = YGunState = YGunState.Idle;
                SerialPort.Write(_xGunStateCommands[XGunState.Idle] + _yGunStateCommands[YGunState.Idle]);
            }

            // Camera frame callback is not on UI tread. Need to get there to set image source
            this.Dispatcher.InvokeAsync(() =>
            {
                commandLabel.Content = string.Format("XGunState: {0}, YGunState: {1}", XGunState, YGunState);

                if (!_test)
                {
                    image.Source = ToBitmapSource(frame);
                }
                else
                {
                    image.Source = ToBitmapSource(smoothedGrayFrame);
                }
            });
        }

        [DllImport("gdi32")]
        private static extern int DeleteObject(IntPtr o);

        private bool _manualMode = false;

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if(hsvGrid.Visibility == Visibility.Visible) {
                _manualMode = true;
                hsvGrid.Visibility = Visibility.Collapsed;
                moveGrid.Visibility = Visibility.Visible;
            } else
            {
                _manualMode = false;
                hsvGrid.Visibility = Visibility.Visible;
                moveGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void upButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(_manualMode && _serialConnected)
            {
                SerialPort.Write(_yGunStateCommands[YGunState.Up]);
            }
        }

        private void downButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_manualMode && _serialConnected)
            {
                SerialPort.Write(_yGunStateCommands[YGunState.Down]);
            }
        }

        private void rightButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_manualMode && _serialConnected)
            {
                SerialPort.Write(_xGunStateCommands[XGunState.Right]);
            }
        }

        private void leftButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_manualMode && _serialConnected)
            {
                SerialPort.Write(_xGunStateCommands[XGunState.Left]);
            }
        }

        private void stopButton_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_manualMode && _serialConnected)
            {
                SerialPort.Write(_xGunStateCommands[XGunState.Idle] + _yGunStateCommands[YGunState.Idle]);
            }
        }

        public static BitmapSource ToBitmapSource(IImage image)
        {
            using (System.Drawing.Bitmap source = image.Bitmap)
            {
                IntPtr ptr = source.GetHbitmap(); //obtain the Hbitmap

                BitmapSource bs = System.Windows.Interop
                  .Imaging.CreateBitmapSourceFromHBitmap(
                  ptr,
                  IntPtr.Zero,
                  Int32Rect.Empty,
                  System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());

                DeleteObject(ptr); //release the HBitmap
                return bs;
            }
        }

        private void onSliderChanged(object sender, RoutedEventArgs e)
        {
            if (hueMaxSlider != null && hueMinSlider != null && satMaxSlider != null && satMinSlider != null && valMaxSlider != null && valMinSlider != null)
            {
                _maxHue = hueMaxSlider.Value;
                _maxSat = satMaxSlider.Value;
                _maxVal = valMaxSlider.Value;

                _minHue = hueMinSlider.Value;
                _minSat = satMinSlider.Value;
                _minVal = valMinSlider.Value;

                if (labelmaxHue != null && labelminHue != null && labelmaxSat != null && labelminSat != null && labelmaxVal != null && labelminVal != null)
                {
                    var labelString = "Min Hue: " + _minHue;
                    labelminHue.Content = labelString;

                    labelString = "Max Hue: " + _maxHue;
                    labelmaxHue.Content = labelString;

                    labelString = "Min Sat: " + _minSat;
                    labelminSat.Content = labelString;

                    labelString = "Max Sat: " + _maxSat;
                    labelmaxSat.Content = labelString;

                    labelString = "Min Val: " + _minVal;
                    labelminVal.Content = labelString;

                    labelString = "Max Val: " + _maxVal;
                    labelmaxVal.Content = labelString;
                }
            }
        }

        private void startStopButton_Click(object sender, RoutedEventArgs e)
        {
            if (_captureInProgress)
            {
                _capture.Stop();
                startStopButton.Content = "Start";
                _captureInProgress = false;
            }
            else
            {
                _capture.Start();
                startStopButton.Content = "stop";
                _captureInProgress = true;
            }
        }

        private void switch_Click(object sender, RoutedEventArgs e)
        {
            if (_test)
            {
                _test = false;
            }
            else
            {
                _test = true;
            }
        }
    }
}
