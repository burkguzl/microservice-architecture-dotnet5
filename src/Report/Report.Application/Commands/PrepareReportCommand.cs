using MediatR;
using Report.Application.Models;
using Report.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Application.Commands
{
    public class PrepareReportCommand: IRequest<ReportResponse>
    {
        public List<PersonModel> Persons { get; set; }
        public string ReportId { get; set; }
        public string Location { get; set; }
    }
}
