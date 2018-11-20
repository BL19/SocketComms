using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SocketComms
{
	public class Sockets
	{
		public static List<Connection> connections = new List<Connection>();

		public static Connection GetConnecion(string ip, int port)
		{
			TcpClient c = new TcpClient(ip, port);
			Connection connection = new Connection(c);
			connections.Add(connection);
			return connection;
		}

		public static TcpListener StartListener(ListenerOptions opt)
		{
			TcpListener tcpListener = new TcpListener(IPAddress.Any, opt.port);
			tcpListener.Start();
			Thread thread = new Thread(new ListenerThread(tcpListener, opt).Run);
			thread.Start();
			return tcpListener;
		}

		public static List<Connection> GetConnections()
		{
			return connections;
		}

		public static void DropConnections()
		{
			foreach (Connection connection in connections)
			{
				connection.Disconnect();
			}
		}
	}
}
