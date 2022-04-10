namespace RandomGameDesigner
{
    public class IdeaGenerator
    {
        public bool IsVanity(string name)
        {
            long result;
            return !Int64.TryParse(name, out result);
        }

        public bool IsUrl(string url)
        {
            return url.Contains("https://");
        }

        public string ClearUrl(string url)
        {
            if (url.Contains("https://steamcommunity.com/id/"))
            {
                return url.Replace("https://steamcommunity.com/id/","").Replace("/","");
            }
            else if (url.Contains("https://steamcommunity.com/profiles/"))
            {
                return url.Replace("https://steamcommunity.com/profiles/", "").Replace("/", "");
            }
            return url;
        }

        public bool IsValidUrl(string url)
        {
            if (url.Contains("https://steamcommunity.com/id/") && (url.Length > "https://steamcommunity.com/id/".Length))
            {
                return true;
            }
            else if (url.Contains("https://steamcommunity.com/profiles/") && url.Length > "https://steamcommunity.com/profiles/".Length)
            {
                return true;
            }
            return false;
        }
    }
}