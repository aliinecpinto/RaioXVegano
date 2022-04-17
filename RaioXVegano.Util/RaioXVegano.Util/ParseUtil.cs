using Newtonsoft.Json;

namespace RaioXVegano.Util
{
    public static class ParseUtil
    {
        public static string ParseJson(object t) 
        {
            return JsonConvert.SerializeObject(t);
        }

        public static T ParseString<T>(string json) 
        {
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
