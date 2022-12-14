using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;

namespace VipCRM.Web
{
    public static class AutoComplete
    {
        public static MvcHtmlString TypeaheadFor<TModel, TValue>(
       this HtmlHelper<TModel> htmlHelper,
       Expression<Func<TModel, TValue>> expression,
       IEnumerable<string> source,
       int items = 8)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            if (source == null)
                throw new ArgumentNullException("source");

            var jsonString = new JavaScriptSerializer().Serialize(source);

            return htmlHelper.TextBoxFor(
                expression,
                new
                {
                    autocomplete = "off",
                    data_provide = "typeahead",
                    data_items = items,
                    data_source = jsonString
                }
            );
        } 
    }
}