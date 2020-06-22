using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IDbService _service;

        public TaskController(IDbService service)
        {
            _service = service;
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProject(int id)
        {
            _service.DeleteProject(id);
            return Ok();
        }

        [HttpGet("{id:int}")]
        public IActionResult GetTeamMember(int id)
        {
            return Ok(_service.GetTeamMember(id));
        }
    }
}