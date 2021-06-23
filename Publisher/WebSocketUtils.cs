using System.Text.Json;
using System.Text.Json.Serialization;

namespace Publisher
{
    public static class WebSocketUtils
    {
        private static long ackId;

        private static readonly JsonSerializerOptions jsonSerializerOptions = new(JsonSerializerDefaults.Web)
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static string CreateSendToAllMessage(string message)
        {
            var content = new { Type = "sendToAll", DataType = "text", Data = message, AckId = ++ackId };
            var json = JsonSerializer.Serialize(content, jsonSerializerOptions);

            return json;
        }

        public static string CreateSendToGroupMessage(string message, string group)
        {
            var content = new { Type = "sendToGroup", DataType = "text", Group = group, Data = message, AckId = ++ackId };
            var json = JsonSerializer.Serialize(content, jsonSerializerOptions);

            return json;
        }
    }
}
