using System.Text;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace GFOL.Helpers.HtmlHelpers
{
    public static class InlineHelpers
    {

        /// <summary>
        /// Creates a govuk compliant Question with label, help text and error messages
        /// </summary>
        /// <param name="helper">The HTML Class</param>
        /// <param name="question">The QuestionVM class</param>
        /// <param name="htmlAttributes">Any additional attributes to apply to the questions.</param>
        /// <returns>Returns the HTML for GDS compliant questions</returns>
        public static IHtmlContent GdsQuestion(this IHtmlHelper helper, Question question, object htmlAttributes = null)
        {
            IHtmlContent content;

            switch (question.InputType)
            {
                case "textbox":
                    content = BuildTextBox(question);
                    break;

                case "optionlist":
                    content = BuildOptionList(question);
                    break;

                case "selectlist":
                    content = BuildSelectList(question);
                    break;

                default:
                    content = BuildInfoPage(question);
                    break;
            }

            return new HtmlString(content.ToString());
        }

        private static IHtmlContent BuildInfoPage(Question question)
        {
            var title = question.QuestionText;

            var sb = new StringBuilder();

            sb.AppendLine($"<p class=\"govuk-body\">{title}</p>");

            return new HtmlString(sb.ToString());

        }

        private static IHtmlContent BuildTextBox(Question question)
        {
            var sb = new StringBuilder();
            sb.Append(GetErrorSection(question));
            sb.AppendLine("<div class=\"govuk-form-group\">");
            sb.AppendLine($"<label class=\"govuk-label\">{question.QuestionText}</label>");
            sb.AppendLine($"<input class=\"govuk-input govuk-input--width-20\" type=\"text\" id=\"Questions_0__Answer\" name=\"Questions[0].Answer\" value=\"{question.Answer}\" >");
            sb.AppendLine("</div>");

            return new HtmlString(sb.ToString());
        }

        private static IHtmlContent BuildOptionList(Question question)
        {
            var elementId = question.QuestionId;
            var isValidationErrored = question.Validation?.IsErrored;
            var validationErrorMsg = question.Validation?.ErrorMessage;
            var isRequiredErrored = question.Validation?.Required?.IsErrored;
            var requiredErrorMsg = question.Validation?.Required?.ErrorMessage;
            var isStringlenErrored = question.Validation?.StringLength?.IsErrored;
            var stringlenErrorMsg = question.Validation?.StringLength?.ErrorMessage;

            var sb = new StringBuilder();
            if (isValidationErrored.HasValue && (bool)isValidationErrored)
            {
                sb.AppendLine($"<h2 class=\"govuk-error-message\">{validationErrorMsg}</h2>");
            }
            if (isRequiredErrored.HasValue && (bool)isRequiredErrored)
            {
                sb.AppendLine($"<h2 class=\"govuk-error-message\">{requiredErrorMsg}</h2>");
            }
            if (isStringlenErrored.HasValue && (bool)isStringlenErrored)
            {
                sb.AppendLine($"<h2 class=\"govuk-error-message\">{stringlenErrorMsg}</h2>");
            }
            sb.AppendLine("<div class=\"govuk-form-group\">");
            sb.AppendLine("<fieldset class=\"govuk-fieldset\" aria-describedby=\"changed-name-hint\">");
            sb.AppendLine("<div class=\"govuk-radios govuk-radios--inline\">");
            foreach (var option in question.Options.Split(','))
            {
                var chk=(option == question.Answer ? "checked=\"checked\"" : "");
                sb.AppendLine("<div class=\"govuk-radios__item\">");
                sb.AppendLine($"<input class=\"govuk-radios__input\" type=\"radio\" {chk} value=\"{option}\" id=\"Questions_0__Answer\" name=\"Questions[0].Answer\" value=\"{question.Answer}\">");
                sb.AppendLine($"<label class=\"govuk-label govuk-radios__label\">{option}</label>");
                sb.AppendLine("</div>");
            }
            sb.AppendLine("</div>");
            sb.AppendLine("</fieldset>");
            sb.AppendLine("</div>");

            return new HtmlString(sb.ToString());
        }

        private static IHtmlContent BuildSelectList(Question question)
        {
            var elementId = question.QuestionId;
            var isValidationErrored = question.Validation?.IsErrored;
            var validationErrorMsg = question.Validation?.ErrorMessage;
            var isRequiredErrored = question.Validation?.Required?.IsErrored;
            var requiredErrorMsg = question.Validation?.Required?.ErrorMessage;
            var isStringlenErrored = question.Validation?.StringLength?.IsErrored;
            var stringlenErrorMsg = question.Validation?.StringLength?.ErrorMessage;

            var sb = new StringBuilder();
            if (isValidationErrored.HasValue && (bool)isValidationErrored)
            {
                sb.AppendLine($"<h2 class=\"govuk-error-message\">{validationErrorMsg}</h2>");
            }
            if (isRequiredErrored.HasValue && (bool)isRequiredErrored)
            {
                sb.AppendLine($"<h2 class=\"govuk-error-message\">{requiredErrorMsg}</h2>");
            }
            if (isStringlenErrored.HasValue && (bool)isStringlenErrored)
            {
                sb.AppendLine($"<h2 class=\"govuk-error-message\">{stringlenErrorMsg}</h2>");
            }
            sb.AppendLine("<div class=\"govuk-form-group\">");
            sb.AppendLine("<fieldset class=\"govuk-fieldset\" aria-describedby=\"changed-name-hint\">");
            sb.AppendLine($"<span id=\"changed-name-hint\" class=\"govuk-hint\">{question.QuestionText}</span>");
            sb.AppendLine("<div class=\"govuk-radios govuk-radios--inline\">");
            sb.AppendLine("<select class=\"gfol-dropdown\" id=\"Questions_0__Answer\" name=\"Questions[0].Answer\">");
            foreach (var option in question.Options.Split(','))
            {
                var chk = (option == question.Answer ? "selected=\"true\"" : "");
                sb.AppendLine($"<option class=\"govuk-radios__item\" {chk}>{option}</option>");
            }
            sb.AppendLine("</select>");
            sb.AppendLine("</div>");
            sb.AppendLine("</fieldset>");
            sb.AppendLine("</div>");

            return new HtmlString(sb.ToString());
        }

        private static string GetErrorSection(Question question)
        {
            var isValidationErrored = question.Validation?.IsErrored;
            var validationErrorMsg = question.Validation?.ErrorMessage;
            var isRequiredErrored = question.Validation?.Required?.IsErrored;
            var requiredErrorMsg = question.Validation?.Required?.ErrorMessage;
            var isStringlenErrored = question.Validation?.StringLength?.IsErrored;
            var stringlenErrorMsg = question.Validation?.StringLength?.ErrorMessage;

            var sb = new StringBuilder();
            if (isValidationErrored.HasValue && (bool)isValidationErrored)
            {
                sb.AppendLine($"<h2 class=\"govuk-error-message\">{validationErrorMsg}</h2>");
            }
            if (isRequiredErrored.HasValue && (bool)isRequiredErrored)
            {
                sb.AppendLine($"<h2 class=\"govuk-error-message\">{requiredErrorMsg}</h2>");
            }
            if (isStringlenErrored.HasValue && (bool)isStringlenErrored)
            {
                sb.AppendLine($"<h2 class=\"govuk-error-message\">{stringlenErrorMsg}</h2>");
            }

            return sb.ToString();
        }
        public static IHtmlContent GfolButton(this IHtmlHelper helper, string buttonType, string buttonText, object htmlAttributes = null)
        {
            var button = new TagBuilder("button");
            button.Attributes.Add("class", "govuk-button");
            button.Attributes.Add("type", buttonType.ToLower());
            button.InnerHtml.Append(buttonText);

            button.MergeHtmlAttributes(htmlAttributes);

            return button;
        }

        private static void MergeHtmlAttributes(this TagBuilder tagBuilder, object htmlAttributes)
        {
            if (htmlAttributes != null)
            {
                var customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var customAttribute in customAttributes)
                {
                    tagBuilder.MergeAttribute(customAttribute.Key, customAttribute.Value.ToString());
                }
            }
        }

    }
}
