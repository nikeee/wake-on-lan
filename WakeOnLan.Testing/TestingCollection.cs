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

    internal class TestingCollection<T, U, V> : List<TestItem<T, U, V>>
    {
        public void Add(T toTest1, U toTest2, V expected)
        {
            Add(new TestItem<T, U, V>(toTest1, toTest2, expected));
        }
    }

    internal class TestItem<T, U>
    {
        public readonly T ToTest1;
        public readonly U Expected;

        public TestItem(T toTest, U expected)
        {
            ToTest1 = toTest;
            Expected = expected;
        }
    }
    internal class TestItem<T, U, V> : TestItem<T, V>
    {
        public readonly U ToTest2;
        public TestItem(T toTest1, U toTest2, V expected)
            : base(toTest1, expected)
        {
            ToTest2 = toTest2;
        }
    }

    internal class BaBaBTestItem : TestItem<byte[], byte[], bool>
    {
        public BaBaBTestItem(byte[] toTest1, byte[] toTest2, bool expected)
            : base(toTest1, toTest2, expected)
        { }
    }
    internal class BaBaBaTestItem : TestItem<byte[], byte[], byte[]>
    {
        public BaBaBaTestItem(byte[] toTest1, byte[] toTest2, byte[] expected)
            : base(toTest1, toTest2, expected)
        { }
    }

    internal class BaSTestItem : TestItem<byte[], string>
    {
        public BaSTestItem(byte[] toTest, string expected)
            : base(toTest, expected)
        { }
    }

    internal class BaITestItem : TestItem<byte[], int>
    {
        public BaITestItem(byte[] toTest, int expected)
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
