using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RandomGameDesigner.Models;
using RandomGameDesigner.Models.RequestModels;
using System.Net.Http.Headers;

namespace RandomGameDesigner
{

   
    public class DataAccess
    {
        private const string URL_API = "http://api.steampowered.com/{0}";
        private const string URL_STORE = "https://store.steampowered.com/api/appdetails";
        private static readonly string STEAM_KEY = Environment.GetEnvironmentVariable("STEAM_API_KEY") ?? string.Empty;
        private HttpClient _httpClientStore;
        private HttpClient _httpClientAPIGames;
        private HttpClient _httpClientAPIUser;
        private HttpClient _httpClientAPIUserV2;


        public enum AccountState
        {
            NotFound = 0,
            Private = 1,
            Public = 3
        }

        public DataAccess()
        {
            _httpClientStore = SetClient(URL_STORE);
            _httpClientAPIGames = SetClient(String.Format(URL_API, "IPlayerService/GetOwnedGames/v0001/"));
            _httpClientAPIUser = SetClient(String.Format(URL_API, "ISteamUser/ResolveVanityURL/v0001/"));
            _httpClientAPIUserV2 = SetClient(String.Format(URL_API, "ISteamUser/GetPlayerSummaries/v0002/"));
        }

        private HttpClient SetClient(string url)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
        public void Close()
        {
            _httpClientStore.Dispose();
            _httpClientAPIGames.Dispose();
            _httpClientAPIUser.Dispose();
            _httpClientAPIUserV2.Dispose();
        }

        public Game GetGameInfo(string gameId)
        {
            
            JObject response = makeRequestAsync(String.Format("?appids={0}",gameId), _httpClientStore).Result;
            if (response == null)
            {
                return new Game() { };
            }
            bool success = ((bool)response[gameId]["success"]);
            if (success)
            {
                int number1 = 0;

                var name = response[gameId]["data"]["name"].ToString();
                var image = response[gameId]["data"]["header_image"].ToString();
                var tags = response[gameId]["data"]["categories"].Values();
                var genres = response[gameId]["data"]["genres"].Values();
                Game game = new Game() { };
                try
                {
                    game.SetProperties(gameId, name, image);
                    game.SetGenres(genres);
                    game.SetTags(tags);
                }
                catch (Exception)
                {

                    return new Game() { };
                }
                
                return game;
            }
            return new Game() { };
            
        }

        public List<gameid> GetUserGamesIDs(string userSteamId)
        {

            JObject response = makeRequestAsync(String.Format("?key={0}&steamid={1}&format=json", STEAM_KEY,userSteamId), _httpClientAPIGames).Result;
            if (response == null) return null;
            try
            {
                var name = response["response"]["games"].ToString();
                List<gameid> steps = JsonConvert.DeserializeObject<List<gameid>>(name);
                return steps;
            }
            catch (Exception)
            {

                return null;
            }
           

        }

        public AccountState GetAccountState(string userSteamId)
        {
            JObject response = makeRequestAsync(String.Format("?key={0}&steamids={1}", STEAM_KEY, userSteamId), _httpClientAPIUserV2).Result;
            if (response == null) return AccountState.NotFound;
            if (!response["response"]["players"].HasValues) return AccountState.NotFound;
            var name = response["response"]["players"][0]["communityvisibilitystate"].ToString();
            if (name == "1" || name == "2")
            {
                return AccountState.Private;
            }else if(name == "3")
            {
                return AccountState.Public;
            }
            return AccountState.NotFound; 
        }


        public string GetUserId(string name)
        {
            JObject response = makeRequestAsync(String.Format("?key={0}&vanityurl={1}", STEAM_KEY, name), _httpClientAPIUser).Result;
            if (response == null) return null;
            var statusId = ((int)response["response"]["success"]);
            if (statusId == 1) {
                return ((string?)response["response"]["steamid"]);
            }
            return null;
        }


        public async Task<JObject> makeRequestAsync(string urlParameters, HttpClient client)
        {


            // List data response.
            HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                var sds = response.Content.ToString();
                // Parse the response body.
                var jsonString = await response.Content.ReadAsStringAsync();  //Make sure to add a reference to System.Net.Http.Formatting.dll
                return (JObject)JsonConvert.DeserializeObject<object>(jsonString);
                //foreach (var d in dataObjects)
                //{
                //    Console.WriteLine("{0}", d);
                //}
            }
            else
            {
                return null;
            }

            // Make any other calls using HttpClient here.

            // Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
        }
    }
}
