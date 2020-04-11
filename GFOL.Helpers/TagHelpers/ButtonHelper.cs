using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GFOL.Helpers.TagHelpers
{

    [HtmlTargetElement("gfol-button", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ButtonHelper : TagHelper
    {
        public ButtonHelper()
        {
            ButtonStatus = GfolEnums.Status.Enabled;
        }

        [HtmlAttributeName("button-type")]
        public GfolEnums.Buttons ButtonType { get; set; }

        [HtmlAttributeName("button-text")]
        public string ButtonText { get; set; }

        [HtmlAttributeName("button-status")]
        public GfolEnums.Status ButtonStatus { get; set; }

        [HtmlAttributeName("start-now")]
        public bool StartNow { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button ";

            var btnClass = StartNow ? "govuk-button govuk-button--start" : "govuk-button";
            output.Attributes.SetAttribute("class", btnClass);

            output.Attributes.SetAttribute("type", ButtonType.ToString().ToLower());

            if (ButtonStatus == GfolEnums.Status.Disabled)
            {
                output.Attributes.SetAttribute("disabled", "disabled");
                output.Attributes.SetAttribute("aria-disabled", "true");
                output.Attributes.SetAttribute("class", "govuk-button govuk-button--disabled");
            }
            
            //var img = "<svg class='govuk - button__start - icon' xmlns='http://www.w3.org/2000/svg' width='17.5' height='19' viewBox='0 0 33 40' aria-hidden='true' focusable='false'>< path fill = 'currentColor' d = 'M0 0h13l20 20-20 20H0l20-20z' /></ svg >";
            //output.PostContent.SetHtmlContent(img);
            output.Content.SetContent(ButtonText);
        }
    }
}