using Microsoft.Extensions.DependencyInjection;
using OCS.Applications.Tests.Fixtures;
using OCS.Applications.Infrastructure.Contexts;

namespace OCS.Applications.Tests;

public class ControllerTestsBase : IAsyncLifetime
{
    protected HttpClient Client { get; }
    protected AppDbContext Context { get; }


    private readonly IServiceScope _scope;

    public ControllerTestsBase(ApplicationsApiFactory applicationsApiFactory)
    {
        Client = applicationsApiFactory.Client;
        _scope = applicationsApiFactory.Services.CreateScope();
        Context = _scope.ServiceProvider.GetRequiredService<AppDbContext>();
    }

    public Task InitializeAsync()
    {
        return Context.Database.EnsureCreatedAsync();
    }

    public Task DisposeAsync()
    {
        Context.Database.EnsureDeleted();
        _scope.Dispose();
        return Task.CompletedTask;
    }
}