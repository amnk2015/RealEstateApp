using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Models
{
    public class PropertyDetails
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }
        public string FullImageUrl => AppSettings.ApiUrl + ImageUrl;

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("bookmark")]
        public Bookmark Bookmark { get; set; }
    }

    public class Bookmark
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("status")]
        public bool Status { get; set; }

        [JsonProperty("user_Id")]
        public int UserId { get; set; }

        [JsonProperty("propertyId")]
        public int PropertyId { get; set; }
    }
}
