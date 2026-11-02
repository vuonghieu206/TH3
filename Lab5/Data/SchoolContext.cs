using BaiThucHanh5_Net10.Models;
using Microsoft.EntityFrameworkCore;

namespace BaiThucHanh5_Net10.Data;

public class SchoolContext(DbContextOptions<SchoolContext> options) : DbContext(options)
{
    public DbSet<Learner> Learners => Set<Learner>();
    public DbSet<Major> Majors => Set<Major>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Major>().HasKey(m => m.MajorID);
        modelBuilder.Entity<Learner>().HasKey(l => l.LearnerID);
        modelBuilder.Entity<Learner>()
            .HasOne(l => l.Major)
            .WithMany(m => m.Learners)
            .HasForeignKey(l => l.MajorID)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
