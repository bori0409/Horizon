using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HorizonUpNow.Model
{
    public class DeviceModel
    {
        public int DeviceId { get; set; }
        public string PersonName { get; set; }
        public DeviceModel()
        {
                
        }
        public DeviceModel(int deviceid, string name)
        {
            DeviceId = deviceid;
            PersonName = name;
        }
    }
}
