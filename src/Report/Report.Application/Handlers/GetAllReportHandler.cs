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
    public class GetAllReportHandler : IRequestHandler<GetAllReportQuery, List<ReportResponse>>
    {
        private readonly IReportRepository _reportRepository;
        public GetAllReportHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public async Task<List<ReportResponse>> Handle(GetAllReportQuery request, CancellationToken cancellationToken)
        {
            var model = new List<ReportResponse>();

            var response = await _reportRepository.GetAllAsync();

            if (response != null)
            {
                foreach (var report in response)
                {
                    model.Add(new ReportResponse
                    {
                        Id = report.Id,
                        Date = report.Date.ToString("dddd, dd MMMM yyyy"),
                        Location = report.Location,
                        PersonCount = report.PersonCount,
                        PhoneCount = report.PhoneCount,
                        Status = report.Status
                    });
                }
            }

            return model;
        }
    }
}
