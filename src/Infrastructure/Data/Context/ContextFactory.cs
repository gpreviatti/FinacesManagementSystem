using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Context
{
    public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyContext>();
            //var dbConnection = "Persist Security Info=True;Server=DESKTOP-CJHGFFK;Database=FmsDB;User Id=application;Password=application";
            var dbConnection = "Server=(localdb)\\mssqllocaldb;Integrated Security=true;Initial Catalog=FmsDB";

            //SQLServer
            optionsBuilder.UseSqlServer(dbConnection).UseLazyLoadingProxies();

            return new MyContext(optionsBuilder.Options);
        }
    }
}
