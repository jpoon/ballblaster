using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlasterComms
{
    class SerialComms : CommsBase, IBlasterComms
    {
        SerialCommsConfig _config;
        SerialPort _port;
        Thread _readThread;
        bool _continue;

        struct ChannelCommands
        {
            public ChannelCommands(char positiveStart, char negativeStart, char stop)
            {
                PositiveStartCommand = positiveStart;
                NegativeStartCommand = negativeStart;
                StopCommand = stop;
            }

            public char PositiveStartCommand;
            public char NegativeStartCommand;
            public char StopCommand;
        }

        static readonly Dictionary<ControlChannels, ChannelCommands> _channelCommands = new Dictionary<ControlChannels, ChannelCommands>() {
            { ControlChannels.Yaw, new ChannelCommands('l', 'm', 'n' )},
            { ControlChannels.Pitch, new ChannelCommands('g', 'h', 'i' )},
            { ControlChannels.Reload, new ChannelCommands('a', 'b', 'c' )},
        };

        public SerialComms(SerialCommsConfig config)
        {
            _config = config;
            Open();
        }

        public void Open()
        {
            _continue = true;
            _port = new SerialPort();
            _port.PortName = _config.PortName;
            _port.BaudRate = 115200;
            _port.Parity = Parity.None;
            _port.DataBits = 8;
            _port.StopBits = StopBits.One;
            _port.Handshake = Handshake.None;
            _port.ReadTimeout = 250;
            _port.WriteTimeout = 20;
            _readThread = new Thread(ReadPort);
            _port.Open();
            _readThread.Start();
        }

        public void Close()
        {
            _continue = false;
            if (_port.IsOpen)
            {
                _readThread.Join();
                _port.Close();
            }
        }

        public void MoveTo(int x, int y, bool isRelative)
        {
            CalculateAndSendMoveControl(ControlChannels.Yaw, x, isRelative);
            CalculateAndSendMoveControl(ControlChannels.Pitch, y, isRelative);
        }

        void CalculateAndSendMoveControl(ControlChannels channel, int distance, bool isRelative)
        {
            if (distance != 0)
            {
                long duration = 0;
                bool positive = true;
                if (isRelative)
                {
                    duration = CalculateRunDuration(channel, Math.Abs(distance));
                    positive = distance > 0;
                }
                else
                {
                    System.Diagnostics.Debug.Assert(false, "Only relative positioning currently supported");
                }
                var commands = _channelCommands[channel];
                SendMoveControl(positive ? commands.PositiveStartCommand : commands.NegativeStartCommand, commands.StopCommand, duration);
            }
        }

        void SendMoveControl(char startControl, char stopControl, long runDuration)
        {
            Task.Run(() => _port.Write(startControl.ToString()))
                .ContinueWith(async delegate { await Task.Delay((int)runDuration); })
                .ContinueWith((antecedent) => _port.Write(stopControl.ToString()));
        }

        public void Fire()
        {
            _port.Write("q");
        }

        public event StatusDelegate Status;

        public void Dispose()
        {
            Close();
        }

        void ReadPort()
        {
            while (_continue)
            {
                try
                {
                    string message = _port.ReadLine();
                    if (this.Status != null)
                    {
                        this.Status(0, 0, false, false, false);
                    }
                }
                catch (TimeoutException) 
                { 
                }
            }
        }
    }
}
