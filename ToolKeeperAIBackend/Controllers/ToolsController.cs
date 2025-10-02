using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Service.Abstraction;
using Service.Dto.Create;
using Service.Dto.Patch;
using Service.Model;
using Service.Settings;
using System.Text.Json;
using ToolKeeperAIBackend.Extensions;

namespace ToolKeeperAIBackend.Controllers
{
	public class ToolsController : BaseDataController<Tool, ToolDto, PatchToolDto>
    {
        protected readonly IEmployeeService _employeeService;
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly ModelAPISettings _settings;

        public ToolsController(IToolService toolService, IEmployeeService employeeService, 
                               IMapper mapper, IHttpClientFactory httpClientFactory, IOptions<AppSettings> options) 
        : base(toolService, mapper)
        {
            _employeeService = employeeService;
            _httpClientFactory = httpClientFactory;
            _settings = options.Value.ModelAPISettings;
        }

        [HttpGet("GetByKitId/{kitId:long}")]
        public async Task<IActionResult> GetToolsByKitId([FromRoute] long kitId)
		{
            var result = await ((IToolService)_service).GetByToolKitIdAsync(kitId);

            return this.FromResult(result);
		}

        [HttpPost("CheckToolsPresence/{toolKitSerialNumber}")]
        public async Task<IActionResult> CheckToolsPresence([FromBody] ToolCheckingDto[] toolCheckings, [FromRoute] string toolKitSerialNumber)
        {
            var result = await ((IToolService)_service).CheckToolsPresenceAsync(toolCheckings, toolKitSerialNumber);

            if (result.IsSuccess)
                Console.WriteLine(JsonSerializer.Serialize(result.Data));

            return this.FromResult(result);
        }

        [HttpPost("TakeTools/{employeeId:long}")]
        public async Task<IActionResult> TakeTools([FromBody] ToolMovementDto[] toolMovements, [FromRoute] long employeeId)
        {
            var searchEmployee = await _employeeService.GetByIdAsync(employeeId);

            if (!searchEmployee.IsSuccess)
                return NotFound($"Employee with id - {employeeId} doesnt exist");

            var result = await ((IToolService)_service).TakeToolsAsync(toolMovements, employeeId);

            return this.FromResult(result);
        }

        [HttpPost("ReturnTools/{employeeId:long}")]
        public async Task<IActionResult> ReturnTools([FromBody] ToolMovementDto[] toolMovements, [FromRoute] long employeeId)
        {
            var searchEmployee = await _employeeService.GetByIdAsync(employeeId);

            if (!searchEmployee.IsSuccess)
                return NotFound($"Employee with id - {employeeId} doesnt exist");

            var result = await ((IToolService)_service).ReturnToolsAsync(toolMovements, employeeId);

            return this.FromResult(result);
        }

        [HttpPost("Test/{toolKitSerialNumber}")]
        public async Task<IActionResult> TestWorkability(IFormFile archiveFile, [FromRoute] string toolKitSerialNumber = "")
        {
            Console.WriteLine("TEST!!!!");

            if (archiveFile == null || archiveFile.Length == 0)
                return BadRequest("There is not uploaded archive file");

            var photos = new List<byte[]>();
            var photoNames = new List<string>();

            using (var stream = archiveFile.OpenReadStream())
            using (var archive = new System.IO.Compression.ZipArchive(stream, System.IO.Compression.ZipArchiveMode.Read))
            {
                foreach (var entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        entry.FullName.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
                    {
                        using var entryStream = entry.Open();
                        using var ms = new MemoryStream();

                        await entryStream.CopyToAsync(ms);
                        
                        photos.Add(ms.ToArray());
                        photoNames.Add(entry.FullName);
                    }
                }
            }

            var responseMessage = await SendPhotosToApi(photos, photoNames);

            if (!responseMessage.IsSuccessStatusCode)
                return BadRequest(await responseMessage.Content.ReadAsStringAsync());

            var responseJson = await responseMessage.Content.ReadAsStringAsync();

            List<string> predictions = System.Text.Json.JsonSerializer.Deserialize<List<string>>(responseJson)!;

            var response = photoNames
                .Select((name, i) => new { FileName = name, Result = predictions[i] })
                .ToDictionary(x => x.FileName, x => x.Result);

            return Ok(response);
        }

        private async Task<HttpResponseMessage> SendPhotosToApi(List<byte[]> photos, List<string> photoNames)
        {
            using var form = new MultipartFormDataContent();

            for (int i = 0; i < photos.Count; i++)
            {
                form.Add(new ByteArrayContent(photos[i]), $"files", photoNames[i]);
            }

            using HttpClient httpClient = _httpClientFactory.CreateClient(nameof(HttpClient));

            return await httpClient.PostAsync(_settings.PredictBatchImagesUrl, form);
        }
    }
}
