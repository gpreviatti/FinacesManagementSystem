using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Data.Context;

public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
{
    public MyContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<MyContext>();

        builder
            .UseNpgsql("Host=localhost;Port=5432;Database=FinancesManagementSystem;User ID=postgres;Password=admin");

        return new MyContext(builder.Options);
    }
}