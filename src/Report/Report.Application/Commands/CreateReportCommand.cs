using MediatR;
using Report.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Application.Commands
{
    public class CreateReportCommand:IRequest<ReportResponse>
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public int PersonCount { get; set; }
        public int PhoneCount { get; set; }
        public string Status { get; set; }
    }
}
