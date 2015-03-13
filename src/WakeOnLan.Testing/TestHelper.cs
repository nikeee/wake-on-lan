
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
