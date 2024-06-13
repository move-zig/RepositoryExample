namespace RepositoryExample;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Data.Odbc;
using System.Threading;

public class Program : IHostedService
{
    static void Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        builder.Services.AddSingleton<IDbConnection>(new OdbcConnection(builder.Configuration.GetConnectionString("Default")));
        builder.Services.AddSingleton<IRepository, MyRepository>();
        builder.Services.AddHostedService<Program>();

        using var host = builder.Build();
        host.Start();
    }

    private readonly IRepository repository;

    public Program(IRepository repository)
    {
        this.repository = repository;
    }

    /// <inheritdoc/>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        this.repository.Open();

        try
        {
            this.repository.InsertFoo("bar");
        }
        finally
        {
            this.repository.Close();
        }
    }

    /// <inheritdoc/>
    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
