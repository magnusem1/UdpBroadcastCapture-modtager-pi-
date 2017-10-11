using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpBroadcastCapture
{
    class Program
    {
        // https://msdn.microsoft.com/en-us/library/tst0kwb1(v=vs.110).aspx
        // IMPORTANT Windows firewall must be open on UDP port 7000
        // Use the network EGV5-DMU2 to capture from the local IoT devices
        private const int Port = 7000;
        //private static readonly IPAddress IpAddress = IPAddress.Parse("192.168.5.137"); 
        private static readonly IPAddress IpAddress = IPAddress.Any;
        // Listen for activity on all network interfaces
        // https://msdn.microsoft.com/en-us/library/system.net.ipaddress.ipv6any.aspx


        static void Main()
        {
            using (var service = new Broadcast.Service1Client())
            {
                service.PostTempToList(ref)
            }
        }

        // To parse data from the IoT devices in the teachers room, Elisagårdsvej
        private static void Parse(string response)
        {
            string[] parts = response.Split(' ');
            foreach (string part in parts)
            {
                Console.WriteLine(part);
            }
            string temperatureLine = parts[6];
            string temperatureStr = temperatureLine.Substring(temperatureLine.IndexOf(": ") + 2);
            Console.WriteLine(temperatureStr);
        }







        public string GetBroadcast()
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IpAddress, 9998);

            using (UdpClient socket = new UdpClient(Port))
            {
            
                byte[] datagramReceived = socket.Receive(ref remoteEndPoint);

                string message = Encoding.ASCII.GetString(datagramReceived, 0, datagramReceived.Length);
                return message;
            }
        }
    }
}
