using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GFOL.Helpers.TagHelpers
{

    [HtmlTargetElement("gfol-fieldset", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class FieldsetHelper
        : TagHelper
    {
        [HtmlAttributeName("fieldset-type")]
        public GfolEnums.FieldsetTypes FieldsetType { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "fieldset";

            output.Attributes.SetAttribute("class", GfolEnums.GetCssClassFromEnum(FieldsetType));

            var children = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(children);
        }
    }
}