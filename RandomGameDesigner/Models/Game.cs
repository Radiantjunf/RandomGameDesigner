using Newtonsoft.Json.Linq;

namespace RandomGameDesigner.Models
{
    public class Game
    {

        public string GameKey { get; set; }
        public string Title { get; set; }
        public string Tags { get; set; }
        public string Genres { get; set; }
        public string ImageLink { get; set; }

        
        public void SetProperties(string gameKey,string title,string imageLink)
        {
            Genres = gameKey;
            Title = title;
            if (!String.IsNullOrEmpty(imageLink)){
                ImageLink = imageLink;
            }
            else
            {
                ImageLink = "";
            }
        }

        public void SetTags(IJEnumerable<JToken> tags)
        {
            Tags = JTokenListToString(tags);
        }

        public void SetGenres(IJEnumerable<JToken> genres)
        {
            Genres = JTokenListToString(genres);
        }

        private string JTokenListToString(IJEnumerable<JToken> list)
        {
            if (list == null) return null;
            string Temp = string.Empty;
            foreach (JToken item in list)
            {
                var t = ((string?)item);
                int number = 0;
                if (!int.TryParse(t, out number))
                {
                    if (string.IsNullOrEmpty(Temp))
                    {
                        Temp = t;
                    }
                    else
                    {
                        Temp = Temp + "," + t;
                    }
                }
            }
            return Temp;
        }


    }
}
