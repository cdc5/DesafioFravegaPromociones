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
            CreateMap<Promocion, PromocionVigenteDto>();
            //.ForMember(dest => dest.Bancos, opt => opt.MapFrom(so => so.TurnosRenspas.Select(t => t.RenspasId).ToList()))
        }
    }
}
