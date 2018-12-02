
namespace WakeOnLan.Testing
{
    public abstract class TestHelper
    {
        protected byte[] Ba(params byte[] bytes) => bytes;
        protected string[] Sa(params string[] strings) => strings;
        protected char[] Ca(params char[] chars) => chars;
    }
}
