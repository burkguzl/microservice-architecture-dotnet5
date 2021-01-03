using RabbitMQ.Client;
using System;

namespace EventBusRabbitMQ.Abstract
{
    public interface IRabbitMQConnection : IDisposable
    {
        //check the rabbit mq connection
        bool IsConnected { get;}
        bool TryConnect();
        //perfom the create queue and apply queue operations
        IModel CreateModel();
    }
}
