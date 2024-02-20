namespace APICommons
{
    public class APIConstants
    {
        public static string FilmApiPath = "films/";
        public static string StarShipApiPath = "starships/";
        public static string PlanetsApiPath = "planets/";
        public static string StarWarServiceBaseUrl = "https://swapi.dev/api/";

        public static string FilmUrl = $"{StarWarServiceBaseUrl}{FilmApiPath}";
        public static string PlanetsUrl = $"{StarWarServiceBaseUrl}{PlanetsApiPath}";
        public static string StarShiplUrl = $"{StarWarServiceBaseUrl}{StarShipApiPath}";

    }
}
