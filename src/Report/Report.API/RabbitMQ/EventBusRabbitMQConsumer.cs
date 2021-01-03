using EventBusRabbitMQ.Abstract;
using EventBusRabbitMQ.Constants;
using EventBusRabbitMQ.Events;
using MediatR;
using MongoDB.Bson;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Report.Application.Responses;
using Report.Core.Entities;
using Report.Core.Enums;
using Report.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Report.API.RabbitMQ
{
    public class EventBusRabbitMQConsumer
    {
        private readonly IRabbitMQConnection _connection;
        private readonly IMediator _mediator;
        private readonly IReportRepository _reportRepository;

        public EventBusRabbitMQConsumer(IRabbitMQConnection connection, IMediator mediator, IReportRepository reportRepository)
        {
            _connection = connection;
            _mediator = mediator;
            _reportRepository = reportRepository;
        }

        public void Consume()
        {
            var channel = _connection.CreateModel();
            channel.QueueDeclare(queue: EventBusConstants.PrepareReportQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += ReceivedEvent;

            channel.BasicConsume(queue: EventBusConstants.PrepareReportQueue, autoAck: true, consumer: consumer, noLocal:false);

        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.PrepareReportQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var prepareReportEvent = JsonConvert.DeserializeObject<PrepareReportEvent>(message);

                //create a new report
                var reportEntity = new ReportEntity
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Date = DateTime.Now,
                    Status = ReportStatusEnum.Preparing.ToString()
                };

                await _reportRepository.CreateAsync(reportEntity);

                //update the report
                int totalPerson = 0;
                int totalPhoneNumber = 0;
                foreach (var person in prepareReportEvent.Persons)
                {
                    if (person.Addresses.Where(i => i.Location == prepareReportEvent.Location).Any())
                    {
                        totalPerson++;

                        foreach (var address in person.Addresses)
                        {
                            if (address.Location == prepareReportEvent.Location)
                            {
                                if (!string.IsNullOrWhiteSpace(address.Phone))
                                {
                                    totalPhoneNumber++;
                                }
                            }

                        }
                    }

                }

                await _reportRepository.UpdateAsync(reportEntity.Id, totalPerson, totalPhoneNumber);

            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}
