using AzureStorageQueueAndTableCRUD;
using FluentAssertions;
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
        [Fact]
        public void AddMessageToAzureStorageQueueUnitTest()
        {
            // Arrange
            var queueStorage = new StorageAccountQueueService("ConnectionString", "QueueName");

            // Act
            var result = queueStorage.SendMessageAsync("Caner Test Message" + " " + DateTime.Now.ToString("yyyy/MM/dd HH:mm"));

            // Assert
            result.Status.Should().Be(201);
        }
    }
}
