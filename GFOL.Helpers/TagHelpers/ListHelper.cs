using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace GFOL.Helpers.TagHelpers
{

    [HtmlTargetElement("gfol-list")]
    public class ListHelper : TagHelper
    {
        [HtmlAttributeName("list-type")]
        public GfolEnums.ListTypes ListType { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";

            output.Attributes.SetAttribute("class", GfolEnums.GetCssClassFromEnum(ListType));

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }

}