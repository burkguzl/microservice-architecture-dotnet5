﻿using MediatR;
using MongoDB.Bson;
using Report.Application.Commands;
using Report.Application.Responses;
using Report.Core.Entities;
using Report.Core.Enums;
using Report.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Report.Application.Handlers
{
    public class CreateReportHandler : IRequestHandler<CreateReportCommand, ReportResponse>
    {
        private readonly IReportRepository _reportRepository;
        public CreateReportHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<ReportResponse> Handle(CreateReportCommand request, CancellationToken cancellationToken)
        {
            var model = new ReportResponse();

            var reportEntity = new ReportEntity
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Date = DateTime.Now,
                Status = ReportStatusEnum.Preparing.ToString()
            };

            await _reportRepository.CreateAsync(reportEntity);

            var response = await _reportRepository.GetAsync(reportEntity.Id);

            if (response != null)
            {
                model = new ReportResponse
                {
                    Id = response.Id,
                    Status = response.Status,
                    Date = response.Date.ToString("dddd, dd MMMM yyyy")
                };
            }

            return model;
        }
    }
}
