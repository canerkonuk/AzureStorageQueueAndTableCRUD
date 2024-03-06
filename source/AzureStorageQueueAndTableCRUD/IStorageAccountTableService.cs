using Azure.Data.Tables;

namespace AzureStorageQueueAndTableCRUD
{
    public interface IStorageAccountTableService<T> where T : ITableEntity
    {
        string AddTableEntity(T entity);
        string DeleteTableEntity(T entity);
        TableEntity GetTableEntity(string rowKey, string partitionKey);
        string UpdateTableEntity(T entity);
    }
}