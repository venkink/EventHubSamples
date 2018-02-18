using Microsoft.Azure.EventHubs;
using System;
using System.Text;
using System.Threading.Tasks;

namespace EventHubSamples.Core
{
    class Program
    {
        private static EventHubClient eventHubClient;
        private const string EhConnectionString = "Endpoint=sb://hon-eventhub.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=A5cL9Qp1GdRYit/IG+Y/KAOZAtTo4JBV9JVDpegoQbw=";
        private const string EhEntityPath = "send";
        static void Main(string[] args)
        {
            MainAsync(args).GetAwaiter().GetResult();
        }
        private static async Task MainAsync(string[] args)
        {
            // Creates an EventHubsConnectionStringBuilder object from the connection string, and sets the EntityPath. updated
            // Typically, the connection string should have the entity path in it, but this simple scenario
            // uses the connection string from the namespace.
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(EhConnectionString)
            {
                EntityPath = EhEntityPath
            };

            eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
            while (true)
            {
                Console.WriteLine("Enter Message to send");
                string message = Console.ReadLine();
                if (message == "stop")
                {
                    break;
                }
                await SendMessagesToEventHub(message);
            }
            await eventHubClient.CloseAsync();


            Console.WriteLine("Press ENTER to exit.");
            Console.ReadLine();
        }
        private static async Task SendMessagesToEventHub(string message)
        {
            //for (var i = 0; i < numMessagesToSend; i++)
            //{
            try
            {
                //      var message = $"Message {i}";
                Console.WriteLine($"Sending message: {message}");
                await eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(message)));
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} > Exception: {exception.Message}");
            }

         //   await Task.Delay(10);
            //  }

            Console.WriteLine($"{message} messages sent.");
        }
    }
}
