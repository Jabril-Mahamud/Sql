using Azure.Storage.Queues.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql.Interfaces;

public interface IQueueMessageProcessor
{
    Task ProcessMessageAsync(QueueMessage message);
}
