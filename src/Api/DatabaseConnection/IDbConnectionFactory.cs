using System.Data;

namespace Api.DatabaseConnection;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}