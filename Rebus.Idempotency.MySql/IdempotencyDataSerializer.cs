using Newtonsoft.Json;

namespace Rebus.Idempotency.MySql
{
    public class IdempotencyDataSerializer
    {
        public static IdempotencyData DeserializeData(string data)
        {
            if (data == null) return null;
            return JsonConvert.DeserializeObject<IdempotencyData>(data);
        }

        public static string SerializeData(IdempotencyData data)
        {
            if (data == null) return null;
            return JsonConvert.SerializeObject(data);
        }
    }
}
