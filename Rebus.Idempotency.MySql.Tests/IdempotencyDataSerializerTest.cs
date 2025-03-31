using Xunit;

namespace Rebus.Idempotency.MySql.Tests
{
    public class IdempotencyDataSerializerTest
    {
        [Fact]
        public void Given_IdempotencyDataJson_Then_SerializesCorrectly()
        {
            // Arrange
            var data = "{\"OutgoingMessages\":[{\"MessageId\":\"0004a81a-355d-4ffc-a99d-1efbfe77dd23\",\"MessagesToSend\":[{\"DestinationAddresses\":[\"ecsv2_development_webhookcenterapi\"],\"TransportMessage\":{\"Headers\":{\"rbs2-intent\":\"pub\",\"rbs2-msg-id\":\"d4309fdb-a45c-457e-9143-6bf2b4651048\",\"rbs2-return-address\":\"ecsv2_development_webhookcenterapi\",\"rbs2-senttime\":\"2025-03-06T05:55:03.5702729+00:00\",\"rbs2-sender-address\":\"ecsv2_development_webhookcenterapi\",\"rbs2-msg-type\":\"Webhooks.Messaging.Messages.TriggerNotificationCommandMessage, Webhooks\",\"rbs2-corr-id\":\"0004a81a-355d-4ffc-a99d-1efbfe77dd23\",\"rbs2-corr-seq\":\"1\",\"rbs2-content-type\":\"application/json;charset=utf-8\"},\"Body\":\"eyIkdHlwZSI6IldlYmhvb2tzLk1lc3NhZ2luZy5NZXNzYWdlcy5UcmlnZ2VyTm90aWZpY2F0aW9uQ29tbWFuZE1lc3NhZ2UsIFdlYmhvb2tzIiwiV2ViaG9va0lkIjoiMTJlNDhjNjQtMjZhNS00ZjI0LWJhN2MtNDBkZTQ1MTM0NzAyIiwiRXZlbnROYW1lIjoiT1JERVJfQ09ORklSTUVEIiwiTWVzc2FnZSI6eyIkdHlwZSI6IlNhZy5OZXh0R2VuLlBpZ2Vvbi5NZXNzYWdlcy5FdmVudHMuT3JkZXJDZW50ZXIuVjUuT3JkZXJDb25maXJtZWRFdmVudCwgU2FnLk5leHRHZW4uUGlnZW9uLk1lc3NhZ2VzIiwiT3JkZXJJZCI6ODc3OTEsIlNlbmRFbWFpbCI6dHJ1ZSwiU2FsZXNQb2ludElkIjoxMTIyLCJPcmRlck51bWJlciI6Ikk1OUtBS0pNQVhWWEIiLCJDb3JyZWxhdGlvbklkIjoiZDQ3NDg3Y2YtMDVjOS00ZjJiLTg0NjktODNjMzMzZGNlNmMxIiwiVGVuYW50SWQiOjk1Nn0sIlRlbmFudElkIjo5NTZ9\"}}]}],\"HandledMessageIds\":{\"0004a81a-355d-4ffc-a99d-1efbfe77dd23\":true}}";
            
            // Act
            var actual = IdempotencyDataSerializer.DeserializeData(data);

            // Assert
            var actualJson = IdempotencyDataSerializer.SerializeData(actual);
            Assert.Equal(data, actualJson);
        }
    }
}