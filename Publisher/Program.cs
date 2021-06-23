using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Azure.Messaging.WebPubSub;
using Publisher;
using Websocket.Client;

await SendWithWebSocketAsync();

async Task SendWithSdkAsync()
{
    var serviceClient = new WebPubSubServiceClient(Settings.ConnectionString, Settings.HubName);

    do
    {
        Console.Write("\nWrite the message to send: ");
        var message = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(message))
        {
            break;
        }

        await serviceClient.SendToAllAsync(message);

    } while (true);
}

async Task SendWithWebSocketAsync()
{
    var serviceClient = new WebPubSubServiceClient(Settings.ConnectionString, Settings.HubName);
    var url = serviceClient.GetClientAccessUri(roles: new string[] { "webpubsub.sendToGroup", "webpubsub.joinLeaveGroup" });

    using var client = new WebsocketClient(url, () =>
    {
        var inner = new ClientWebSocket();
        inner.Options.AddSubProtocol("json.webpubsub.azure.v1");
        return inner;
    });

    await client.Start();

    do
    {
        Console.Write("\nWrite the message to send: ");
        var message = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(message))
        {
            break;
        }

        var request = WebSocketUtils.CreateSendToGroupMessage(message, null);
        await client.SendInstant(request);

    } while (true);
}