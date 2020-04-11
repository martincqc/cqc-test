using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GFOL.Helpers
{
    public class FormVM
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("form_name")]
        public string FormName { get; set; }
        [JsonProperty("pre_amble")]
        public string Preamble { get; set; }
        [JsonProperty("last_modified")]
        public string LastModified { get; set; }
        [JsonProperty("pages")]
        public List<PageVM>Pages { get; set; }
    }
}
