using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace RedisDAL
{
    public class RedisConfigure
    {
        private readonly ConnectionMultiplexer redis;
        private readonly IDatabase _db;

        public RedisConfigure(IConfiguration configuration)
        {
            string connectionString = configuration.GetSection("Redis:ConnectionString").Value;
            redis = ConnectionMultiplexer.Connect(connectionString);
            _db = redis.GetDatabase();
        }

        public IDatabase GetDatabase() => _db;
    }
}
