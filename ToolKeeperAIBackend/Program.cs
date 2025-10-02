using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
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

            builder.Services.AddHttpClient<HttpClient>(nameof(HttpClient), (serviceProvider, httpClient) =>
             {
                 var settings = serviceProvider.GetRequiredService<IOptions<AppSettings>>().Value.ModelAPISettings;

                 httpClient.BaseAddress = new Uri(string.Join(':', settings.Host, settings.Port));
             });

            builder.Services.AddAutoMapper(typeof(AppMappingProfile));

            builder.Services.AddTransient<IToolKitService, ToolKitService>();
            builder.Services.AddTransient<IToolService, ToolService>();
            builder.Services.AddTransient<IEmployeeService, EmployeeService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ToolKeeperDbContext>();
                db.Database.Migrate();
            }

            app.MapControllers();

            app.Run();
		}
	}
}
