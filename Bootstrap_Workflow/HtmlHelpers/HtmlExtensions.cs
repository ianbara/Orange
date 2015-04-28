using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bootstrap_Workflow.HtmlHelpers
{
    public static class HtmlExtensions
    {


        /// <summary>
        /// Render a checkbox without an additional hidden input
        /// </summary>
        /// <remarks>
        /// From: http://stackoverflow.com/questions/2860940/asp-net-mvcwhy-does-the-checkboxfor-render-an-additional-input-tag-and-how-can
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="html"></param>
        /// <param name="field"></param>
        /// <param name="htmlAttributes"></param>
        /// <returns></returns>
        //public static MvcHtmlString BasicCheckBoxFor<T>(this HtmlHelper<T> html, Expression<Func<T, bool>> expression, IDictionary<string, object> htmlAttributes = null)
        public static MvcHtmlString BasicCheckBoxFor(this HtmlHelper html, Func<bool> field,
            IDictionary<string, object> htmlAttributes = null)
        {
            if (htmlAttributes == null)
            {
                throw new NullReferenceException("You must supply an HTML attributes object");
            }

            if (htmlAttributes["id"] == null)
            {
                throw new NullReferenceException("You must supply an HTML ID attribute");
            }

            var fieldValue = field.Invoke();

            var id = htmlAttributes["id"];
            var name = id;

            if (htmlAttributes["name"] != null)
            {
                name = htmlAttributes["name"];
            }

            var dataAttributes =
                htmlAttributes.Where(htmlAttribute => htmlAttribute.Key.StartsWith("data-"))
                    .Aggregate(string.Empty,
                        (current, htmlAttribute) =>
                            current + string.Format("{0}=\"{1}\" ", htmlAttribute.Key, htmlAttribute.Value));

            //TODO: add unobtrusive client validation rules if they exist
            var output = string.Format("<input id=\"{0}\" name=\"{1}\" type=\"checkbox\" {2} {3} />", id, name,
                fieldValue ? "checked" : string.Empty, dataAttributes);

            return MvcHtmlString.Create(output);
        }

    }
}