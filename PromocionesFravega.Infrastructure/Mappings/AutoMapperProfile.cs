using AutoMapper;
using PromocionesFravega.Core.DTOs;
using PromocionesFravega.Core.Entities;

namespace PromocionesFravega.Infrastructure.Mappings
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Promocion, PromocionDto>();
            CreateMap<PromocionDto, Promocion>();
            CreateMap<Promocion, PromocionUpdateDto>();
            CreateMap<PromocionUpdateDto, Promocion>();
            CreateMap<Promocion, PromocionVigenteDto>();
            CreateMap<PromocionVigenteDto, Promocion>();
            CreateMap<Promocion, PromocionVigenciaUpdateDto>();
            CreateMap<PromocionVigenciaUpdateDto, Promocion>();            
            
        }
    }
}
