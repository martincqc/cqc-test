﻿using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GFOL.Helpers.TagHelpers
{

    [HtmlTargetElement("gfol-main-wrapper")]
    public class MainWrapperHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "main";
            output.Attributes.SetAttribute("class", "govuk-main-wrapper");
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }

}