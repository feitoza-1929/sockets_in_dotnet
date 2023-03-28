using System.Net.Sockets;
using System.Net;
using System.Text;


namespace Sockets
{
    public class SocketClient
    {
        
        public static void StartClient() 
        {
            byte[] buffer = new byte[1024];

            IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
            IPAddress ipAdress = ipHostInfo.AddressList[0];
            IPEndPoint ipEndPoint = new IPEndPoint(ipAdress, 11_000);

            Socket client = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            client.Connect(ipEndPoint);

            Console.WriteLine($"Socket connected to {client.RemoteEndPoint.ToString()}");

            byte[] encodedMessage = Encoding.ASCII.GetBytes("Hi! I'm the client");

            _ = client.Send(encodedMessage);

            int receivedBytes = client.Receive(buffer);
            string decodedMessage = Encoding.ASCII.GetString(buffer, 0, receivedBytes);
            Console.WriteLine($"Message received from server: {decodedMessage}");

            client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
        
    }
}