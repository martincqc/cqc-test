using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GFOL.Helpers
{
    public class PageVM
    {
        [JsonProperty("page_id")]
        public string PageId { get; set; }
        [JsonProperty("page_name")]
        public string PageName { get; set; }
        [JsonProperty("questions")]
        public List<Question> Questions { get; set; }
        [JsonProperty("buttons")]
        public List<Buttons> Buttons { get; set; }
        [JsonProperty("previous_page_id")]
        public string PreviousPageId { get; set; }
        [JsonProperty("next_page_id")]
        public string NextPageId { get; set; }
    }

    public class Buttons
    {
        [JsonProperty("button_text")]
        public string ButtonText { get; set; }
        [JsonProperty("button_type")]
        public string ButtonType { get; set; }
    }
    public class Question
    {
        [JsonProperty("question_id")]
        public string QuestionId { get; set; }
        [JsonProperty("question")]
        public string QuestionText { get; set; }
        [JsonProperty("data_type")]
        public string DataType { get; set; }
        [JsonProperty("input_type")]
        public string InputType { get; set; }
        [JsonProperty("options")]
        public string Options { get; set; }
        [JsonProperty("answer")]
        public string Answer { get; set; }
        [JsonProperty("validation")]
        public Validation Validation { get; set; }
    }

    public class Validation
    {
        [JsonProperty("required")]
        public Required Required { get; set; }
        [JsonProperty("is_errored")]
        public bool IsErrored { get; set; }
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
        [JsonProperty("string_length")]
        public StringLength StringLength { get; set; }

    }

    public class Required
    {
        [JsonProperty("is_required")]
        public bool IsRequired { get; set; }
        [JsonProperty("is_errored")]
        public bool IsErrored { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
    }

    public class StringLength
    {
        [JsonProperty("max_len")]
        public int MaxLen { get; set; }
        [JsonProperty("min_len")]
        public int MinLen { get; set; }
        [JsonProperty("is_errored")]
        public bool IsErrored { get; set; }

        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }
    }
}
