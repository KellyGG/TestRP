
using TestRP.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.IO;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;

namespace TestRP
{
public class DatabaseContext : DbContext
{
    public DbSet<Product> ProductAll { get; set; }
          private readonly IConfiguration _config;

        public DatabaseContext(IConfiguration config)
    {
           _config=config;
        Database.EnsureCreated();
      
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var currentPath=System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
        string databasePath=_config.GetSection("DbConnectionConfig").GetSection("PathDatabase").Value;
         // optionsBuilder.UseSqlite($"Data Source={currentPath}\\Product.db");
          optionsBuilder.UseSqlite($@"{databasePath}");
         
        
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
         {
             // Map table names
             modelBuilder.Entity<Product>().ToTable("Product", "table");
             modelBuilder.Entity<Product>(entity =>
             {
                 entity.HasKey(e => e.Id);
                 entity.HasIndex(e => e.Name).IsUnique();
                 entity.Property(e => e.Description);
             });
            base.OnModelCreating(modelBuilder);
         }
}


}
