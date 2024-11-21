
using Microsoft.EntityFrameworkCore;
using SigmaSoftware.Domain.Entities;

namespace SigmaSoftware.Infrastructure.DBContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Candidate> Candidates { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Candidate>()
            .HasIndex(c => c.Email)
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}
