using APIModels.Models;
using AutoMapper;

namespace APICommons
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<FilmModel, FilmViewResponseModel>();
            CreateMap<PlanetsModel, PlanetsViewResponseModel>();
        }
    }
}
