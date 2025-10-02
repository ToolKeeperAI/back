using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction;
using Service.Dto.Create;
using Service.Dto.Patch;
using Service.Model;
using Service.OperationResult;
using System.Text.Json;
using System.Text.Json.Serialization;
using ToolKeeperAIBackend.Extensions;

namespace ToolKeeperAIBackend.Controllers
{
	public class ToolsController : BaseDataController<Tool, ToolDto, PatchToolDto>
    {
        protected readonly IEmployeeService _employeeService;

        public ToolsController(IToolService toolService, IEmployeeService employeeService, IMapper mapper) : base(toolService, mapper)
        {
            _employeeService = employeeService;
        }

        [HttpGet("GetByKitId/{kitId:long}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetToolsByKitId([FromRoute] long kitId)
		{
            var result = await ((IToolService)_service).GetByToolKitIdAsync(kitId);

            return this.FromResult(result);
		}

        //TODO: add authentication

        [HttpPost("CheckToolsPresence/{toolKitSerialNumber}")]
        //[Authorize]
        public async Task<IActionResult> CheckToolsPresence([FromBody] ToolCheckingDto[] toolCheckings, [FromRoute] string toolKitSerialNumber)
        {
            var result = await ((IToolService)_service).CheckToolsPresenceAsync(toolCheckings, toolKitSerialNumber);

            if (result.IsSuccess)
                Console.WriteLine(JsonSerializer.Serialize(result.Data));

            return this.FromResult(result);
        }

        [HttpPost("TakeTools/{employeeId:long}")]
        //[Authorize]
        public async Task<IActionResult> TakeTools([FromBody] ToolMovementDto[] toolMovements, [FromRoute] long employeeId)
        {
            var searchEmployee = await _employeeService.GetByIdAsync(employeeId);

            if (!searchEmployee.IsSuccess)
                return NotFound($"Employee with id - {employeeId} doesnt exist");

            var result = await ((IToolService)_service).TakeToolsAsync(toolMovements, employeeId);

            return this.FromResult(result);
        }

        [HttpPost("ReturnTools/{employeeId:long}")]
        //[Authorize]
        public async Task<IActionResult> ReturnTools([FromBody] ToolMovementDto[] toolMovements, [FromRoute] long employeeId)
        {
            var searchEmployee = await _employeeService.GetByIdAsync(employeeId);

            if (!searchEmployee.IsSuccess)
                return NotFound($"Employee with id - {employeeId} doesnt exist");

            var result = await ((IToolService)_service).ReturnToolsAsync(toolMovements, employeeId);

            return this.FromResult(result);
        }

        [HttpPost("Test/{toolKitSerialNumber}")]
        [AllowAnonymous]
        public async Task<IActionResult> TestWorkability(IFormFile file, [FromRoute] string toolKitSerialNumber)
        {
            return Ok();
        }
    }
}
