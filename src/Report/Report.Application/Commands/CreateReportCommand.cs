using MediatR;
using Report.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Application.Commands
{
    public class CreateReportCommand: IRequest<ReportResponse>
    {
        public string Location { get; set; }
    }
}
