using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using Service.Abstraction;
using Service.Db;
using Service.Implementation;
using Service.Settings;
using ToolKeeperAIBackend.Automapper;

namespace ToolKeeperAIBackend
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<AppSettings>(builder.Configuration.GetRequiredSection(nameof(AppSettings)));

            builder.Services.AddDbContextFactory<ToolKeeperDbContext>((IServiceProvider serviceProvider, DbContextOptionsBuilder optionsBuilder) =>
            {
                var configuration = serviceProvider.GetRequiredService<IConfiguration>();

                var connectionString = configuration.GetConnectionString("DefaultConnection");

                optionsBuilder.UseNpgsql(connectionString);
            });

            builder.Services.AddHttpClient<HttpClient>("HttpClient", (serviceProvider, httpClient) =>
             {
                 var settings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value.ModelAPISettings;

                 httpClient.BaseAddress = new Uri(string.Concat(settings.Host, settings.Port));
             });

            builder.Services.AddAutoMapper(typeof(AppMappingProfile));

            builder.Services.AddTransient<IToolKitService, ToolKitService>();
            builder.Services.AddTransient<IToolService, ToolService>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            app.MapControllers();

            app.Run();
		}
	}
}
