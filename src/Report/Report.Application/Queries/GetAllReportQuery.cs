using MediatR;
using Report.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Report.Application.Queries
{
    public class GetAllReportQuery: IRequest<List<ReportResponse>>
    {
    }
}
