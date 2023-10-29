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
            // Arrange
            var tableStorage = new StorageAccountTableService<TableEntity>("ConnectionString", "TestTableName", "TestPartitionKey");
            var entitytest = new TableEntity("TestPartitionKey", "TestRowKey")
            {
                { "Property1", "Value1" },
                { "Property2", "Value2" },
                { "Property3", "Value3" }
            };

            // Act

            var result = tableStorage.AddTableEntity(entitytest);

            // Assert
            result.Should().Be("204");
        }


        [Fact]
        public void UpdateEntityIntoAzureStorageTableUnitTest()
        {
            // Arrange
            var tableStorage = new StorageAccountTableService<TableEntity>("ConnectionString", "TestTableName", "TestPartitionKey");
            var getEntityFromTable = tableStorage.GetEntitiesWithRowKeyFromPartition("TestRowKey");

            //Adding new Key-Value's for updating entity:
            getEntityFromTable["UpdateTest1"] = "UpdatedValue1";
            getEntityFromTable["UpdateTest2"] = "UpdatedValue2";
            getEntityFromTable["UpdateTest3"] = "UpdatedValue3";

            // Act
            var result = tableStorage.UpdateTableEntity(getEntityFromTable);

            // Assert
            result.Should().Be("204");
        }


        [Fact]
        public void GetEntityFromAzureStorageTableUnitTest()
        {
            // Arrange
            var tableStorage = new StorageAccountTableService<TableEntity>("ConnectionString", "TestTableName", "TestPartitionKey");

            // Act
            var result = tableStorage.GetEntitiesWithRowKeyFromPartition("TestRowKey");

            // Assert
            result.PartitionKey.Should().Be("TestPartitionKey");
        }
    }
}
