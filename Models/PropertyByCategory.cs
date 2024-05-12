using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Models
{
    public class PropertyByCategory
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("detail")]
        public string Detail { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        public string FullImageUrl => AppSettings.ApiUrl + ImageUrl;

        [JsonProperty("price")]
        public int Price { get; set; }

        [JsonProperty("isTrending")]
        public bool IsTrending { get; set; }

        [JsonProperty("categoryId")]
        public int CategoryId { get; set; }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("bookmarks")]
        public object Bookmarks { get; set; }
    }
}
