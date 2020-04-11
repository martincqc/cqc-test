using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GFOL.Helpers.TagHelpers
{

    [HtmlTargetElement("gfol-row")]
    public class RowHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "govuk-grid-row");

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }

}