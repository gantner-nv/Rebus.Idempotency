using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Rebus.Messages;

namespace Rebus.Idempotency
{
    /// <summary>
    /// This chunk of data help with tracking handled messages and externally visible behavior (i.e. outbound messages) from handling each message
    /// </summary>
    public class IdempotencyData
    {
        private List<OutgoingMessages> _outgoingMessages = new();
        private ConcurrentDictionary<string, bool> _handledMessageIds = new();

        /// <summary>
        /// Gets the outgoing messages
        /// </summary>
        public List<OutgoingMessages> OutgoingMessages
        {
            get { return _outgoingMessages; }
        }

        /// <summary>
        /// Getst the IDs of all messages that have been handled
        /// </summary>
        public ConcurrentDictionary<string, bool> HandledMessageIds
        {
            get { return _handledMessageIds; }
        }

        /// <summary>
        /// Gets whether the message with the given ID has already been handled
        /// </summary>
        public bool HasAlreadyHandled(MessageId messageId)
        {
            return _handledMessageIds.ContainsKey(messageId);
        }

        /// <summary>
        /// Gets the outgoing messages for the incoming message with the given ID
        /// </summary>
        public IEnumerable<OutgoingMessage> GetOutgoingMessages(MessageId messageId)
        {
            var outgoingMessages = _outgoingMessages.FirstOrDefault(o => o.MessageId == messageId);

            return outgoingMessages != null
                ? outgoingMessages.MessagesToSend
                : Enumerable.Empty<OutgoingMessage>();
        }

        /// <summary>
        /// Marks the message with the given ID as handled
        /// </summary>
        public void MarkMessageAsHandled(MessageId messageId)
        {
            _handledMessageIds.TryAdd(messageId, true);
        }

        /// <summary>
        /// Adds the <see cref="TransportMessage"/> as an outgoing message destined for the addresses specified by <paramref name="destinationAddresses"/>
        /// under the given <paramref name="messageId"/>
        /// </summary>
        public void AddOutgoingMessage(MessageId messageId, IEnumerable<string> destinationAddresses, TransportMessage transportMessage)
        {
            var outgoingMessage = new OutgoingMessage(destinationAddresses, transportMessage);

            GetOrCreate(messageId).Add(outgoingMessage);
        }

        OutgoingMessages GetOrCreate(MessageId messageId)
        {
            _handledMessageIds.TryAdd(messageId, true);

            var outgoingMessages = _outgoingMessages.FirstOrDefault(o => o.MessageId == messageId);

            if (outgoingMessages != null) return outgoingMessages;

            outgoingMessages = new OutgoingMessages(messageId, new List<OutgoingMessage>());
            _outgoingMessages.Add(outgoingMessages);

            return outgoingMessages;
        }

        [Obsolete("added for fallback support only, will be removed after prod update")]
        public void SetOutgoingMessage(List<OutgoingMessages> outgoingMessages)
        {
            _outgoingMessages = outgoingMessages;
        }
        
        [Obsolete("added for fallback support only, will be removed after prod update")]
        public void SetHandledMessageIds(ConcurrentDictionary<string, bool> handledMessageIds)
        {
            _handledMessageIds = handledMessageIds;
        }
    }
}
