using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GFOL.Helpers
{
    public class SubmissionVM
    {
        public string Id { get; set; }
        [JsonProperty("form_name")]
        public string FormName { get; set; }
        [JsonProperty("date_created")]
        public string DateCreated { get; set; }
        [JsonProperty("answers")]
        public List<Answer> Answers { get; set; }
    }

    public class Answer
    {
        [JsonProperty("page_id")]
        public string PageId { get; set; }
        [JsonProperty("question_id")]
        public string QuestionId { get; set; }
        [JsonProperty("question")]
        public string Question { get; set; }
        [JsonProperty("answer")]
        public string AnswerText { get; set; }
        [JsonProperty("show_in_confirm")]
        public bool ShowInConfirm { get; set; }
    }
}
