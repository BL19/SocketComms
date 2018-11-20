using System;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;

namespace SocketComms
{
	internal class ListenerThread
	{
		private TcpListener s;

		private ListenerOptions o;

		public ListenerThread(TcpListener s, ListenerOptions o)
		{
			this.s = s;
			this.o = o;
		}

		internal void Run()
		{
			while (true)
			{
                
				TcpClient c = s.AcceptTcpClient();
				Connection connection = new Connection(c);
				for (int i = 0; i < o.curr; i++)
				{
					connection.AddHandler(o.handlers[i], o.handlersQ[i]);
				}
				Sockets.connections.Add(connection);
                
			}
		}
	}
}
