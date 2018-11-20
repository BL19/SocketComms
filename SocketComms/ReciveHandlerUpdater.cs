using Newtonsoft.Json;
using SocketComms.Handlers;
using SocketComms.Packets;
using System;
using System.Text;

namespace SocketComms
{
	internal class ReciveHandlerUpdater
	{
		private Connection c;

		public ReciveHandlerUpdater(Connection connection)
		{
			c = connection;
		}

		public void Run()
		{
			while (true)
			{
				byte[] array = new byte[c.bufferSize];
				c.GetClient().GetStream().Read(array, 0, c.bufferSize);
				string @string = Encoding.UTF8.GetString(array);
				object packet = null;
				string[] array2 = new string[20];
				Packet packet2 = JsonConvert.DeserializeObject<Packet>(@string);
				if (packet2.type.Equals(PacketType.Text))
				{
					packet = packet2.content;
				}
				else if (packet2.type.Equals(PacketType.Class))
				{
					string text = string.Concat(packet2.content);
					ClassPacket classPacket = JsonConvert.DeserializeObject<ClassPacket>(text);
					Type type = Type.GetType(classPacket.classLocation);
					array2[0] = classPacket.classLocation;
					packet = JsonConvert.SerializeObject(classPacket.classObject);
				}
				else if (!packet2.type.Equals(PacketType.File) && !packet2.type.Equals(PacketType.Image) && packet2.type.Equals(PacketType.Custom))
				{
					packet = packet2.content;
				}
				IHandler[] handlers = c.GetHandlers();
				PacketType[] handlersType = c.GetHandlersType();
				for (int i = 0; i < c.GetHandlersCount(); i++)
				{
					if (handlersType[i].Equals(packet2.type))
					{
						handlers[i].Handle(c, packet, array2);
					}
				}
			}
		}
	}
}
