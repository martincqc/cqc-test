using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GFOL.Helpers.TagHelpers
{

    [HtmlTargetElement("gfol-paragraph", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ParagraphHelper : TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "p";
            output.Attributes.SetAttribute("class", "govuk-body");

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }
}