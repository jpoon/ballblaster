using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlasterComms
{
    class CommsBase
    {
        protected enum ControlChannels
        {
            Reload,
            Pitch,
            Yaw,
            Trigger,
        }

        const double StandardSpeed = 1.0;
        static readonly Dictionary<ControlChannels, double> _channelFactors = new Dictionary<ControlChannels, double> {
            { ControlChannels.Pitch, 1.0 },
            { ControlChannels.Yaw, 1.0 },
            { ControlChannels.Reload, 1.0 },
        };

        protected long CalculateRunDuration(ControlChannels channel, int distance)
        {
            return (long)(distance * StandardSpeed * _channelFactors[channel]);
        }

    }
}
