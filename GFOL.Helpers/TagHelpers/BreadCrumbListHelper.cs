using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GFOL.Helpers.TagHelpers
{

    [HtmlTargetElement("gfol-breadcrumb-list")]
    public class BreadCrumbListHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ol";
            output.Attributes.SetAttribute("class", "govuk-breadcrumbs__list");

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }

}