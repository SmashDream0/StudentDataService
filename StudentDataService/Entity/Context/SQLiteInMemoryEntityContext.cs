using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace StudentDataService.Entity.Context
{
    public class SQLiteInMemoryEntityContext : EntityContext
    {
        public SQLiteInMemoryEntityContext(DbContextOptions options) : base(CreateSettings())
        { }

        static SQLiteInMemoryEntityContext()
        {
            if (_connection == null)
            { _connection = CreateInMemoryDatabase(); }
        }

        private static readonly DbConnection _connection;

        private static DbContextOptions CreateSettings()
        {
            var optionsBuilder = new DbContextOptionsBuilder<SQLiteInMemoryEntityContext>();
            optionsBuilder.UseSqlite("Data Source=:memory:");

            return optionsBuilder.Options;
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }
    }
}
