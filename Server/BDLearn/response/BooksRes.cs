using Newtonsoft.Json;
using BDLearn.Models;

namespace BDLearn.response
{
    public class BooksRes
    {
        public HttpClient _client;

        public BooksRes() { _client = new HttpClient(); }

        public async Task<BooksApiResponse?> GetBooks(string url)
        {
            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<BooksApiResponse>(jsonResponse);
                }
            }
            catch { }
            return null;
        }
    }
}
