namespace SocketComms.Packets
{
	public class Packet
	{
		public PacketType type;

		public object content;

		public Packet()
		{
		}

		public Packet(PacketType type, object content)
		{
			this.type = type;
			this.content = content;
		}
	}
}
