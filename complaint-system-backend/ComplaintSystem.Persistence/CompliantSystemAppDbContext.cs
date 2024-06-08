using ComplaintSystem.Domain.Entities;
using ComplaintSystem.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Persistence;

public class ComplaintSystemAppDbContext : DbContext
{
    public ComplaintSystemAppDbContext(DbContextOptions<ComplaintSystemAppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ComplaintSystemAppDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Complaint> Complaints { get; set; }
    public DbSet<ComplaintLog> ComplaintLogs { get; set; }
    public DbSet<CorruptionTrend> CorruptionTrends { get; set; }
    public DbSet<Manager> Managers { get; set; }
    public DbSet<OTPEntity> OTPs { get; set; }
    public DbSet<Subordinate> Subordinates { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<NotificationEntity> Notifications { get; set; }

}
