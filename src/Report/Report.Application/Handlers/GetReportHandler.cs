using MediatR;
using Report.Application.Queries;
using Report.Application.Responses;
using Report.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Report.Application.Handlers
{
    public class GetReportHandler : IRequestHandler<GetReportByIdQuery, ReportResponse>
    {
        private readonly IReportRepository _reportRepository;
        public GetReportHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public async Task<ReportResponse> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
        {
           
            var response = await _reportRepository.GetAsync(request.Id);
            var model = new ReportResponse
            {
                Id = response.Id,
                Date = response.Date.ToString("dddd, dd MMMM yyyy"),
                Location = response.Location,
                PersonCount = response.PersonCount,
                PhoneCount = response.PhoneCount,
                Status = response.Status
            };

            return model;


        }
    }
}
