using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Persistence
{
    public class DbConnections
    {
        private readonly string sqlConnectionStringLocalDB;
        private readonly string sqlConnectionStringLocalCSJ;

        public DbConnections(IConfiguration configuration)
        {
            sqlConnectionStringLocalCSJ = configuration.GetConnectionString("SqlServerCSJ");
            sqlConnectionStringLocalDB = configuration.GetConnectionString("Local");

        }
        //Conexion a mi base local
        public IDbConnection CreateSqlConnectionLocalDB() => new SqlConnection(sqlConnectionStringLocalDB);

        //Conexion a la base de Legajos de CSJ
        public IDbConnection CreateSqlConnectionCSJ() => new SqlConnection(sqlConnectionStringLocalCSJ);

        //public IDbConnection CreateSqlConnectionCSJ() => new SqlConnection(sqlConnectionStringLocalDB);

    }
}
