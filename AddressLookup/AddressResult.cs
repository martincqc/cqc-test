using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AddressLookup
{
    public  class AddressResult
    {
        [JsonProperty("summaryline")]
        public string SummaryLine { get; set; }
        [JsonProperty("buildingname")]
        public string BuildingName { get; set; }
        [JsonProperty("number")]
        public string Number { get; set; }
        [JsonProperty("premise")]
        public string Premise { get; set; }
        [JsonProperty("street")]
        public string Street { get; set; }
        [JsonProperty("dependentlocality")]
        public string DependentLocality { get; set; }
        [JsonProperty("posttown")]
        public string PostTown { get; set; }
        [JsonProperty("county")]
        public string County { get; set; }
        [JsonProperty("postcode")]
        public string Postcode { get; set; }
    }
}
