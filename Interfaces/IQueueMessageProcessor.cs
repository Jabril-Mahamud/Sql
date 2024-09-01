using Azure.Storage.Queues.Models;

namespace Sql.Interfaces;

public interface IQueueMessageProcessor
{
    Task ProcessMessageAsync(QueueMessage message);
}