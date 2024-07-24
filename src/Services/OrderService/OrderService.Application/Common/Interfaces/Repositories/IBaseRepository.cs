﻿using OrderService.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Common.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<TEntity> CreateAsync(TEntity entity, CancellationToken token = default);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default);
    Task<bool> DeleteAsync(TEntity entity, CancellationToken token = default);
}
