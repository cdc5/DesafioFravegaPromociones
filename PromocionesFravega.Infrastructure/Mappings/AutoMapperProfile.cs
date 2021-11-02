using AutoMapper;
using PromocionesFravega.Core.DTOs;
using PromocionesFravega.Core.Entities;

namespace PromocionesFravega.Infrastructure.Mappings
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Stock, StockDto>();
            CreateMap<StockPorProducto, StockPorProductoDto>();
        }
    }
}
