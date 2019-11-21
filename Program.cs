using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;

namespace QueueConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("apsettings.json");

            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            CloudStorageAccount account = CloudStorageAccount.Parse(config["connectionString"]);
            CloudQueueClient client = account.CreateCloudQueueClient();

            CloudQueue queue = client.GetQueueReference("filaprocesos");
            queue.CreateIfNotExists();

            for (int i = 0; i < 500; i++)
            {
                CloudQueueMessage message = new CloudQueueMessage("Operation no. " + i.ToString());
                queue.AddMessage(message);
                Console.WriteLine("Mensaje publicado : " + i.ToString());
            }
        }
    }
}
