using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace UDPClient
{

    class UDPNumberReceiverAbroad

    {
        

        private const int Port = 4210;
        





        static void Main()
        {
            IPAddress ip = IPAddress.Parse("172.20.10.4");
            using (UdpClient socket = new UdpClient(new IPEndPoint(ip, Port)))
            {
                IPEndPoint remoteEndPoint = new IPEndPoint(ip, 0);



                while (true)
                {
                    Console.WriteLine("Waiting for broadcast {0}", socket.Client.LocalEndPoint);
                    byte[] datagramReceived = socket.Receive(ref remoteEndPoint);

                    string message = Encoding.ASCII.GetString(datagramReceived, 0, datagramReceived.Length);
                    Console.WriteLine("Receives {0} bytes from {1} port {2} message {3}", datagramReceived.Length,
                        remoteEndPoint.Address, remoteEndPoint.Port, message);


                    var calc = WindowFinder.FindWindow("Calculator");
                    WindowFinder.SetForegroundWindow(calc);
                   var number = float.Parse(message, CultureInfo.InvariantCulture.NumberFormat);
                    
                    if (number < -0.6)
                    {
                        Console.WriteLine("LEFT");
                       SendKeys.SendWait("1");
                    }
                    else if (number > 0.6)
                    {
                        Console.WriteLine("RIGHT");
                        //
                        SendKeys.SendWait("2");
                    }
                    else
                        {
                        Console.WriteLine("WAITINGG.........");
                        // SendKeys.SendWait("") 
                    }
                        /*
                        SendKeys.SendWait("111");
                        SendKeys.SendWait("*");
                        SendKeys.SendWait("11");
                        SendKeys.SendWait("=");
                        Console.WriteLine("Calculator done");
                        */

                    }
                }
        }


    }
    public class WindowFinder
    {
        
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);


        public static IntPtr FindWindow(string caption)
        {

            return FindWindow(null, caption);





        }




    }
}
