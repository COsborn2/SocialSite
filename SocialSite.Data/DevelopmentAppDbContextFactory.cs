using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace SocialSite.Data
{
    public class DevelopmentAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // This is only used when adding migrations and updating the database from the cmd line.
            // It shouldn't ever be used in code where it might end up running in production.
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var localConnectionString = Environment.OSVersion.Platform == PlatformID.Unix 
                ? "Server=localhost,1433;Database=SocialSite;User ID=SA;Password=Password1!" 
                : "Server=(localdb)\\MSSQLLocalDB;Database=SocialSite;Trusted_Connection=True;MultipleActiveResultSets=True;";

            builder.UseSqlServer(localConnectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
