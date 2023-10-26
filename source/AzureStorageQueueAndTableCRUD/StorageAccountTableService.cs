using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageQueueAndTableCRUD
{
    public class StorageAccountTableService<T>: IStorageTableRepository<T> where T : ITableEntity
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



    }
}
