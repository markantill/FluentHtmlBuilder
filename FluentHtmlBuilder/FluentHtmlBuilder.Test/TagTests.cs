using System;
using System.Linq;
using NUnit;
using NUnit.Framework;

namespace FluentHtmlBuilder.Test
{
    [TestFixture]
    public class TagTests
    {
        [Test]
        public void EmptySpan()
        {
            var element = Tag.Element("span").ToString();
            Assert.AreEqual("<span></span>", element);
        }

        [Test]
        public void SpanWithContent()
        {
            var element = Tag.Element("span", "content").ToString();
            Assert.AreEqual("<span>content</span>", element);
        }

        [Test]
        public void InnerHtmlSingleAndEnumeration()
        {
            var items = new[] { "one", "two", "three" };
            var list = Tag.Element("ul").WithInnerHtml(
                          Tag.Element("li", "First Thing in list"),
                          items.Select(i => Tag.Element("li", i))).ToString();

            Assert.AreEqual("<ul><li>First Thing in list</li>\r\n<li>one</li>\r\n<li>two</li>\r\n<li>three</li></ul>", list);
        }
    }
}
