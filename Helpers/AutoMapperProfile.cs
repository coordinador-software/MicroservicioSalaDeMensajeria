using AutoMapper;
using ChatAPI.Models.DB;
using ChatAPI.Models.DTO;

namespace ChatAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<MENSAJES, MENSAJES_DTO>().ReverseMap();

            CreateMap<MENSAJES_HISTORICOS, MENSAJES_DTO>().ReverseMap();

            CreateMap<SISTEMAS, SISTEMAS_DTO>().ReverseMap();
        }
    }
}
