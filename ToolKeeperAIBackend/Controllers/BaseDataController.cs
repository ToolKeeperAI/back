using Microsoft.AspNetCore.Mvc;
using Service.Abstraction;
using Service.Model;
using System.Linq.Expressions;
using ToolKeeperAIBackend.Extensions;

namespace ToolKeeperAIBackend.Controllers
{
    [ApiController]
    [Route("odata/api/v1/[controller]")]
    public abstract class BaseDataController<T, TDto> : ControllerBase
        where T : BaseModel
        where TDto : class
    {
        protected readonly IBaseEntityService<T, TDto> _service;

        protected BaseDataController(IBaseEntityService<T, TDto> service)
        {
            _service = service;
        }

        [HttpGet]
        public virtual ActionResult<IQueryable<T>> GetAll() => Ok(_service.Get());

        [HttpGet("{key:long}")]
        public virtual async Task<IActionResult> GetById([FromRoute] long key)
        {
            var result = await _service.GetByExpressionAsEnumerableAsync(GetPredicatToFindById(key));

            return this.FromResult(result);
        }

        [HttpDelete("{key:long}")]
        public virtual async Task<IActionResult> Delete([FromRoute] long key)
        {
            var result = await _service.DeleteAsync(key);

            return this.FromResult(result);
        }

        [HttpPost]
        public virtual async Task<ActionResult> Create([FromBody] TDto dto)
        {
            var result = await _service.CreateAsync(dto);

            if (!result.IsSuccess)
                return this.FromResult(result);

            var query = await _service.GetByExpressionAsEnumerableAsync(GetPredicatToFindById(result.Data!));

            return this.FromResult(query);
        }

        [HttpPut("{key:long}")]
        public virtual async Task<IActionResult> Change([FromRoute] long key, [FromBody] TDto dto)
        {
            var result = await _service.UpdateAsync(key, dto);

            if (!result.IsSuccess)
                return this.FromResult(result);

            var query = await _service.GetByExpressionAsEnumerableAsync(GetPredicatToFindById(key));

            return this.FromResult(query);
        }

        protected virtual Expression<Func<T, bool>> GetPredicatToFindById(long key) => entity => entity.Id == key;

        protected virtual Expression<Func<T, bool>> GetPredicatToFindById(T entity) => dbEntity => dbEntity.Id == entity.Id;
    }
}
