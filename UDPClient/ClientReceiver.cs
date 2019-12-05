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
        static void MainX(string[] args)
        {
            //Creates a UdpClient for reading incoming data.
            
            //UdpClient udpReceiver = new UdpClient(0); used if you dont know the port
            UdpClient udpReceiver = new UdpClient(7000);

            //Creates an IPEndPoint to record the IP Address and port number of the sender.
            //This IPEndPoint will allow you to read datagrams sent from a specific ip-source on port 7000
            // 192.168.3.224/127.0.0.1 
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            //IPEndPoint RemoteIpEndPoint = new IPEndPoint(ip, 7000);


            //BROADCASTING RECEIVER
            //This IPEndPoint will allow you to read datagrams sent from any ip-source on port 7000
            //IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 7000);
            string MotionWebApi = "http://motioninmotions.azurewebsites.net/";
            //IPEndPoint object will allow us to read datagrams sent from any source.
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);
            MotionsModel motionsModel = new MotionsModel();
            MyHttpClient httpClient = new MyHttpClient();
            // Blocks until a message returns on this socket from a remote host.
            Console.WriteLine("Receiver is blocked");
            try
            {
                while (true)
                {
                    Byte[] receiveBytes = udpReceiver.Receive(ref RemoteIpEndPoint);

                    string receivedData = Encoding.ASCII.GetString(receiveBytes);

                    Console.WriteLine("Sender: " + receivedData.ToString());
                    Console.WriteLine("This message was sent from " +
                                                RemoteIpEndPoint.Address.ToString() +
                                                " on their port number " +
                                                RemoteIpEndPoint.Port.ToString());
                    string[] motionData = receivedData.Split(' ');
                    if (motionData != null)

                    {
                        motionData = motionData.Where(i => i != motionData[0]).ToArray();
                        motionsModel.Roll = Convert.ToDouble(motionData[1]);
                        motionsModel.Yaw = Convert.ToDouble(motionData[2]);
                        motionsModel.Pitch = Convert.ToDouble(motionData[3]);
                        motionsModel.MyDataTime = Convert.ToDateTime(motionData[4]);
                        motionsModel.DeviceId = Convert.ToInt32(motionData[5]);
                        using (HttpClient client = new HttpClient())
                        {
                            string stringmomtion = JsonConvert.SerializeObject(motionsModel);
                            var content = new StringContent(stringmomtion, Encoding.UTF8, "application/json");
                            client.BaseAddress = new Uri(MotionWebApi);
                            client.DefaultRequestHeaders.Clear();
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var response = client.PostAsync("/api/motion", content).Result;

                        }
                        Thread.Sleep(1);

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
