using System;
using Azure.Storage.Queues.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Sql.Functions
{
    public class QFunction
    {
        private readonly ILogger<QFunction> _logger;

        public QFunction(ILogger<QFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(QFunction))]
        public void Run([QueueTrigger("queue-items", Connection = "QString")] QueueMessage message)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {message.MessageText}");
        }
    }
}
