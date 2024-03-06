using Azure.Data.Tables;
using AzureStorageQueueAndTableCRUD;
using FluentAssertions;
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
        [Fact]
        public void AddEntityIntoAzureStorageTableUnitTest()
        {
            var tableStorage = new StorageAccountTableService<TableEntity>("ConnectionString", "TableName");
            var entitytest = new TableEntity("PartitionKey", "RowKey")
            {
                { "Property1", "Value1" },
                { "Property2", "Value2" },
                { "Property3", "Value3" }
            };

            var result = tableStorage.AddTableEntity(entitytest);
            result.Should().Be("204");
        }

        [Fact]
        public void UpdateEntityIntoAzureStorageTableUnitTest()
        {
            var tableStorage = new StorageAccountTableService<TableEntity>("ConnectionString", "TableName");
            var tableEntityFromStorage = tableStorage.GetTableEntity("PartitionKey", "RowKey");

            //Adding new Key-Value's for updating entity:
            tableEntityFromStorage["UpdateTest1"] = "UpdatedValue1";
            tableEntityFromStorage["UpdateTest2"] = "UpdatedValue2";
            tableEntityFromStorage["UpdateTest3"] = "UpdatedValue3";

            var result = tableStorage.UpdateTableEntity(tableEntityFromStorage);
            result.Should().Be("204");
        }

        [Fact]
        public void GetEntityFromAzureStorageTableUnitTest()
        {
            var tableStorage = new StorageAccountTableService<TableEntity>("ConnectionString", "TestTableName");
            var result = tableStorage.GetTableEntity("PartitionKey","RowKey");
            result.PartitionKey.Should().Be("TestPartitionKey");
        }
    }
}
