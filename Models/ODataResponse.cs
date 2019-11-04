using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FreeTime.Models
{
    public class ODataResponse
    {
        [JsonPropertyName("value")]
        public List<ODataValue> Value { get; set; } = new List<ODataValue>();
    }
}
