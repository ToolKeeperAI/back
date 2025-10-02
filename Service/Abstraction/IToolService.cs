using Service.Dto;
using Service.Model;
using Service.OperationResult;

namespace Service.Abstraction
{
	public interface IToolService : IBaseEntityService<Tool, ToolDto>
	{
		Task<Result> TakeTools(ToolMovementDto[] toolMovements, long employeeId);

		Task<Result> ReturnTools(ToolMovementDto[] toolMovements, long employeeId);
	}
}
