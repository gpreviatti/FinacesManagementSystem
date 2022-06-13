using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics.CodeAnalysis;

namespace Data.Context;

public class ContextFactory : IDesignTimeDbContextFactory<MyContext>
{
    [ExcludeFromCodeCoverage]
    public MyContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<MyContext>();
        
        builder
            .UseNpgsql("Host=localhost;Port=5432;Database=FinancesManagementSystem;User ID=postgres;Password=admin");

        return new MyContext(builder.Options);
    }
}