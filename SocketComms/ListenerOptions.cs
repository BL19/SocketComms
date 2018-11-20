using SocketComms.Handlers;
using SocketComms.Packets;

namespace SocketComms
{
	public class ListenerOptions
	{
		public IHandler[] handlers = new IHandler[200];

		public PacketType[] handlersQ = new PacketType[200];

		public int curr = 0;

		public int port;

		public void AddHandler(IHandler h, PacketType type)
		{
			handlers[curr] = h;
			handlersQ[curr] = type;
			curr++;
		}
	}
}
