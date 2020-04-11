using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GFOL.Helpers.TagHelpers
{
    [HtmlTargetElement("gfol-container")]
    public class ContainerHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", "govuk-width-container");
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
