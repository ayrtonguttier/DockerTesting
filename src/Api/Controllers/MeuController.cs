using Api.DatabaseConnection;
using Dapper;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("/Teste")]
public class MeuController: ControllerBase
{
    private readonly IDbConnectionFactory _factory;

    public MeuController(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    [HttpGet]
    public async Task<IActionResult> GetTabelas()
    {
        using var connection = _factory.CreateConnection();
        var result = await connection.QueryAsync("SHOW TABLES;");
        return Ok(result);
    }
}