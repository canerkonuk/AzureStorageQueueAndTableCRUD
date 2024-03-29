using Azure.Storage.Queues;
using Azure;
using Azure.Storage.Queues.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageQueueAndTableCRUD
{
    public class StorageAccountQueueService : IStorageAccountQueueService
    {
        private readonly string _connectionString;
        private readonly string _queueName;
        private QueueClient _queueClient;

        public StorageAccountQueueService(string connectionString, string queueName)
        {
            _connectionString = connectionString;
            _queueName = queueName;
            _queueClient = new QueueClient(_connectionString, _queueName.ToLower());
            _queueClient.CreateIfNotExists();
        }

        public Response SendMessageAsync(string message)
        {
            if (_queueClient.Exists())
            {
                var bytes = Encoding.UTF8.GetBytes(message);
                var clientQueueRequest = _queueClient.SendMessage(Convert.ToBase64String(bytes));

                return clientQueueRequest.GetRawResponse();
            }
            return null;
        }
    }
}
