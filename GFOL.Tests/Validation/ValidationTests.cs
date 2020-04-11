using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using GFOL.Helpers;
using Xunit;

namespace GFOL.Tests.Validation
{
    public class ValidationTests
    {
        [Theory]
        [InlineData("yes", 2, 3)]
        [InlineData("no", 2, 3)]
        [InlineData("YES", 2, 3)]
        [InlineData("NO", 2, 3)]
        [InlineData("Yes", 2, 3)]
        [InlineData("No", 2, 3)]
        [InlineData("qweoiuweroiu", 2, 20)]
        [InlineData("qwoeiuqwe qweoiu qwe", 2, 20)]
        public void TestValidationStringLengthPass(string answer, int minLen, int maxLen)
        {
            //arrange
            var pageToValidate = new PageVM
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        Answer = answer, DataType = "string", InputType = "text",
                        Validation = new Helpers.Validation
                        {
                            ErrorMessage = "",
                            Required = new Required
                            {
                                ErrorMessage = "required error", IsRequired = true
                            },
                            StringLength = new StringLength
                            {
                                ErrorMessage = "string length error", MinLen = minLen, MaxLen = maxLen
                            }
                        }
                    }
                }
            };
            var sut = new GFOLValidation();
            //act
            sut.ValidatePage(pageToValidate);
            var validationError = pageToValidate.Questions.Where(x => x.Validation.IsErrored).FirstOrDefault();
            var requiredError = pageToValidate.Questions.Where(x => x.Validation.Required.IsErrored).FirstOrDefault();
            var stringLenError = pageToValidate.Questions.Where(x => x.Validation.StringLength.IsErrored).FirstOrDefault();
            //assert
            validationError.Should().BeNull();
            requiredError.Should().BeNull();
            stringLenError.Should().BeNull();
        }
        [Theory]
        [InlineData("yess", 2, 3)]
        [InlineData("n", 2, 3)]
        [InlineData("YESS", 2, 3)]
        [InlineData("N", 2, 3)]
        [InlineData("Yess", 2, 3)]
        [InlineData("No000", 6, 10)]
        [InlineData("qweoiuweroiuQWOEIUQWE", 2, 20)]
        [InlineData("qwoeiuqwe qweoiu qweW", 2, 20)]
        public void TestValidationStringLengthFail(string answer, int minLen, int maxLen)
        {
            //arrange
            var pageToValidate = new PageVM
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        Answer = answer, DataType = "string", InputType = "text",
                        Validation = new Helpers.Validation
                        {
                            ErrorMessage = "",
                            Required = new Required
                            {
                                ErrorMessage = "required error", IsRequired = true
                            },
                            StringLength = new StringLength
                            {
                                ErrorMessage = "string length error", MinLen = minLen, MaxLen = maxLen
                            }
                        }
                    }
                }
            };
            var sut = new GFOLValidation();
            //act
            sut.ValidatePage(pageToValidate);
            var validationError = pageToValidate.Questions.Where(x => x.Validation.IsErrored).FirstOrDefault();
            var requiredError = pageToValidate.Questions.Where(x => x.Validation.Required.IsErrored).FirstOrDefault();
            var stringLenError = pageToValidate.Questions.Where(x => x.Validation.StringLength.IsErrored).FirstOrDefault();
            //assert
            validationError.Should().BeNull();
            requiredError.Should().BeNull();
            stringLenError.Should().NotBeNull();
            stringLenError.Validation.StringLength.IsErrored.Should().BeTrue();
            stringLenError.Validation.StringLength.ErrorMessage.Should().NotBeNullOrEmpty();
        }
        [Theory]
        [InlineData(null, 2, 20)]
        [InlineData("", 2, 20)]
        [InlineData("              ", 6, 10)]
        public void TestValidationRequiredFail(string answer, int minLen, int maxLen)
        {
            //arrange
            var pageToValidate = new PageVM
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        Answer = answer, DataType = "string", InputType = "text",
                        Validation = new Helpers.Validation
                        {
                            ErrorMessage = "",
                            Required = new Required
                            {
                                ErrorMessage = "required error", IsRequired = true
                            },
                            StringLength = new StringLength
                            {
                                ErrorMessage = "string length error", MinLen = minLen, MaxLen = maxLen
                            }
                        }
                    }
                }
            };
            var sut = new GFOLValidation();
            //act
            sut.ValidatePage(pageToValidate);
            var validationError = pageToValidate.Questions.Where(x => x.Validation.IsErrored).FirstOrDefault();
            var requiredError = pageToValidate.Questions.Where(x => x.Validation.Required.IsErrored).FirstOrDefault();
            var stringLenError = pageToValidate.Questions.Where(x => x.Validation.StringLength.IsErrored).FirstOrDefault();
            //assert
            validationError.Should().BeNull();
            requiredError.Should().NotBeNull();
            stringLenError.Should().BeNull();
            requiredError.Validation.Required.IsErrored.Should().BeTrue();
            requiredError.Validation.Required.ErrorMessage.Should().NotBeNullOrEmpty();
        }
        [Theory]
        [InlineData("qweoiuqwepoiqwepoiqwepoi@asdlkj.org", 5, 200)]
        [InlineData("qwe@qwe.co.uk", 5, 200)]
        [InlineData("qwe@qwe.com", 5, 200)]
        public void TestValidationEmailPass(string answer, int minLen, int maxLen)
        {
            //arrange
            var pageToValidate = new PageVM
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        Answer = answer, DataType = "email", InputType = "text",
                        Validation = new Helpers.Validation
                        {
                            ErrorMessage = "",
                            Required = new Required
                            {
                                ErrorMessage = "required error", IsRequired = true
                            },
                            StringLength = new StringLength
                            {
                                ErrorMessage = "string length error", MinLen = minLen, MaxLen = maxLen
                            }
                        }
                    }
                }
            };
            var sut = new GFOLValidation();
            //act
            sut.ValidatePage(pageToValidate);
            var validationError = pageToValidate.Questions.Where(x => x.Validation.IsErrored).FirstOrDefault();
            var requiredError = pageToValidate.Questions.Where(x => x.Validation.Required.IsErrored).FirstOrDefault();
            var stringLenError = pageToValidate.Questions.Where(x => x.Validation.StringLength.IsErrored).FirstOrDefault();
            //assert
            validationError.Should().BeNull();
            requiredError.Should().BeNull();
            stringLenError.Should().BeNull();
        }
        [Theory]
        [InlineData("qweoiuqwepoiqwepoiqwepoiasdlkj.org", 5, 200)]
        [InlineData("qweqweco.uk", 5, 200)]
        [InlineData("qwe@qwe", 5, 200)]
        [InlineData("weroiuweroiuweroiuweroiu", 5, 200)]
        public void TestValidationEmailFail(string answer, int minLen, int maxLen)
        {
            //arrange
            var pageToValidate = new PageVM
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        Answer = answer, DataType = "email", InputType = "text",
                        Validation = new Helpers.Validation
                        {
                            ErrorMessage = "validation error",
                            Required = new Required
                            {
                                ErrorMessage = "required error", IsRequired = true
                            },
                            StringLength = new StringLength
                            {
                                ErrorMessage = "string length error", MinLen = minLen, MaxLen = maxLen
                            }
                        }
                    }
                }
            };
            var sut = new GFOLValidation();
            //act
            sut.ValidatePage(pageToValidate);
            var validationError = pageToValidate.Questions.Where(x => x.Validation.IsErrored).FirstOrDefault();
            var requiredError = pageToValidate.Questions.Where(x => x.Validation.Required.IsErrored).FirstOrDefault();
            var stringLenError = pageToValidate.Questions.Where(x => x.Validation.StringLength.IsErrored).FirstOrDefault();
            //assert
            validationError.Should().NotBeNull();
            requiredError.Should().BeNull();
            stringLenError.Should().BeNull();
            validationError.Validation.IsErrored.Should().BeTrue();
            validationError.Validation.ErrorMessage.Should().NotBeNullOrEmpty();
        }
        [Theory]
        [InlineData("qw1 2as", 5, 100)]
        [InlineData("xx2 2xx", 5, 100)]
        public void TestValidationPostcodePass(string answer, int minLen, int maxLen)
        {
            //arrange
            var pageToValidate = new PageVM
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        Answer = answer, DataType = "postcode", InputType = "text",
                        Validation = new Helpers.Validation
                        {
                            ErrorMessage = "",
                            Required = new Required
                            {
                                ErrorMessage = "required error", IsRequired = true
                            },
                            StringLength = new StringLength
                            {
                                ErrorMessage = "string length error", MinLen = minLen, MaxLen = maxLen
                            }
                        }
                    }
                }
            };
            var sut = new GFOLValidation();
            //act
            sut.ValidatePage(pageToValidate);
            var validationError = pageToValidate.Questions.Where(x => x.Validation.IsErrored).FirstOrDefault();
            var requiredError = pageToValidate.Questions.Where(x => x.Validation.Required.IsErrored).FirstOrDefault();
            var stringLenError = pageToValidate.Questions.Where(x => x.Validation.StringLength.IsErrored).FirstOrDefault();
            //assert
            validationError.Should().BeNull();
            requiredError.Should().BeNull();
            stringLenError.Should().BeNull();
        }
        [Theory]
        [InlineData("qweqweqwe", 5, 100)]
        [InlineData("qweqweqwe werwerwer", 5, 100)]
        [InlineData("xxxxx", 5, 100)]
        public void TestValidationAddressFail(string answer, int minLen, int maxLen)
        {
            //arrange
            var pageToValidate = new PageVM
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        Answer = answer, DataType = "postcode", InputType = "text",
                        Validation = new Helpers.Validation
                        {
                            ErrorMessage = "validation error",
                            Required = new Required
                            {
                                ErrorMessage = "required error", IsRequired = true
                            },
                            StringLength = new StringLength
                            {
                                ErrorMessage = "string length error", MinLen = minLen, MaxLen = maxLen
                            }
                        }
                    }
                }
            };
            var sut = new GFOLValidation();
            //act
            sut.ValidatePage(pageToValidate);
            var validationError = pageToValidate.Questions.Where(x => x.Validation.IsErrored).FirstOrDefault();
            var requiredError = pageToValidate.Questions.Where(x => x.Validation.Required.IsErrored).FirstOrDefault();
            var stringLenError = pageToValidate.Questions.Where(x => x.Validation.StringLength.IsErrored).FirstOrDefault();
            //assert
            validationError.Should().NotBeNull();
            requiredError.Should().BeNull();
            stringLenError.Should().BeNull();
            validationError.Validation.IsErrored.Should().BeTrue();
            validationError.Validation.ErrorMessage.Should().NotBeNullOrEmpty();
        }
        [Theory]
        [InlineData("Firsname Lastname", 5, 100)]
        [InlineData("Firstname Middlename Lastname", 5, 100)]
        [InlineData("Firstname Middlename NextMiddlename Lastname", 5, 100)]
        public void TestValidationNamePass(string answer, int minLen, int maxLen)
        {
            //arrange
            var pageToValidate = new PageVM
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        Answer = answer, DataType = "fullname", InputType = "text",
                        Validation = new Helpers.Validation
                        {
                            ErrorMessage = "",
                            Required = new Required
                            {
                                ErrorMessage = "required error", IsRequired = true
                            },
                            StringLength = new StringLength
                            {
                                ErrorMessage = "string length error", MinLen = minLen, MaxLen = maxLen
                            }
                        }
                    }
                }
            };
            var sut = new GFOLValidation();
            //act
            sut.ValidatePage(pageToValidate);
            var validationError = pageToValidate.Questions.Where(x => x.Validation.IsErrored).FirstOrDefault();
            var requiredError = pageToValidate.Questions.Where(x => x.Validation.Required.IsErrored).FirstOrDefault();
            var stringLenError = pageToValidate.Questions.Where(x => x.Validation.StringLength.IsErrored).FirstOrDefault();
            //assert
            validationError.Should().BeNull();
            requiredError.Should().BeNull();
            stringLenError.Should().BeNull();
        }
        [Theory]
        [InlineData("Firsname", 5, 100)]
        [InlineData("Firsname,Lastname", 5, 100)]
        [InlineData("Firstname,Middlename,Lastname", 5, 100)]
        [InlineData("Firstname,Middlename,NextMiddlename,Lastname", 5, 100)]
        public void TestValidationNameFail(string answer, int minLen, int maxLen)
        {
            //arrange
            var pageToValidate = new PageVM
            {
                Questions = new List<Question>
                {
                    new Question
                    {
                        Answer = answer, DataType = "fullname", InputType = "text",
                        Validation = new Helpers.Validation
                        {
                            ErrorMessage = "validation error",
                            Required = new Required
                            {
                                ErrorMessage = "required error", IsRequired = true
                            },
                            StringLength = new StringLength
                            {
                                ErrorMessage = "string length error", MinLen = minLen, MaxLen = maxLen
                            }
                        }
                    }
                }
            };
            var sut = new GFOLValidation();
            //act
            sut.ValidatePage(pageToValidate);
            var validationError = pageToValidate.Questions.Where(x => x.Validation.IsErrored).FirstOrDefault();
            var requiredError = pageToValidate.Questions.Where(x => x.Validation.Required.IsErrored).FirstOrDefault();
            var stringLenError = pageToValidate.Questions.Where(x => x.Validation.StringLength.IsErrored).FirstOrDefault();
            //assert
            validationError.Should().NotBeNull();
            requiredError.Should().BeNull();
            stringLenError.Should().BeNull();
            validationError.Validation.IsErrored.Should().BeTrue();
            validationError.Validation.ErrorMessage.Should().NotBeNullOrEmpty();
        }
    }
}
