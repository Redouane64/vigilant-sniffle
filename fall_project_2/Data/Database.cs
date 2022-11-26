
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<ApplicationDataContext>
{
    public ApplicationDataContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ApplicationDataContext>()
            .UseSqlite("Data Source=./app.db");

        var context = new ApplicationDataContext(builder.Options);

        return context;
    }
}