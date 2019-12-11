using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace UDPClient
{
    class ClientReceiver
    {
        static void Main(string[] args)
        {
            //clasess
            MyHttpClient myHttpClient = new MyHttpClient();
            MotionsModel motionsModel = new MotionsModel();
            //UDP Set Up
            UdpClient udpReceiver = new UdpClient(7000);
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            //Rest API URI
           // string MotionWebApi = "http://motioninmotions.azurewebsites.net/";
          
       
           
            //// Blocks until a message returns on this socket from a remote host.
            Console.WriteLine("Receiver is blocked");


            try
            {
                while (true)
                {
                    Byte[] receiveBytes = udpReceiver.Receive(ref RemoteIpEndPoint);
                    Console.WriteLine(" Its Getting This      1");

                    string receivedData = Encoding.ASCII.GetString(receiveBytes);

                    //Here we are separating the different values                    
                    string[] motionData = receivedData.Split(' ');
                    //This is going to delete the first one - whoch is just a word we dont need
                    motionData = motionData.Where(i => i != motionData[0]).ToArray();
                    motionsModel.Roll = Convert.ToDouble(motionData[1]);
                    motionsModel.Yaw = Convert.ToDouble(motionData[2]);
                    motionsModel.Pitch = Convert.ToDouble(motionData[3]);
                    motionsModel.MyDataTime = DateTime.Now;
                    motionsModel.DeviceId = Convert.ToInt32(motionData[6]);

                    //Console.WriteLine();
                    //Console.WriteLine();
                    Console.WriteLine(" Its Getting This  *************************************************************");
                    //using (HttpClient client = new HttpClient())
                    //    {
                    //        string stringmomtion = JsonConvert.SerializeObject(motionsModel);
                    //        var content = new StringContent(stringmomtion, Encoding.UTF8, "application/json");
                    //        client.BaseAddress = new Uri(MotionWebApi);
                    //        client.DefaultRequestHeaders.Clear();
                    //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //        var response = client.PostAsync("/api/motion", content).Result;

                    //    }


                    Console.WriteLine("Sender: " + receivedData.ToString());
                    Console.WriteLine("This message was sent from " +
                                                RemoteIpEndPoint.Address.ToString() +
                                                " on their port number " +
                                                RemoteIpEndPoint.Port.ToString());
                    //myHttpClient.PostItemHttpTask(MmotionsModel)
                    var task = myHttpClient.PostItemHttpTask(motionsModel);
                    //THIS IS JUST FOR TESTING REMOVE THE THREAD BEFORE SUBMITING
                    Thread.Sleep(5000);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
