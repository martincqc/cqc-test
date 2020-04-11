using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GFOL.Helpers.TagHelpers
{

    [HtmlTargetElement("gfol-column", ParentTag = "gfol-row")]
    public class ColumnHelper : TagHelper
    {
        [HtmlAttributeName("desktop-size")]
        public GfolEnums.DesktopColumns DesktopSize { get; set; }

        [HtmlAttributeName("tablet-size")]
        public GfolEnums.TabletColumns TabletSize { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var tabletClass = GfolEnums.GetCssClassFromEnum(TabletSize);
            var desktopClass = GfolEnums.GetCssClassFromEnum(DesktopSize);

            output.TagName = "div";
            output.Attributes.SetAttribute("class", $"{tabletClass} {desktopClass}");
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}