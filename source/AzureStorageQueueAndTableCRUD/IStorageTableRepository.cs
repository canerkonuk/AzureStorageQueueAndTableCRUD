using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureStorageQueueAndTableCRUD
{
    public interface IStorageTableRepository<T> where T : class, ITableEntity, new()
    {
        string AddTableEntity(T entity);
        Dictionary<string, string> GetEntitiesWithPartitionKey(string rowKey);
    }
}
