using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UDPClient
{
    class MotionsModel
    {
      


      
            public int Id { get; set; }
            public double Pitch
            {
                get; set;
            }
            public double Roll
            {
                get; set;
            }
            public double Yaw { get; set; }
            public DateTime MyDataTime { get; set; }
            public int DeviceId { get; set; }

            public MotionsModel()
            {

            }
            public MotionsModel(int id, double roll, double yaw, double pitch, int devid, DateTime dateTime)
            {
                Id = id;
                Roll = roll;
                Yaw = yaw;
                Pitch = pitch;
                DeviceId = devid;
                MyDataTime = dateTime;
            }


        }
    }



