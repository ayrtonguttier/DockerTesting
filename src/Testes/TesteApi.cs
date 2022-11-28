using System.Net;

namespace Testes;

public class TesteApi : IClassFixture<CustomFactory>
{
    private readonly CustomFactory _factory;

    public TesteApi(CustomFactory factory)
    {
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
    }
}