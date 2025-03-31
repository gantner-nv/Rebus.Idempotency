using System;
using System.Collections.Concurrent;
using Newtonsoft.Json;

namespace Rebus.Idempotency.MySql
{
    public class IdempotencyDataSerializer
    {
        public static IdempotencyData DeserializeData(string data)
        {
            if (data == null) return null;
            try
            {
                return JsonConvert.DeserializeObject<IdempotencyData>(data);
            }
            catch (JsonSerializationException e)
            {
                Console.WriteLine(e);

                // TODO: Remove this fallback (+ try/catch) once production updated
#pragma warning disable CS0618 // Type or member is obsolete
                var dataWithHashSet = JsonConvert.DeserializeObject<IdempotencyDataOld>(data);
                var  idempotencyData = new IdempotencyData();
                idempotencyData.SetOutgoingMessage(dataWithHashSet.OutgoingMessages);
                ConcurrentDictionary<string, bool> _handledMessageIds = new(); 
                foreach (var handledMessageId in dataWithHashSet.HandledMessageIds)
                {
                    _handledMessageIds.TryAdd(handledMessageId, true);
                }
                
                idempotencyData.SetHandledMessageIds(_handledMessageIds);
                return idempotencyData;
#pragma warning restore CS0618 // Type or member is obsolete
            }
        }

        public static string SerializeData(IdempotencyData data)
        {
            if (data == null) return null;
            return JsonConvert.SerializeObject(data);
        }
    }
}
