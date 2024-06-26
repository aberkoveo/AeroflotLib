﻿using Microsoft.EntityFrameworkCore;
using Integra.Domain.Support;

namespace Integra.Application.Interfaces;

public interface ISupportRequestDBContext
{
    DbSet<SupportRequest> SupportRequests { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}