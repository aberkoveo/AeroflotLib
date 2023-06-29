using Integra.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integra.Persistence.EntityTypeConfigurations
{
    public class SupportRequestConfiguration : IEntityTypeConfiguration<SupportRequest>
    {
        public void Configure(EntityTypeBuilder<SupportRequest> builder)
        {
            builder.HasKey(request => request.ID) ;
            builder.HasIndex(request => request.ID).IsUnique();
            builder.Property(request => request.SMID).HasMaxLength(50);
            builder.Property(request => request.CreationDate).HasMaxLength(20);
            builder.Property(request => request.Priority);
            builder.Property(request => request.BatchId).HasMaxLength(10);
            builder.Property(request => request.BatchOwner).HasMaxLength(50);
            builder.Property(request => request.Categories);
            builder.Property(request => request.Comment);

        }
    }
}
