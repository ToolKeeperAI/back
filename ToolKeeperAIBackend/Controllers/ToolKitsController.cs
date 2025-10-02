using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstraction;
using Service.Dto;
using Service.Model;
using Service.OperationResult;

namespace ToolKeeperAIBackend.Controllers
{
    public class ToolKitsController : BaseDataController<ToolKit, ToolKitDto>
    {
        public ToolKitsController(IToolKitService service) : base(service)
        {

        }

        //[HttpGet]
        //[AllowAnonymous]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ToolKit>))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        //public async Task<IActionResult> Get()
        //{
        //    return Ok();
        //}

        //[HttpGet("{toolKitId:long}")]
        //[AllowAnonymous]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ToolKit))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        //public async Task<IActionResult> GetById([FromRoute] long toolKitId)
        //{
        //    return Ok();
        //}

        //[HttpPost]
        //[Authorize]
        //[Produces<ToolKit>()]
        //[ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ToolKit))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        //public async Task<IActionResult> Create([FromBody] ToolKitDto toolKitDto)
        //{
        //    return Created();
        //}

        //[HttpPut("{toolKitId:long}")]
        //[Authorize]
        //[Produces<ToolKit>()]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ToolKit))]
        //[ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        //public async Task<IActionResult> Update([FromBody] ToolKitDto toolKitDto, [FromRoute] long toolKitId)
        //{
        //    return Ok();
        //}

        //[HttpDelete("{toolKitId:long}")]
        //[Authorize]
        //[Produces<ToolKit>()]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Error))]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(Error))]
        //public async Task<IActionResult> Delete([FromRoute] long toolKitId)
        //{
        //    return Ok();
        //}
    }
}
