using AzureStorageQueueAndTableCRUD;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AzureStorageQueueAndTableCRUDUnitTest.Functional
{
    public class StorageAccountQueueUnitTest
    {
        private readonly IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        private readonly StorageAccountQueueService _storageAccountQueueService;
        public StorageAccountQueueUnitTest()
        {
            _storageAccountQueueService = new StorageAccountQueueService(config["ConnectionString"], config[""]);
        }

        [Fact]
        public void AddMessageToAzureStorageQueueUnitTest()
        {
            // Arrange
            // Act
            var result = _storageAccountQueueService.SendMessageAsync("Queue Test Message" + " " + DateTime.Now.ToString("yyyy/MM/dd HH:mm"));

            // Assert
            result.Status.Should().Be(201);
        }
    }
}
