using Newtonsoft.Json;
using RealEstateApp.Models;
using System.Net.Http.Headers;

namespace RealEstateApp.Services
{
    public class ApiService
    {

        public async Task<bool> RegisterUserAsync(string name, string email, string password,string phone) 
        {
            var register = new Register()
            {
                Name = name,
                Email = email,
                Password = password,
                Phone = phone
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(register);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/users/register", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
             
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var login = new Login()
            {
                Email = username,
                Password = password
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/users/login", content);
            if (!response.IsSuccessStatusCode) return false;
            var jsoResults = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Token>(jsoResults);
            Preferences.Set("accesstoken", result.AccessToken);
            Preferences.Set("userid", result.UserId);
            Preferences.Set("username", result.UserName);
            return true;
        }

        public static async Task<List<Categories>> GetCategories()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/categories"); 
            return JsonConvert.DeserializeObject<List<Categories>>(response);  
        }

        public static async Task<List<TrendingProperty>> GetTrendingProperties()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/TrendingProperties");
            return JsonConvert.DeserializeObject<List<TrendingProperty>>(response);
        }

        public static async Task<List<SearchProperty>> FindProperties(string address)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/SearchProperties?address=" + address);
            return JsonConvert.DeserializeObject<List<SearchProperty>>(response);
        }

        public static async Task<List<PropertyByCategory>> GetPropertyByCategory(int categoryId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/PropertyList?categoryId=" + categoryId);
            return JsonConvert.DeserializeObject<List<PropertyByCategory>>(response);
        }

        public static async Task<PropertyDetails> GetPropertyByDetails(int propertyId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/Properties/PropertyDetail?id=" + propertyId);
            return JsonConvert.DeserializeObject<PropertyDetails>(response);
        }

        public static async Task<List<BookMarkList>> GetBookMarkList()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.GetStringAsync(AppSettings.ApiUrl + "api/bookmarks");
            return JsonConvert.DeserializeObject <List<BookMarkList>>(response);
        }

        public static async Task<bool> AddBookMark(AddBookMark addBookmark)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(addBookmark);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.PostAsync(AppSettings.ApiUrl + "api/bookmarks", content);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

        public static async Task<bool> DeleteBookMark(int bookmarkId)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accesstoken", string.Empty));
            var response = await httpClient.DeleteAsync(AppSettings.ApiUrl + "api/bookmarks/" + bookmarkId);
            if (!response.IsSuccessStatusCode) return false;
            return true;
        }

    }
}
