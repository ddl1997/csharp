using Microsoft.Extensions.Caching.StackExchangeRedis;
using StackExchange.Redis;
using System;

namespace CSharpTest.FuncTest.Utils
{
    public class DistributedLock : IDistributedLock
    {
        private static ConnectionMultiplexer redisConn;
        private static RedisCacheOptions options;
        private static int _database;
        private static TimeSpan lock_expiry = new TimeSpan(0, 1, 0);
        private static readonly object Locker = new object();
        #region lua script
        private static string script = @"
            local isExist = redis.call('exists', KEYS[1])
            if isExist == 0 then
                redis.call('set', KEYS[1], ARGV[1])
                redis.call('expire', KEYS[1], ARGV[2])
                return true
            else
                return false
            end";
        #endregion
        public static void Config(string configuration, string instanceName, int database = 0)
        {
            options = new RedisCacheOptions
            {
                Configuration = configuration,
                InstanceName = instanceName
            };
            _database = database;
            redisConn = ConnectionMultiplexer.Connect(options.Configuration);
        }

        private ConnectionMultiplexer GetConnection()
        {
            if (redisConn == null)
            {
                lock (Locker)
                {
                    if (redisConn == null || !redisConn.IsConnected)
                    {
                        try { redisConn = ConnectionMultiplexer.Connect(options.Configuration); }
                        catch (Exception) { }
                    }
                }
            }
            return redisConn;
        }

        public bool TryLockScript(string key, string value)
        {
            string result = GetConnection().GetDatabase(_database).ScriptEvaluate(script, new RedisKey[] { key }, new RedisValue[] { value, lock_expiry.TotalSeconds }).ToString();
            return "1".Equals(result, StringComparison.OrdinalIgnoreCase);
        }

        public bool TryLockTransaction(string key, string value)
        {
            var transaction = GetConnection().GetDatabase(_database).CreateTransaction();
            transaction.AddCondition(Condition.KeyNotExists(key));
            transaction.StringSetAsync(key, value, expiry: lock_expiry);
            return transaction.Execute();
        }

        public void UnLock(string key)
        {
            GetConnection().GetDatabase(_database).KeyDelete(key);
        }

        public bool ScriptTest()
        {
            return "True".Equals(
                GetConnection().GetDatabase(_database).ScriptEvaluate(script, new RedisKey[]{ "abc" }, new RedisValue[]{ "456", lock_expiry.TotalSeconds }).ToString(), 
                StringComparison.OrdinalIgnoreCase);
            
        }
    }
}
