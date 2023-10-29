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
    public class StorageAccountTableService<T> : IStorageTableRepository<T> where T : class, ITableEntity, new()
    {

        private readonly string _connectionString;
        private readonly string _tableName;
        private readonly string _partitionKey;
        private TableClient _tableClient;

        public StorageAccountTableService(string connectionString, string tableName, string partitionKey)
        {
            _connectionString = connectionString;
            _tableName = tableName;
            _tableClient = new TableClient(_connectionString, _tableName);
            _tableClient.CreateIfNotExists();
            _partitionKey = partitionKey;
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


        public T GetEntitiesWithRowKeyFromPartition(string rowKey)
        {
            string partitionKeyFilter = $"PartitionKey eq '{_partitionKey}' and RowKey eq '{rowKey}'";
            Pageable<T> pageableEntities = _tableClient.Query<T>(partitionKeyFilter);
            var entity = pageableEntities.FirstOrDefault();

            //Dictionary<string, string> entityDict = new Dictionary<string, string>();
            //if (entity != null)
            //{
            //    var entityJson=JsonConvert.SerializeObject(entity);
            //    entityDict= JsonConvert.DeserializeObject<Dictionary<string, string>>(entityJson);
            //}
            return entity;
        }


    }
}
