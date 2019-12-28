using System;

namespace Tests
{
    public class TestBase
    {
        public TestContext Context { get; set; }

        public TestBase()
        {
            Context = new TestContext();
        }

        protected T GetCreate<T>(Func<T> create)
        {
            if (Context.TryGetValue<T>(out var result)) return result;
            result = create();
            Context.Set(result);
            return result;
        }

        protected T GetCreate<T>(Func<T> create, string key)
        {
            if (Context.TryGetValue<T>(key, out var result)) return result;
            result = create();
            Context.Set(result, key);
            return result;
        }
    }
}
