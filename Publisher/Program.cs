using System;
using Azure.Messaging.WebPubSub;
using Publisher;

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
    //await serviceClient.SendToAllAsync(RequestContent.Create(new { Message = message }));

} while (true);