using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace GFOL.AddressFinder
{
    public interface IAddressFinder
    {
        Task<List<AddressResult>> GetAddresses(string postcode, string apiKey);
        Task<List<AddressResult>> GetAddressesTest(string postcode, string apiKey);
    }
    public class AddressFinder : IAddressFinder
    {
        public async Task<List<AddressResult>> GetAddresses(string postcode, string apiKey)
        {
            var addressList = new List<AddressResult>();

            // Create the URL to API including API key and encoded address
            var addressUrl = $"https://ws.postcoder.com/pcw/{apiKey}/address/GB/{postcode}?page=0";

            // Create a disposable HTTP client
            using (var client = new HttpClient())
            {
                // Specify "application/json" in content-type header to request json return values
                // To request XML instead, simply change to "application/xml" or remove
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Execute our get request
                using (var resp = await client.GetAsync(addressUrl))
                {
                    // Triggered if API does not return 200 HTTP code
                    // More info - https://developers.alliescomputing.com/postcoder-web-api/error-handling

                    // Here we will output a basic message with HTTP code
                    if (!resp.IsSuccessStatusCode)
                    {
                        //output.error_message = $"An error occurred - {resp.StatusCode.ToString()}";
                    }
                    else
                    {
                        // Store JSON response in our list of AddressLookup objects
                        addressList = JsonConvert.DeserializeObject<List<AddressResult>>(await resp.Content.ReadAsStringAsync());

                    }
                }
            }

            return addressList;
        }

        public async Task<List<AddressResult>> GetAddressesTest(string postcode, string apiKey)
        {
            var addresses = new List<AddressResult>();
            using (var r = new StreamReader("Content/testResults.json"))
            {
                var file = r.ReadToEnd();

                addresses = JsonConvert.DeserializeObject<List<AddressResult>>(file);
            }

            return addresses;
        }
    }
}
