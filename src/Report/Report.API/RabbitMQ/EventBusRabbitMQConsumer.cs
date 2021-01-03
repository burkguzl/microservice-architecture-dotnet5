using EventBusRabbitMQ.Abstract;
using EventBusRabbitMQ.Constants;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Events.Models;
using MediatR;
using MongoDB.Bson;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Report.Application.Commands;
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

            channel.BasicConsume(queue: EventBusConstants.PrepareReportQueue, autoAck: true, consumer: consumer, noLocal: false);

        }

        private async void ReceivedEvent(object sender, BasicDeliverEventArgs e)
        {
            if (e.RoutingKey == EventBusConstants.PrepareReportQueue)
            {
                var message = Encoding.UTF8.GetString(e.Body.Span);
                var prepareReportEvent = JsonConvert.DeserializeObject<PrepareReportEvent>(message);

                //create a new report
                var createCommand = new CreateReportCommand
                {
                    Location = prepareReportEvent.Location
                };

                var createCommandResponse = await _mediator.Send(createCommand);

                //update the report
                var prepareReportCommand = new PrepareReportCommand
                {
                    Location = prepareReportEvent.Location,
                    Persons = prepareReportEvent.Persons.Select(i => new Application.Models.PersonModel
                    {
                        Id = i.Id,
                        Company = i.Company,
                        FirstName = i.FirstName,
                        LastName = i.LastName,
                        Addresses = i.Addresses.Select(x => new Application.Models.AddressModel
                        {
                            Id = x.Id,
                            AddressType = x.AddressType,
                            Email = x.Email,
                            Location = x.Location,
                            Phone = x.Phone
                        }).ToList()
                    }).ToList(),
                    ReportId = createCommandResponse.Id,
                    RequestId = prepareReportEvent.RequestId
                };

                await _mediator.Send(prepareReportCommand);
            }
        }

        public void Disconnect()
        {
            _connection.Dispose();
        }
    }
}
