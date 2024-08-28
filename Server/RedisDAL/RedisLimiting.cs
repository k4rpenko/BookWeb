using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDAL
{
    class RedisLimiting
    {
        private static readonly TimeSpan RequestWindow = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan BlockDuration = TimeSpan.FromMinutes(30);
        private static readonly int MaxRequests = 5;
        private readonly IDatabase db;
        public RedisLimiting(IDatabase _db) { db = _db; }

        bool AuthRedisUser(string ip)
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
                if (db.HashGet(requestKey, "Blocked").ToString() == "0")
                {
                    db.HashIncrement(requestKey, "Request", 1);
                }
                else
                {
                    if (DateTime.UtcNow < DateTime.Parse(db.HashGet(requestKey, "Blocked").ToString())) ;
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
