using WebApplication2.Models;

namespace WebApplication2.ApiEndPoints;

public static class Api
{
    public const string ApiBaseurl = "api";

    public static class Movies
    {
        private const string MovieBaseurl = $"{ApiBaseurl}/movies";
        public  const string Create = MovieBaseurl;
        public const string GetAll = MovieBaseurl ;
        public const string  Get = MovieBaseurl+$"/{{idOrSlug}}" ;
        public const string  Update = MovieBaseurl+$"/{{id}}" ;
        public const string  Delete = MovieBaseurl+$"/{{id}}" ;
    }
}