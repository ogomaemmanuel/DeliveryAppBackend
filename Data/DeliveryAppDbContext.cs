using DeliveryAppBackend.Features.Accounts.Models;
using DeliveryAppBackend.Features.Chat.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DeliveryAppBackend.Data
{
    public class DeliveryAppDbContext : IdentityDbContext<SystemUser, AppRole, int>
    {
        public DeliveryAppDbContext(DbContextOptions<DeliveryAppDbContext> options) : base(options) => Database.EnsureCreated();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => base.OnConfiguring(optionsBuilder);
        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }
        public async override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            AddTimestamps();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void AddTimestamps()
        {
            var entities = ChangeTracker.Entries()
                .Where(x => x.Entity is TrackableEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                var now = DateTime.UtcNow; // current datetime

                if (entity.State == EntityState.Added)
                {
                    ((TrackableEntity)entity.Entity).CreatedAt = now;
                    ((TrackableEntity)entity.Entity).UpdatedAt = now;
                }
                ((TrackableEntity)entity.Entity).UpdatedAt = now;
            }
        }
    }

}
