using System;
using System.Net.WebSockets;
using Azure.Messaging.WebPubSub;
using Subscriber;
using Websocket.Client;

var serviceClient = new WebPubSubServiceClient(Settings.ConnectionString, Settings.HubName);
var url = serviceClient.GetClientAccessUri();

using var client = new WebsocketClient(url, () =>
{
    var inner = new ClientWebSocket();
    //inner.Options.AddSubProtocol("json.webpubsub.azure.v1");
    return inner;
});

client.MessageReceived.Subscribe(message => Console.WriteLine($"Message received: {message}\n"));
await client.Start();

Console.WriteLine("\nWaiting for messages... Press a key to exit\n");
Console.ReadKey();
