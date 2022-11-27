namespace fall_project_2.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

public class DatabaseContextFactory : IDesignTimeDbContextFactory<ApplicationDataContext>
{
    public ApplicationDataContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<ApplicationDataContext>()
            .UseSqlite("Data Source=./app.db");

        return new ApplicationDataContext(builder.Options);
    }
}