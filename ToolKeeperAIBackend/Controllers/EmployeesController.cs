using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction;
using Service.Dto;
using Service.Model;
using Service.OperationResult;

namespace ToolKeeperAIBackend.Controllers
{
    public class EmployeesController : BaseDataController<Employee, EmployeeDto>
    {
        public EmployeesController(IEmployeeService service) : base(service)
        {
            
        }

        //[HttpGet]
        //[AllowAnonymous]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Employee>))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        //public async Task<IActionResult> Get()
        //{
        //    return Ok();
        //}

        //[HttpGet("{employeeId:long}")]
        //[AllowAnonymous]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        //public async Task<IActionResult> GetById([FromRoute] long employeeId)
        //{
        //    return Ok();
        //}

        //[HttpPost]
        //[Authorize]
        //[Produces<Employee>()]
        //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Employee))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        //public async Task<IActionResult> Create([FromBody] EmployeeDto employeeDto)
        //{
        //    return Created();
        //}

        //[HttpPut("{employeeId:long}")]
        //[Authorize]
        //[Produces<Employee>()]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        //public async Task<IActionResult> Update([FromBody] EmployeeDto employeeDto, [FromRoute] long employeeId)
        //{
        //    return Ok();
        //}

        //[HttpDelete("{employeeId:long}")]
        //[Authorize]
        //[Produces<Employee>()]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        //public async Task<IActionResult> Delete([FromRoute] long employeeId)
        //{
        //    return Ok();
        //}
    }
}
