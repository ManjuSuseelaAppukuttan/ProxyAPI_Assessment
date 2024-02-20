using APIModels.Models;

namespace APICommons
{
    public static class FiledMapper
    {
        /// <summary>
        /// Extension method to convert FilmModel to FilmViewModel
        /// </summary>
        /// <param name="film">FilmModel</param>
        /// <returns>FilmViewModel</returns>
        public static FilmViewResponseModel Map(this FilmModel film)
        {
            return new FilmViewResponseModel
            {
                Id = int.TryParse(film.Url?.Replace(APIConstants.FilmUrl, "").TrimEnd('/'), out int id) ? id : 0,
                Title = film.Title,
                Episode_id = film.Episode_id,
                Opening_crawl = film.Opening_crawl,
                Director = film.Director,
                Producer = film.Producer,
                //Rlease_date = DateTime.TryParseExact(film.Rlease_date, "dd/MM/yyyy HH:mm:ss", null, System.Globalization.DateTimeStyles.None, out DateTime dateTime) ? dateTime : null,

                Starships = film.Starships.Select(url =>
                {
                    string? modifiedUrl = url?.Replace(APIConstants.StarShiplUrl, "").TrimEnd('/');
                    return int.TryParse(modifiedUrl, out int itemId) ? itemId : 0;
                }).ToArray()
            };
        }

        /// <summary>
        /// Extension method to convert StarshipModel to StarShipViewModel
        /// </summary>
        /// <param name="starship">StarshipModel</param>
        /// <returns>StarShipViewModel</returns>
        public static PlanetsViewResponseModel Map(this PlanetsModel planets)
        {
            return new PlanetsViewResponseModel
            {
                Name = planets.Name,
                Climate = planets.Climate,
                Diameter = planets.Diameter,
                Gravity = planets.Gravity,
                Films = planets.Films.Select(url =>
                {
                    string modifiedUrl = url?.Replace(APIConstants.FilmApiPath, "").TrimEnd('/');
                    return modifiedUrl;
                }).ToList(),
            };
        }
    }       
     
}