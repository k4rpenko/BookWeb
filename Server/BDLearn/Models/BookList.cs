using Newtonsoft.Json;

namespace BDLearn.Models
{
    public class BookList
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("authors")]
        public string[] Authors { get; set; }

        [JsonProperty("pageCount")]
        public int PageCount { get; set; }
    }

    public class VolumeInfo
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("authors")]
        public string[] Authors { get; set; }

        [JsonProperty("pageCount")]
        public int PageCount { get; set; }


        [JsonProperty("imageLinks")]
        public imageLinks imageLinks { get; set; }

        [JsonProperty("publishedDate")]
        public string publishedDate { get; set; }
    }

    public class imageLinks
    {
        [JsonProperty("smallThumbnail")]
        public string smallThumbnail { get; set; }

        [JsonProperty("thumbnail")]
        public string thumbnail { get; set; }
    }

    public class listPrice
    {
        [JsonProperty("amount")]
        public string amount { get; set; }

        [JsonProperty("currencyCode")]
        public string currencyCode { get; set; }
    }

    public class saleInfo
    {
        [JsonProperty("country")]
        public string country{ get; set; }

        [JsonProperty("saleability")]
        public string saleability { get; set; }

        [JsonProperty("listPrice")]
        public listPrice listPrice { get; set; }
    }

    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("volumeInfo")]
        public VolumeInfo VolumeInfo { get; set; }

        [JsonProperty("saleInfo")]
        public saleInfo saleInfo { get; set; }

    }

    public class BooksApiResponse
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }
}
