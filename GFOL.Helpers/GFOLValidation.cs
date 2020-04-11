using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace GFOL.Helpers
{
    public interface IGFOLValidation
    {
        void ValidatePage(PageVM pageVm);
    }

    public class GFOLValidation : IGFOLValidation
    {
        private readonly string _postcodePattern = "^([A-Z]{1,2})([0-9][0-9A-Z]?) ([0-9])([ABDEFGHJLNPQRSTUWXYZ]{2})$";
        private readonly string _emailPattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
        public void ValidatePage(PageVM pageVm)
        {
            foreach (var question in pageVm.Questions)
            {
                ValidateQuestion(question);
            }
        }

        private void ValidateQuestion(Question question)
        {
            var answer = question.Answer;

            if (question.Validation.Required.IsRequired)
            {
                if (string.IsNullOrWhiteSpace(answer))
                {
                    question.Validation.Required.IsErrored = true;
                    return;
                }
            }

            if (question.Validation.StringLength != null)
            {
                if (answer.Length > question.Validation.StringLength.MaxLen ||
                    answer.Length < question.Validation.StringLength.MinLen)
                {
                    question.Validation.StringLength.IsErrored = true;
                    return;
                }
            }

            switch (question.DataType.ToLower())
            {
                case "postcode":
                {

                    var isValid = CheckPostcode(answer);
                    if (!isValid)
                    {
                        question.Validation.IsErrored = true;
                    }

                    break;
                }
                case "address":
                {
                    //check for at least 3 lines of address and a valid postcode
                    var addressLines = answer.Split(',');
                    var validPostcode = addressLines.Any(line => CheckPostcode(line.TrimStart()));
                    var isValid = (validPostcode && addressLines.Length > 3);
                        if (!isValid)
                        {
                            question.Validation.IsErrored = true;
                        }

                        break;
                }
                case "email":
                {
                    var isValid = CheckEmail(answer);
                    if (!isValid)
                    {
                        question.Validation.IsErrored = true;
                    }

                    break;
                }
                case "fullname":
                {
                    var isValid = CheckFullName(answer);
                    if (!isValid)
                    {
                        question.Validation.IsErrored = true;
                    }

                    break;
                }
            }

        }
        private bool CheckPostcode(string text)
        {
            if (Regex.IsMatch(text.ToUpper(), _postcodePattern))
            {
                return true;
            }

            return false;
        }
        private bool CheckEmail(string text)
        {
            if (Regex.IsMatch(text, _emailPattern))
            {
                return true;
            }

            return false;
        }
        private bool CheckFullName(string text)
        {
            var count = text.Split(' ').Length;
            return count>1;
        }
    }
}
