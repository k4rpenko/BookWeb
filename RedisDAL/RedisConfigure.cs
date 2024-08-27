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


        public class Limiting
        {
            private static readonly TimeSpan RequestWindow = TimeSpan.FromMinutes(5);
            private static readonly TimeSpan BlockDuration = TimeSpan.FromMinutes(30);
            private static readonly int MaxRequests = 5;
            private readonly IDatabase db;

            public Limiting(IDatabase _db) { db = _db; }

            bool RedisUser(string ip)
            {
                var requestKey = $"requests:{ip}";

                if (!db.KeyExists(requestKey))
                {
                    db.HashSet(requestKey, new HashEntry[]
                    {
                        new HashEntry("Request", 0),
                        new HashEntry("Blocked", "0")
                    });
                    return true;
                }
                else
                {
                    if(db.HashGet(requestKey, "Blocked").ToString() == "0")
                    {
                        db.HashIncrement(requestKey, "Request", 1);
                    }
                    else
                    {
                        if (DateTime.UtcNow < DateTime.Parse(db.HashGet(requestKey, "Blocked").ToString()));
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
        }
    }
}
