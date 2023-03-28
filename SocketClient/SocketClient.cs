using System.Net.Sockets;
using System.Net;
using System.Text;


namespace Sockets
{
    public class SocketClient
    {
        
        public static void StartClient() 
        {
            try
            {
                byte[] buffer = new byte[1024];

                IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
                IPAddress ipAdress = ipHostInfo.AddressList[0];
                IPEndPoint ipEndPoint = new IPEndPoint(ipAdress, 11000);

                Socket client = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                client.Connect(ipEndPoint);

                while(true)
                {
                    Console.WriteLine($"Socket connected to {client.RemoteEndPoint.ToString()}");

                    byte[] encodedMessage = Encoding.ASCII.GetBytes("Hi! I'm the client<|EOM|>");

                    _ = client.Send(encodedMessage);

                    // int receivedBytes = await client.ReceiveAsync(buffer, SocketFlags.None);
                    // string decodedMessage = Encoding.ASCII.GetString(buffer, 0, receivedBytes);
                    // Console.WriteLine($"Message received from server: {decodedMessage}");
                    break;
                }

                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
        
    }
}