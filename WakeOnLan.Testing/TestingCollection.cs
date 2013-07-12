using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakeOnLan.Testing
{
    internal class TestingCollection<T, U> : List<TestItem<T, U>>
    {
        public void Add(T toTest, U expected)
        {
            Add(new TestItem<T, U>(toTest, expected));
        }
    }

    internal class TestItem<T, U>
    {
        public readonly T ToTest;
        public readonly U Expected;

        public TestItem(T toTest, U expected)
        {
            ToTest = toTest;
            Expected = expected;
        }
    }

    internal class BaSTestItem : TestItem<byte[], string>
    {
        public BaSTestItem(byte[] toTest, string expected)
            : base(toTest, expected)
        { }
    }

    internal class BaBTestItem : TestItem<byte[], bool>
    {
        public BaBTestItem(byte[] toTest, bool expected)
            : base(toTest, expected)
        { }
    }
    internal class BaBaTestItem : TestItem<byte[], byte[]>
    {
        public BaBaTestItem(byte[] toTest, byte[] expected)
            : base(toTest, expected)
        { }
    }
}
