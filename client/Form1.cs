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

            serialPort = new SerialPort("COM3", 115200);
            //serialPort.Open();

            /*
            if (!serialPort.IsOpen)
            {
               throw new ArgumentException(); 
            }*/
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

        private void fireButton_MouseDown(object sender, EventArgs e)
        {
            serialPort.Write("q");
            Thread.Sleep(500);
            serialPort.Write("d");
        }

        private void omgButton_MouseDown(object sender, EventArgs e)
        {
            serialPort.Write("d");
            serialPort.Write("i");
            serialPort.Write("n");
        }

        private void button_MouseUp(object sender, EventArgs e)
        {
            Console.Write("stop");
        }
    }
}
