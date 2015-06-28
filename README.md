# FluentHtmlBuilder
Build HTML fluently in the Linq to XML style 

This provides a set of extentions to the MVC TagBuilder to enable create HTML in a fluent stlye similar to LINQ to XML.

Example:

    var tag = Tag.Element("span", "some text);
    
    var items = new [] { "one", "two", "three" };
    var list = Tag.Element("ul").WithInnerHtml(
                  Tag.Element("li", "First Thing in list"), 
                  items.Select(i => Tag.Element("li", i))):
                  
    var html = list.ToString();
