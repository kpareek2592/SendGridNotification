using Newtonsoft.Json;

namespace SendGridEmailApplication.Models
{
    public class SmsContract
    {
        [JsonProperty("from", Order = 1)]
        public string From { get; set; }

        [JsonProperty("body", Order = 2)]
        public string Body { get; set; }

        [JsonProperty("to", Order = 3)]
        public string ToPhoneNumber { get; set; }        
    }
}