using AutoMapper;
using Service.Abstraction;
using Service.Dto.Create;
using Service.Dto.Patch;
using Service.Model;

namespace ToolKeeperAIBackend.Controllers
{
    public class ToolKitsController : BaseDataController<ToolKit, ToolKitDto, PatchToolKitDto>
    {
        public ToolKitsController(IToolKitService service, IMapper mapper) : base(service, mapper)
        {

        }
    }
}
