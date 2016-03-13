using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.PlatformAbstractions;


namespace Service.Data.Relational
{
    // [DbConfigurationType(typeof(RelationalDatabaseConfiguration))]
    // public class DataContext : DbContext 
    // {
    //     public DbSet<SampleEntity> Entites {get; set;}
    //     
    //     public DataContext(IConfiguration config) 
    //     : base(config.Get("Data:DefaultConnection:ConnectionString")) 
    //     { 
    //     } 
    // 
    //     protected override void OnModelCreating(ModelBuilder builder)
    //     {
    //         //var path = PlatformServices.Default.Application.ApplicationBasePath;
    //         //optionsBuilder.UseSqlite("Filename=" + Path.Combine(path, "sample.db"));
    //         builder.Entity<SampleEntity>().HasKey(m => m.Id); 
    //         base.OnModelCreating(builder); 
    //     }
    // }
}