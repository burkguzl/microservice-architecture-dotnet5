using MediatR;
using Report.Application.Commands;
using Report.Application.Responses;
using Report.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Report.Application.Handlers
{
    public class PrepareReportHandler : IRequestHandler<PrepareReportCommand, ReportResponse>
    {
        private readonly IReportRepository _reportRepository;
        public PrepareReportHandler(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public async Task<ReportResponse> Handle(PrepareReportCommand request, CancellationToken cancellationToken)
        {
            int totalPerson = 0;
            int totalPhoneNumber = 0;
            foreach (var person in request.Persons)
            {
                if (person.Addresses.Where(i=> i.Location == request.Location).Any())
                {
                    totalPerson++;

                    foreach (var address in person.Addresses)
                    {
                        if (address.Location == request.Location)
                        {
                            if (!string.IsNullOrWhiteSpace(address.Phone))
                            {
                                totalPhoneNumber++;
                            }
                        }
                      
                    }
                }

            }

            await _reportRepository.UpdateAsync(request.ReportId, totalPerson, totalPhoneNumber);

            var response = await _reportRepository.GetAsync(request.ReportId);

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
