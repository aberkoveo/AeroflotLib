using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using FC12.SupportExtensions.Models;
using Integra.Application.Interfaces;
using Integra.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Integra.Persistence
{
    public class SupportRequestDBContext : DbContext, ISupportRequestDBContext
    {
        public DbSet<SupportRequest> SupportRequests { get; set; }
        public SupportRequestDBContext(DbContextOptions<SupportRequestDBContext> opts)
            : base(opts) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SupportRequestConfiguration());
            base.OnModelCreating(builder);
        }
        
    }
}
