using Newtonsoft.Json;
using SocketComms.Handlers;
using SocketComms.Packets;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketComms
{
	public class Connection
	{
		protected TcpClient c;

		private IHandler[] handlers = new IHandler[200];

		private PacketType[] handlersQual;

		private int currH = 0;

		public int bufferSize = 1048576;

		public Connection(TcpClient c)
		{
			this.c = c;
			handlersQual = new PacketType[handlers.Length];
			Thread thread = new Thread(new ReciveHandlerUpdater(this).Run);
			thread.Start();
		}

		private void SendPacket(string packet)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(packet);
			c.GetStream().Write(bytes, 0, bytes.Length);
		}

		private void SendPacket(Packet p)
		{
			SendPacket(JsonConvert.SerializeObject((object)p));
		}

		public void SendCustomPacket(CustomPacket packet)
		{
			ClassPacket classPacket = new ClassPacket();
			classPacket.classLocation = packet.GetType().FullName;
			classPacket.classObject = packet;
			Packet p = new Packet(PacketType.Custom, classPacket);
			SendPacket(p);
		}

		public void SendText(string text)
		{
			SendPacket(new Packet(PacketType.Text, text));
		}

		public void SendClass(object c)
		{
			ClassPacket classPacket = new ClassPacket();
			classPacket.classLocation = c.GetType().FullName;
			classPacket.classObject = c;
			SendPacket(new Packet(PacketType.Class, classPacket));
		}

		public void AddHandler(IHandler handler, PacketType type)
		{
			handlers[currH] = handler;
			handlersQual[currH] = type;
			currH++;
		}

		public void Disconnect()
		{
			c.Dispose();
		}

		public void Drop()
		{
			Disconnect();
		}

		public TcpClient GetClient()
		{
			return c;
		}

		public IHandler[] GetHandlers()
		{
			return handlers;
		}

		public PacketType[] GetHandlersType()
		{
			return handlersQual;
		}

		public int GetHandlersCount()
		{
			return currH;
		}
	}
}
