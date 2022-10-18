using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TcpClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Enter the server IP address");
            var ipAddressString = Console.ReadLine();
            IPAddress ipAddress;
            while (!IPAddress.TryParse(ipAddressString, out ipAddress))
            {
                Console.WriteLine("The IP address format is incorrect, enter the IP address.");
            }

            var ipEndPoint = new IPEndPoint(ipAddress, 5553);
            using Socket client = new(
                ipEndPoint.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            await client.ConnectAsync(ipEndPoint);
            // Set inactivity period for sending ACK message to 30 seconds and repeat interval if the ACK hasn't been echoed to 5 seconds.
            // Thus the time the connection is supposed to be kept alive for 30 + 10 * 5 = 45 seconds.
            // If the server drops the connection before that and the client won't keep it alive,
            // we will get SocketException: 'An existing connection was forcibly closed by the remote host.' on ReceiveAsync().
            socket_SetKeepAlive(client, true, 30_000, 5_000);
            while (true)
            {
                // Send message.
                var message = "Hi friends 👋!<|EOM|>";
                var messageBytes = Encoding.UTF8.GetBytes(message);
                _ = await client.SendAsync(messageBytes, SocketFlags.None);
                Console.WriteLine($"Socket client sent message: \"{message}\"");

                // Receive ack.
                var buffer = new byte[1_024];
                var received = await client.ReceiveAsync(buffer, SocketFlags.None);
                var response = Encoding.UTF8.GetString(buffer, 0, received);
                if (response == "<|ACK|>")
                {
                    Console.WriteLine(
                        $"Socket client received acknowledgment: \"{response}\"");
                    break;
                }
                // Sample output:
                //     Socket client sent message: "Hi friends 👋!<|EOM|>"
                //     Socket client received acknowledgment: "<|ACK|>"
            }

            client.Shutdown(SocketShutdown.Both);
        }

        private static void socket_SetKeepAlive(Socket socket, bool on, uint keepAliveTime, uint keepAliveInterval)
        {
            int size = Marshal.SizeOf(default(uint));

            var inOptionValues = new byte[size * 3];

            BitConverter.GetBytes((uint)(on ? 1 : 0)).CopyTo(inOptionValues, 0);
            BitConverter.GetBytes((uint)keepAliveTime).CopyTo(inOptionValues, size);
            BitConverter.GetBytes((uint)keepAliveInterval).CopyTo(inOptionValues, size * 2);

            socket.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
        }
    }
}
