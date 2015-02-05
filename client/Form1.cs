namespace client
{
    using System;
    using System.IO.Ports;
    using System.Threading;
    using System.Windows.Forms;

    public partial class Form1 : Form
    {
        private SerialPort serialPort;

        public Form1()
        {
            InitializeComponent();

            this.leftButton.MouseDown += leftButton_MouseDown;
            this.rightButton.MouseDown += rightButton_MouseDown;
            this.upButton.MouseDown += upButton_MouseDown;
            this.downButton.MouseDown += downButton_MouseDown;

            this.leftButton.MouseUp += stopButton_MouseDown;
            this.rightButton.MouseUp += stopButton_MouseDown;
            this.upButton.MouseUp += stopButton_MouseDown;
            this.downButton.MouseUp += stopButton_MouseDown;

            serialPort = new SerialPort("COM3", 115200);
            serialPort.Open();

            if (!serialPort.IsOpen)
            {
               throw new ArgumentException(); 
            }
        }
        void stopButton_MouseDown(object sender, EventArgs e)
        {
            serialPort.Write("i");
            serialPort.Write("n");
        }

        private void leftButton_MouseDown(object sender, EventArgs e)
        {
            serialPort.Write("l");
        }

        private void rightButton_MouseDown(object sender, EventArgs e)
        {
            serialPort.Write("m");
        }

        private void upButton_MouseDown(object sender, EventArgs e)
        {
            serialPort.Write("h");
        }

        private void downButton_MouseDown(object sender, EventArgs e)
        {
            serialPort.Write("g");
        }

        private void fireButton_Click(object sender, EventArgs e)
        {
            serialPort.Write("q");
            Thread.Sleep(500);
            serialPort.Write("d");
        }

        private void omgButton_Click(object sender, EventArgs e)
        {
            serialPort.Write("d");
            serialPort.Write("i");
            serialPort.Write("n");
        }
    }
}
