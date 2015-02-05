using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlasterComms
{
    class RestComms : CommsBase, IBlasterComms
    {
        public RestComms(RestCommsConfig config)
        {

        }

        public void Open()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public void MoveTo(int x, int y, bool isRelative)
        {
            throw new NotImplementedException();
        }

        public void Fire()
        {
            throw new NotImplementedException();
        }

        public event StatusDelegate Status;

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
