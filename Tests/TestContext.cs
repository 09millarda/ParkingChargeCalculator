using System.Collections.Generic;

namespace Tests
{
    public class TestContext
    {
        private readonly Dictionary<string, object> _entityDictionary;

        public TestContext()
        {
            _entityDictionary = new Dictionary<string, object>();
        }

        public void Set<TEntity>(TEntity entity, string key)
        {
            _entityDictionary.Add(key, entity);
        }

        public void Set<TEntity>(TEntity entity)
        {
            Set(entity, typeof(TEntity).FullName);
        }

        public TEntity Get<TEntity>(string key)
        {
            return (TEntity)_entityDictionary[key];
        }

        public bool TryGetValue<TEntity>(out TEntity entity)
        {
            entity = default;
            if (!_entityDictionary.ContainsKey(typeof(TEntity).FullName)) return false;

            entity = (TEntity)_entityDictionary[typeof(TEntity).FullName];

            return true;
        }

        public bool TryGetValue<TEntity>(string key, out TEntity entity)
        {
            entity = default;
            if (!_entityDictionary.ContainsKey(key)) return false;

            entity = (TEntity)_entityDictionary[key];

            return true;
        }
    }
}
