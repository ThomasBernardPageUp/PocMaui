using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocMaui.Models.DTOs.Down
{
    public class PictureColorsDTODown
    {
        [JsonProperty("colors")]
        public Colors Colors { get; set; }
    }

    public class Colors
    {
        [JsonProperty("dominant")]
        public ColorProperty Dominant { get; set; }

        [JsonProperty("accent")]
        public List<ColorProperty> Accent { get; set; }

        [JsonProperty("other")]
        public List<ColorProperty> Other { get; set; }
    }

    public class ColorProperty
    {
        [JsonProperty("hex")]
        public string Hex { get; set; }
    }
}
