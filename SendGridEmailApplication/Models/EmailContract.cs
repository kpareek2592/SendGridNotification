using Newtonsoft.Json;

namespace SendGridEmailApplication.Models
{
    public class EmailContract
    {
        [JsonProperty("from", Order = 1)]
        public string From { get; set; }

        [JsonProperty("subject", Order = 2)]
        public string Subject { get; set; }

        [JsonProperty("body", Order = 3)]
        public string Body { get; set; }

        public string Alias { get; set; }

        [JsonProperty("tos", Order = 4)]
        public string ToEmailAddress { get; set; }

        [JsonProperty("ccs", Order = 5)]
        public string CcEmailAddress { get; set; }

        [JsonProperty("bccs", Order = 6)]
        public string BccEmailAddress { get; set; }
    }
}