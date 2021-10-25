using ApplicationRegistry.Domain.Entities.Redis;
using ApplicationRegistry.Domain.Persistency;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationRegistry.Domain.Repositories
{
    public interface IRedisRepository : IRepository<RedisEntity>
    {
        Task<RedisEntity> GetWithEndpointsAsync(Guid id);

    }
}
