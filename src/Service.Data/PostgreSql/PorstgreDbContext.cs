using System;
using System.Linq;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Service.Models;
 
 namespace Service.Data.PostgreSql
{ 
    public class PostgreDbContext : DbContext
    {
        public DbSet<SampleEntity> SampleEntities { get; set; }
        private IConfigurationRoot _config;
 
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SampleEntity>().HasKey(m => m.Id);
            builder.Entity<SampleEntity>().Property<DateTime>("UpdatedTimestamp"); 
            base.OnModelCreating(builder);
        }
 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var sqlConnectionString = 
               _config["DataAccessPostgreSqlProvider:ConnectionString"];
 
            optionsBuilder.UseNpgsql(sqlConnectionString);
        }
 
        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            updateUpdatedProperty<SampleEntity>();
            return base.SaveChanges();
        }
 
        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);
 
            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}
