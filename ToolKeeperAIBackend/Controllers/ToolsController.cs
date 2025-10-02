using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction;
using Service.Dto;
using Service.Model;
using Service.OperationResult;

namespace ToolKeeperAIBackend.Controllers
{
	public class ToolsController : BaseDataController<Tool, ToolDto>
    {
        protected readonly IEmployeeService _employeeService;

        public ToolsController(IToolService toolService, IEmployeeService employeeService) : base(toolService)
        {
            _employeeService = employeeService
        }

        [HttpGet("GetByKitId/{kitId:long}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Tool>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        public async Task<IActionResult> GetToolsByKitId([FromRoute] long kitId)
		{
			return Ok();
		}

        //[HttpGet("{toolId:long}")]
        //[AllowAnonymous]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tool))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        //public async Task<IActionResult> GetById([FromRoute] long toolId)
        //{
        //    return Ok();
        //}

        //[HttpPost]
        //[Authorize]
        //[Produces<Tool>()]
        //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Tool))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        //public async Task<IActionResult> Create([FromBody] ToolDto toolDto)
        //{
        //    return Created();
        //}

        //[HttpPut("{toolId:long}")]
        //[Authorize]
        //[Produces<Tool>()]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Tool))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        //public async Task<IActionResult> Update([FromBody] ToolDto toolDto, [FromRoute] long toolId)
        //{
        //    return Ok();
        //}

        //[HttpDelete("{toolId:long}")]
        //[Authorize]
        //[Produces<Tool>()]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        //public async Task<IActionResult> Delete([FromRoute] long toolId)
        //{
        //    return Ok();
        //}

        [HttpPost("CheckToolsPresence")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Error))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        public async Task<IActionResult> CheckToolsPresence([FromBody] ToolCheckingDto[] toolChekings)
        {
            return Ok();
        }

        [HttpPost("TakeTools/{employeeId:long}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Error))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        public async Task<IActionResult> TakeTools([FromBody] ToolMovementDto[] toolMovements, [FromRoute] long employeeId)
        {
            return Ok();
        }

        [HttpPost("ReturnTools/{employeeId:long}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
        [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(Error))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        public async Task<IActionResult> ReturnTools([FromBody] ToolMovementDto[] toolMovements, [FromRoute] long employeeId)
        {
            return Ok();
        }

        [HttpPost("Test/{toolKitSerialNumber}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TestResult))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        public async Task<IActionResult> TestWorkability(IFormFile file, [FromRoute] string toolKitSerialNumber)
        {
            return Ok();
        }
    }
}
