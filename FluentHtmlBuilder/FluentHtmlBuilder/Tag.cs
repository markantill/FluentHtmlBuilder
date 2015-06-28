using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Collections;

namespace FluentHtmlBuilder
{
    /// <summary>
    /// Fluent helper methods for TagBuilder
    /// </summary>
    public static class Tag
    {
        /// <summary>
        /// Create a new HTML element.
        /// </summary>
        /// <param name="value">The element name.</param>
        /// <returns>TagBuilder for the element.</returns>
        public static TagBuilder Element(string value)
        {
            return new TagBuilder(value);
        }

        public static TagBuilder Element(string value, string innerText)
        {
            return new TagBuilder(value).WithInnerText(innerText);
        }

        public static TagBuilder WithId(this TagBuilder tagbuilder, string value)
        {
            if (value != null)
            {
                tagbuilder.GenerateId(value);
            }

            return tagbuilder;
        }

        public static TagBuilder WithAttribute(this TagBuilder tagbuilder, string key, string value)
        {
            if (value != null)
            {
                tagbuilder.Attributes.Add(key, value);
            }

            return tagbuilder;
        }

        public static TagBuilder WithCssClass(this TagBuilder tagbuilder, string value)
        {
            tagbuilder.AddCssClass(value);
            return tagbuilder;
        }

        public static TagBuilder WithCssClasses(this TagBuilder tagbuilder, IEnumerable <string> values)
        {
            foreach (var value in values)
            {
                tagbuilder.AddCssClass(value);    
            }
            
            return tagbuilder;
        }

        public static TagBuilder WithInnerHtml(this TagBuilder tagbuilder, string innerHtml)
        {
            tagbuilder.InnerHtml = innerHtml;
            return tagbuilder;
        }

        public static TagBuilder WithMergeAttributes(this TagBuilder tagbuilder, string key, string value, bool replaceExisting)
        {
            tagbuilder.MergeAttribute(key, value, replaceExisting);
            return tagbuilder;
        }

        public static TagBuilder WithMergeAttributes<TKey, TValue>(this TagBuilder tagbuilder, IDictionary<TKey, TValue> attributes)
        {
            tagbuilder.MergeAttributes(attributes);
            return tagbuilder;
        }

        public static TagBuilder WithMergeAttributes<TKey, TValue>(this TagBuilder tagbuilder, IDictionary<TKey, TValue> attributes, bool replaceExisting)
        {
            tagbuilder.MergeAttributes(attributes, replaceExisting);
            return tagbuilder;
        }

        public static TagBuilder WithInnerText(this TagBuilder tagbuilder, string innerText)
        {
            tagbuilder.SetInnerText(innerText);
            return tagbuilder;
        }

        public static TagBuilder WithInnerHtml(this TagBuilder tagbuilder, params object[] innerHtml)
        {
            var content = GetContent(innerHtml);
            tagbuilder.InnerHtml = content;
            return tagbuilder;
        }

        private static string GetContent(object[] innerHtml)
        {
            var content = string.Join(Environment.NewLine, from html in innerHtml
                                                           where html != null
                                                           let enumeration = html as IEnumerable
                                                           let result = html.ToString()
                                                           where !string.IsNullOrWhiteSpace(result)
                                                           select enumeration != null
                                                            && !typeof(string).IsAssignableFrom(html.GetType())
                                                                ? GetContent(enumeration.Cast<object>().ToArray())
                                                                : result);
            return content;
        }
    }
}

