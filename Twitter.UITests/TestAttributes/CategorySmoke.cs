using NUnit.Framework;

namespace Twitter.UITests.TestAttributes
{
    public class CategorySmoke : CategoryAttribute
    {
        public CategorySmoke() : base("Smoke") { }
    }
}
