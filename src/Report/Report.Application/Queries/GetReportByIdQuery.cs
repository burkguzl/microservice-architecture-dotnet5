using MediatR;
using Report.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Application.Queries
{
    public class GetReportByIdQuery: IRequest<ReportResponse>
    {
        public string Id { get; set; }
        public GetReportByIdQuery(string id)
        {
            Id = id;
        }
    }
}
