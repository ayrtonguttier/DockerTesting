using System.Net;
using Xunit.Abstractions;

namespace Testes;

public class TesteApi : IClassFixture<CustomFactory>
{
    private readonly ITestOutputHelper _output;
    private readonly CustomFactory _factory;

    public TesteApi(ITestOutputHelper output, CustomFactory factory)
    {
        _output = output;
        _factory = factory;
    }

    [Fact]
    public async Task Get()
    {
        var client = _factory.CreateClient();
        var result = await client.GetAsync("/Teste");
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var dados = await result.Content.ReadAsStringAsync();
        dados.Should().NotBeNullOrWhiteSpace();
        
        _output.WriteLine(dados);
    }
}