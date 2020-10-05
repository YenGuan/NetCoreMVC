using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace NetCoreIdentity.Web.Helpers
{
    public static class HtmlHelperInputExtensions
    {

        public static IHtmlContent ComboBoxFor<TModel, TValue>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, List<string> selectItems, object htmlAttributes = null)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException(nameof(htmlHelper));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }
            if (selectItems == null)
            {
                selectItems = new List<string>();
            }
            IModelExpressionProvider modelExpressionProvider = htmlHelper.ViewContext.HttpContext.RequestServices
            .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;
            ModelExpression modelExpression = modelExpressionProvider.CreateModelExpression(htmlHelper.ViewData, expression);

            //return new HtmlString($"<span>propName: {modelExpression.Metadata.PropertyName} | value: {(string)modelExpression.Model}<span>");

            TagBuilder divBuilder = new TagBuilder("div");
            divBuilder.AddCssClass("autocomplete");
            TagBuilder inputBuilder = new TagBuilder("input");
            inputBuilder.GenerateId(modelExpression.Metadata.PropertyName, "_");
            inputBuilder.Attributes.Add("name", modelExpression.Metadata.PropertyName);
            inputBuilder.Attributes.Add("autocomplete", "off");
            TagBuilder datasourceBuilder = new TagBuilder("div");

            datasourceBuilder.GenerateId($"{modelExpression.Metadata.PropertyName}#datasource", "_");
            datasourceBuilder.AddCssClass("autocomplete-datasource");    
            foreach (var selectItem in selectItems)
            {
                //datasourceBuilder.Attributes.Add("data-source", string.Join(',', selectItems));
                var item = new TagBuilder("div");
                item.AddCssClass("autocomplete-datasource-item");
                item.Attributes.Add("autocomplete-datasource-item", selectItem);
                datasourceBuilder.InnerHtml.AppendHtml(item);
            }
            divBuilder.InnerHtml.AppendHtml(inputBuilder);
            divBuilder.InnerHtml.AppendHtml(datasourceBuilder);
            if (htmlAttributes != null)
                inputBuilder.MergeAttributes(GetHtmlAttributeDictionaryOrNull(htmlAttributes));
 
            return divBuilder;
        }
     
        private static IDictionary<string, object> GetHtmlAttributeDictionaryOrNull(object htmlAttributes)
        {
            IDictionary<string, object> htmlAttributeDictionary = null;
            if (htmlAttributes != null)
            {
                htmlAttributeDictionary = htmlAttributes as IDictionary<string, object>;
                if (htmlAttributeDictionary == null)
                {
                    htmlAttributeDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                }
            }

            return htmlAttributeDictionary;
        }
    }
}
