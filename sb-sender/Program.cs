using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;

namespace sb_sender
{
    class Program
    {

        const string ServiceBusConnectionString = "Endpoint=sb://andyrob-sbtest-ns.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=yIXr+s6Sy2f+a0NLmK/YQnc5aIClEfg4Ic0qAtC7G6o=";
        const string QueueName = "test-q";
        static IQueueClient queueClient;

        public static async Task Main(string[] args)
        {    
            const int numberOfMessages = 10;
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);


            try
            {
                while(true)
                {
                // Send messages.
                    await SendMessagesAsync(numberOfMessages);
                    Thread.Sleep(15000);
                }
            }
            finally
            {
                await queueClient.CloseAsync();
            }
        }

        static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {
                for (var i = 0; i < numberOfMessagesToSend; i++)
                {
                    // Create a new message to send to the queue.
                    string messageBody = $"Message {i}";
                    var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    // Write the body of the message to the console.
                    Console.WriteLine($"Sending message: {messageBody}");

                    // Send the message to the queue.
                    await queueClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
        
    }
}
