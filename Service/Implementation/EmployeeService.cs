using AutoMapper;
using Service.Abstraction;
using Service.Db;
using Service.Dto;
using Service.Model;

namespace Service.Implementation
{
	public class EmployeeService : BaseService<Employee, EmployeeDto>, IEmployeeService
	{
        public EmployeeService(IMapper mapper, ToolKeeperDbContext dbContext) :base(dbContext, mapper)
        {
            
        }
    }
}
