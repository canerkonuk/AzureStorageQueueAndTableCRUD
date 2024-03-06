using Azure;
using Azure.Data.Tables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AzureStorageQueueAndTableCRUD
{
    public class StorageAccountTableService<T> : IStorageAccountTableService<T> where T : ITableEntity
    {

        private readonly string _connectionString;
        private readonly string _tableName;
        private TableClient _tableClient;

        public StorageAccountTableService(string connectionString, string tableName)
        {
            _connectionString = connectionString;
            _tableName = tableName;
            _tableClient = new TableClient(_connectionString, _tableName);
            _tableClient.CreateIfNotExists();
        }

        public string AddTableEntity(T entity)
        {
            var tableClientAddEntity = _tableClient.AddEntityAsync(entity);
            tableClientAddEntity.Wait();
            return tableClientAddEntity.Result.Status.ToString();
        }

        public string UpdateTableEntity(T entity)
        {
            var tableClientUpdateEntity = _tableClient.UpdateEntityAsync(entity, entity.ETag);
            tableClientUpdateEntity.Wait();
            return tableClientUpdateEntity.Result.Status.ToString();
        }

        public string DeleteTableEntity(T entity)
        {
            var tableClientUpdateEntity = _tableClient.DeleteEntityAsync(entity.PartitionKey, entity.RowKey);
            tableClientUpdateEntity.Wait();
            return tableClientUpdateEntity.Result.Status.ToString();
        }

        public TableEntity GetTableEntity(string partitionKey, string rowKey)
        {
            string partitionAndRowKeyFilter = $"PartitionKey eq '{partitionKey}' and RowKey eq '{rowKey}'";
            Pageable<TableEntity> pageableEntities = _tableClient.Query<TableEntity>(partitionAndRowKeyFilter);
            var entity = pageableEntities.FirstOrDefault();
            return entity;
        }
    }
}
