using Azure;
using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        public Dictionary<string, string> GetEntitiesWithPartitionKey(string rowKey)
        {
            string partitionKeyFilter = $"PartitionKey eq '{_partitionKey}' and RowKey eq '{rowKey}'";
            Pageable<T> pageableEntities = _tableClient.Query<T>(partitionKeyFilter);
            var entity = pageableEntities.FirstOrDefault();

            object entityValue;
            Dictionary<string, string> entityKeyAndValues = new Dictionary<string, string>();
            if (entity != null)
            {
                var entityKeys = entity;
                //entityKeys.ForEach(entityKey =>
                //{
                //    entity.TryGetValue(entityKey, out entityValue);
                //    entityKeyAndValues.Add(entityKey.ToString(), entityValue.ToString());
                //});
            }
            return entityKeyAndValues;
        }


    }
}
