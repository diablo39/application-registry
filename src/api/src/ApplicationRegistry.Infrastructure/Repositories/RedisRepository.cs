using ApplicationRegistry.Domain.Entities.Redis;
using ApplicationRegistry.Domain.Repositories;
using ApplicationRegistry.Infrastructure.Domain.Persistency;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Infrastructure.Repositories
{
    internal class RedisRepository : RepositoryBase<RedisEntity>, IRedisRepository
    {
        public RedisRepository(DbContext context) : base(context)
        {
        }

        public async Task<RedisEntity> GetWithEndpointsAsync(Guid id)
        {
            return await _set.Include(e => e.Endpoints).FirstAsync(e => e.Id == id);
        }
    }
}
