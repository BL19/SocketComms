namespace SocketComms.Handlers
{
	public interface IHandler
	{
		void Handle(Connection c, object packet, string[] args);
	}
}
