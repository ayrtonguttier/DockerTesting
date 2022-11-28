using Api.DatabaseConnection;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Testes;

public class CustomFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private MySqlTestcontainer _container = new TestcontainersBuilder<MySqlTestcontainer>()
        .WithDatabase(new MySqlTestcontainerConfiguration
        {
            Database = "TESTE",
            Username = "USUARIO",
            Password = "SENHA"
        }).Build();


    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(IDbConnectionFactory));
            services.AddSingleton<IDbConnectionFactory>(new DbConnectionFactory(_container.ConnectionString));
        });

        base.ConfigureWebHost(builder);
    }

    public async Task InitializeAsync()
    {
        await _container.StartAsync();

        
        var script = """
        CREATE TABLE IF NOT EXISTS TB_TESTE(
            ID INT,
            NOME VARCHAR(100)
        )
        """;
        
        await _container.ExecScriptAsync(script);
    }

    public new async Task DisposeAsync()
    {
        await _container.StopAsync();
    }
}