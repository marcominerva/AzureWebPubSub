using System;
using System.Net.WebSockets;
using Azure.Messaging.WebPubSub;
using Subscriber;
using Websocket.Client;

var serviceClient = new WebPubSubServiceClient(Settings.ConnectionString, Settings.HubName);
var url = serviceClient.GetClientAccessUri();

//var url = new Uri("wss://socket.webpubsub.azure.com/client/hubs/Chat?access_token=eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJ3c3M6Ly9zb2NrZXQud2VicHVic3ViLmF6dXJlLmNvbS9jbGllbnQvaHVicy9DaGF0IiwiaWF0IjoxNjI0NDU1Nzg1LCJleHAiOjE2MjQ0NTkzODUsInJvbGUiOlsid2VicHVic3ViLnNlbmRUb0dyb3VwIiwid2VicHVic3ViLmpvaW5MZWF2ZUdyb3VwIl19.CQ6Qk3wrM0Yx0-lBJhEq-CKLFkdwTWnNej3ZDkltf_g");

using var client = new WebsocketClient(url, () =>
{
    var inner = new ClientWebSocket();
    inner.Options.AddSubProtocol("json.webpubsub.azure.v1");
    return inner;
});

client.MessageReceived.Subscribe(message => Console.WriteLine($"Message received: {message}\n"));

await client.Start();

Console.WriteLine("\nWaiting for messages... Press a key to exit\n");
Console.ReadKey();
