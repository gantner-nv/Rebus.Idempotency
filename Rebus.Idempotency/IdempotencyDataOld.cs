using System;
using System.Collections.Generic;

namespace Rebus.Idempotency
{
    [Obsolete("Will be removed after production deployment, added for fallback only")]
    public class IdempotencyDataOld
    {
        public List<OutgoingMessages> OutgoingMessages { get; set; }

        public HashSet<string> HandledMessageIds { get; set; }
    }
}