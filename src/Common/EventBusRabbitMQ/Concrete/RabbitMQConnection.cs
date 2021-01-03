using EventBusRabbitMQ.Abstract;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EventBusRabbitMQ.Concrete
{
    public class RabbitMQConnection : IRabbitMQConnection
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private bool _disposed;

        public RabbitMQConnection(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            if (!IsConnected)
            {
                TryConnect();
            }
        }

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }
        public bool TryConnect()
        {
            try
            {
                _connection = _connectionFactory.CreateConnection();
            }
            catch (Exception)
            {
                Thread.Sleep(2000);

                _connection = _connectionFactory.CreateConnection();
            }

            if (IsConnected)
            {
                return true;
            }

            return false;
        }


        public IModel CreateModel()
        {
            if (!IsConnected)
            {
                throw new InvalidOperationException("no rabbitmq connection");
            }

            return _connection.CreateModel();
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _connection.Dispose();
            }
        }


    }
}
