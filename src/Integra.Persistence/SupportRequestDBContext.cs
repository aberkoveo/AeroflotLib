using Microsoft.EntityFrameworkCore;
using Integra.Application.Interfaces;
using Integra.Persistence.EntityTypeConfigurations;
using Integra.Domain.Support;

namespace Integra.Persistence;

public class SupportRequestDBContext : DbContext, ISupportRequestDBContext
{
    public DbSet<SupportRequest> SupportRequests { get; set; }

    public SupportRequestDBContext(DbContextOptions<SupportRequestDBContext> opts)
        : base(opts)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new SupportRequestConfiguration());
        base.OnModelCreating(builder);
    }
}