namespace Moneys;

public class Program
{
    public static async Task Main(string[] args)
    {
        await Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
          var env = context.HostingEnvironment;
          config.SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
           .AddEnvironmentVariables();
       })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.ConfigureKestrel(op => {
                op.ListenAnyIP(8080);
            });
            webBuilder.UseStartup<Startup>();
        }).Build().RunAsync();
    }
}
