using Newtonsoft.Json;

namespace PocMaui.Models.DTOs.Down
{
    public class ColorDTODown
    {
        [JsonProperty("colors")]
        public List<Color> Colors { get; set; }
    }

    public class Color
    {
        [JsonProperty("hex")]
        public Hex Hex { get; set; }
    }
    public class Hex
    {
        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("clean")]
        public string Clean { get; set; }
    }
}
