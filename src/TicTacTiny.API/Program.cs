using Azure.Data.Tables;
using jkdmyrs.TicTacTiny.API;
using jkdmyrs.TicTacTiny.Infrastructure;
using jkdmyrs.TicTacTiny.API.Middleware;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(builder =>
    {
        builder.UseMiddleware<ExceptionHandlingMiddleware>();
    })
    .ConfigureServices(services =>
    {
        // configuration
        services
            .AddOptions<TableStorageClientOptions>().Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(nameof(TableStorageClientOptions)).Bind(settings);
            });

        // service registration
        services
            .AddLogging()
            .AddSingleton(sp =>
            {
                var options = sp.GetRequiredService<IOptions<TableStorageClientOptions>>();
                return new TableServiceClient(options.Value.ConnectionString ?? throw new ArgumentNullException(nameof(options)));
            })
            .AddSingleton<IStorageClient, TableStorageClient>()
            .AddSingleton<GameManager>();
    })
    .Build();

host.Run();

