using Azure.Data.Tables;
using AzureStorageQueueAndTableCRUD;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AzureStorageQueueAndTableCRUDUnitTest.Functional
{
    public class StorageAccountTableUnitTest
    {
        private readonly IConfiguration config=new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        private readonly StorageAccountTableService<TableEntity> _storageAccountTableService;

        public StorageAccountTableUnitTest()
        {
            _storageAccountTableService = new StorageAccountTableService<TableEntity>(config["ConnectionString"], config["QueueName"]);
        }

        [Fact]
        public void AddEntityIntoAzureStorageTableUnitTest()
        {
            var entitytest = new TableEntity("PartitionKey", "RowKey")
            {
                { "Property1", "Value1" },
                { "Property2", "Value2" },
                { "Property3", "Value3" }
            };

            var result = _storageAccountTableService.AddTableEntity(entitytest);
            result.Should().Be("204");
        }

        [Fact]
        public void UpdateEntityIntoAzureStorageTableUnitTest()
        {
            var tableEntityFromStorage = _storageAccountTableService.GetTableEntity("PartitionKey", "RowKey");

            //Adding new Key-Value's for updating entity:
            tableEntityFromStorage["UpdateTest1"] = "UpdatedValue1";
            tableEntityFromStorage["UpdateTest2"] = "UpdatedValue2";
            tableEntityFromStorage["UpdateTest3"] = "UpdatedValue3";

            var result = _storageAccountTableService.UpdateTableEntity(tableEntityFromStorage);
            result.Should().Be("204");
        }

        [Fact]
        public void GetEntityFromAzureStorageTableUnitTest()
        {
            var result = _storageAccountTableService.GetTableEntity("PartitionKey","RowKey");
            result.PartitionKey.Should().Be("TestPartitionKey");
        }
    }
}
