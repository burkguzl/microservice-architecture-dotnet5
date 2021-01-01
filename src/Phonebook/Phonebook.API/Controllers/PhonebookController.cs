using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PhonebookController: ControllerBase
    {
        [HttpGet("try")]
        public async Task<IActionResult> Task()
        {
            return Content("Hello world");
        }
    }
}
