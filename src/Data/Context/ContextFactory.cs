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
        
        builder.UseInMemoryDatabase("FinancesManagementSystem");

        return new MyContext(builder.Options);
    }
}
