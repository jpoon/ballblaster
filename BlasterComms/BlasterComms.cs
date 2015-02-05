using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlasterComms
{
    public enum CommsTypes
    {
        Serial,
        Rest
    }

    public delegate void StatusDelegate(int x, int y, bool firing, bool limitX, bool limitY);

    public interface IBlasterComms : IDisposable
    {
        void Open();
        void Close();
        void MoveTo(int x, int y, bool isRelative);
        void Fire();
        event StatusDelegate Status;
    }

    public class BlasterCommsFactory
    {
        public static IBlasterComms Create(CommsTypes commsType, CommsConfigBase config)
        {
            switch (commsType)
            {
                case CommsTypes.Serial:
                    return new SerialComms((SerialCommsConfig)config);

                case CommsTypes.Rest:
                    return new RestComms((RestCommsConfig)config);
            }
            return null;
        }
    }
}
