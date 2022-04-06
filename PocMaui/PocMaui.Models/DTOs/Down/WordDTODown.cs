using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocMaui.Models.DTOs.Down
{
    public class WordDTODown
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("WordID")]
        public string WordID { get; set; }

        [JsonProperty("WordName")]
        public string WordName { get; set; }
    }
}
