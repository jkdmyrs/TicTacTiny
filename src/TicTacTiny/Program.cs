using jkdmyrs.TicTacTiny.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices(services =>
    {
        services.AddSingleton<IStorageClient, TableStorageClient>();
    })
    .Build();

host.Run();

