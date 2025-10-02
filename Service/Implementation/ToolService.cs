using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Service.Abstraction;
using Service.Db;
using Service.Dto;
using Service.Exceptions;
using Service.Model;
using Service.OperationResult;

namespace Service.Implementation
{
	public class ToolService : BaseService<Tool, ToolDto>, IToolService
	{
		public ToolService(IMapper mapper, ToolKeeperDbContext dbContext) : base(dbContext, mapper)
		{

		}

		public async Task<Result> TakeTools(ToolMovementDto[] toolMovements, long employeeId)
		{
			HashSet<long> toolIds = toolMovements.Select(toolMovement => toolMovement.ToolId).ToHashSet();

			var checkResult = await CheckToolsAndEmployeeExistsAsync(toolIds, employeeId);

			if (!checkResult.IsSuccess)
				return checkResult;

			var toolInventories = await _dbContext.Inventory
				.Where(inventory => toolIds.Contains(inventory.ToolId))
				.ToDictionaryAsync(inventory => inventory.ToolId);

			var newToolUsages = new List<ToolUsage>();

			foreach (var toolMovement in toolMovements)
			{
				var toolInventory = toolInventories[toolMovement.ToolId];

				if (toolInventory.RemainQuantity < toolMovement.Quantity)
					return Result.Failure(ToolMovementErrors.NotEnoughToolsInStock(toolMovement.ToolId, toolMovement.Quantity, toolInventory.RemainQuantity));

				toolInventory.RemainQuantity -= toolMovement.Quantity;

				newToolUsages.Add(new ToolUsage()
				{
					EmployeeId = employeeId,
					ToolId = toolMovement.ToolId,
					Quantity = toolMovement.Quantity,
					IssueDate = DateTime.UtcNow
				});
			}

			try
			{
				var updated = await _dbContext.SaveChangesAsync();

				return updated > 0
					? Result.Success()
					: Result.Failure(EntityErrors.SaveToDbError(typeof(ToolUsage)));
			}
			catch (DbUpdateException ex)
			{
				throw new DatabaseException($"Failed to update entity: {ex.Message}", typeof(ToolUsage));
			}
		}

		public async Task<Result> ReturnTools(ToolMovementDto[] toolMovements, long employeeId)
		{
			HashSet<long> toolIds = toolMovements.Select(toolMovement => toolMovement.ToolId).ToHashSet();

			var checkResult = await CheckToolsAndEmployeeExistsAsync(toolIds, employeeId);

			if (!checkResult.IsSuccess)
				return checkResult;

			var toolInventories = await _dbContext.Inventory
				.Where(inventory => toolIds.Contains(inventory.ToolId))
				.ToDictionaryAsync(inventory => inventory.ToolId);

			var toolUsages = await _dbContext.ToolUsages
				.Where(toolUsage => toolIds.Contains(toolUsage.ToolId) &&
									toolUsage.EmployeeId == employeeId &&
									toolUsage.ReturnDate != null)
				.ToListAsync();

			var groupedToolUsages = toolUsages.GroupBy(toolUsage => toolUsage.ToolId);

			if (toolIds.Count > groupedToolUsages.Count())
			{
				var missedIds = toolIds.Except(groupedToolUsages.Select(groupedToolUsage => groupedToolUsage.Key));

				return Result.Failure(FindErrors.NotFoundCollectionOfEntities(missedIds, typeof(ToolUsage)));
			}

			foreach (var toolUsagesGroup in groupedToolUsages)
			{
				var toolInventory = toolInventories[toolUsagesGroup.Key];

				foreach (var toolUsage in toolUsagesGroup)
				{
					toolInventory.RemainQuantity += toolUsage.Quantity;

					if (toolInventory.RemainQuantity > toolInventory.TotalQuantity)
						return Result.Failure(ToolMovementErrors.ExtraToolQuantity(toolUsagesGroup.Key));

					toolUsage.ReturnDate = DateTime.UtcNow;
                }
			}

            try
            {
                var updated = await _dbContext.SaveChangesAsync();

                return updated > 0
                    ? Result.Success()
                    : Result.Failure(EntityErrors.SaveToDbError(typeof(ToolUsage)));
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException($"Failed to update entity: {ex.Message}", typeof(ToolUsage));
            }
        }

		protected async Task<Result> CheckToolsAndEmployeeExistsAsync(HashSet<long> toolIds, long employeeId)
		{
			var exists = await _dbContext.Employees.AnyAsync(employee => employee.Id == employeeId);

			if (!exists)
				return Result.Failure(FindErrors.NotFound(employeeId, typeof(Employee)));

			Result<IEnumerable<Tool>> findingResult = await this.GetByExpressionAsEnumerableAsync(tool => toolIds.Contains(tool.Id));

			var findingResultData = findingResult.Data;

			if (!findingResult.IsSuccess)
				return Result.Failure(FindErrors.NotFoundCollectionOfEntities(typeof(Tool)));

			// TODO: ignore tool ids that have not found (maybe). Partial update

			if (toolIds.Count > findingResultData!.Count())
			{
				var missedIds = toolIds.Except(findingResultData!.Select(tool => tool.Id));

				return Result.Failure(FindErrors.NotFoundCollectionOfEntities(missedIds, typeof(Tool)));
			}

			return Result.Success();
		}
	}
}
