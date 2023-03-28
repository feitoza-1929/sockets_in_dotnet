using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Sockets
{
    public class SocketServer
    {
        public static async void StartServer() 
        {
            try
            {
                IPHostEntry ipHostInfo = Dns.GetHostEntry("localhost");
                IPAddress ipAdress = ipHostInfo.AddressList[0];
                IPEndPoint ipEndPoint = new IPEndPoint(ipAdress, 11000);

                Socket server = new(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                server.Bind(ipEndPoint);
                server.Listen(10);

                Console.WriteLine("Waiting for connection...");
                Socket handler = server.Accept();

                while (true)
                {
                    byte[] buffer = new byte[1024];
                    int receive = handler.Receive(buffer, SocketFlags.None);
                    string decodedMessage = Encoding.ASCII.GetString(buffer, 0, receive);

                    if (decodedMessage.IndexOf("<|EOM|>") > -1)
                    {
                        
                        Console.WriteLine($"Received message {decodedMessage}");
                        break;
                    }
                }

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}