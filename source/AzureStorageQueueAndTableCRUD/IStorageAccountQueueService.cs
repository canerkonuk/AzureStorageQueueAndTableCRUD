using Azure;

namespace AzureStorageQueueAndTableCRUD
{
    public interface IStorageAccountQueueService
    {
        Response SendMessageAsync(string message);
    }
}