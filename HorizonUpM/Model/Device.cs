using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonUpM.Model
{
    public class Device
    {
        public int DeviceId { get; set; }
        public string PersonName { get; set; }
        public Device()
        {

        }
        public Device(int deviceid, string name)
        {
            DeviceId = deviceid;
            PersonName = name;
        }
    }
}
