using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GFOL.Helpers.TagHelpers
{

    [HtmlTargetElement("gfol-breadcrumb-list-item")]
    public class BreadCrumbListItemHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            output.Attributes.SetAttribute("class", "govuk-breadcrumbs__list-item");

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }

}