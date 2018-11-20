using Newtonsoft.Json;

namespace SocketComms.Handlers
{
	public class Utils
	{
		public static object DeserializeClass<T>(string json)
		{
			return JsonConvert.DeserializeObject<T>(json);
		}

		public static object DeserializeClass<T>(object packet)
		{
			return JsonConvert.DeserializeObject<T>(string.Concat(packet));
		}
	}
}
