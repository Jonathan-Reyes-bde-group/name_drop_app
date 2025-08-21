using System.Text.Json.Serialization;

namespace ProjectNameDrop.Models.Response
{
    public class NationalizeGetResponse
    {
        // Reference: {"count":880552,"name":"johnson","country":[{"country_id":"US","probability":0.3041847971748919},{"country_id":"JM","probability":0.0914349090316131},{"country_id":"GB","probability":0.037436490888596685},{"country_id":"AU","probability":0.028975770577688745},{"country_id":"NG","probability":0.02797238836047705}]}
        public int Count { get; set; }
        public string Name { get; set; } = null!;
        public List<CountryProbability> Country { get; set; } = new List<CountryProbability>();
        public class CountryProbability
        {
            [JsonPropertyName("country_id")]
            public string CountryId { get; set; } = null!;
            [JsonPropertyName("probability")]
            public double Probability { get; set; }
        }
    }
}
