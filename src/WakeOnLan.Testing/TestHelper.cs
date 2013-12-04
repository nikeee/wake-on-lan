using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakeOnLan.Testing
{
    public abstract class TestHelper
    {
        protected byte[] Ba(params byte[] bytes)
        {
            return bytes;
        }

        protected string[] Sa(params string[] strings)
        {
            return strings;
        }

        protected char[] Ca(params char[] chars)
        {
            return chars;
        }
    }
}