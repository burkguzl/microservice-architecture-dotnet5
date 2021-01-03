using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Report.Application.Queries;
using Report.Application.Responses;
using Report.Core.Entities;
using Report.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Report.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ReportResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<ReportResponse>> GetReportById(string id)
        {
            var query = new GetReportByIdQuery(id);
            var response = await _mediator.Send(query);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);

        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ReportResponse>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<ReportResponse>>> GetAllReports()
        {
            var query = new GetAllReportQuery();
            var response = await _mediator.Send(query);
            if (response == null)
            {
                return BadRequest();
            }
            return Ok(response);

        }
    }
}
