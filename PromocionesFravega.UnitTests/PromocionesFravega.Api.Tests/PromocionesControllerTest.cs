using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using PromocionesFravega.Core.Services;
using PromocionesFravega.Core.Interfaces;
using PromocionesFravega.API.Controllers;
using PromocionesFravega.Infrastructure.Mappings;

namespace PromocionesFravega.UnitTests.PromocionesFravega.Api.Tests
{
    public class PromocionesControllerTest
    {

        private readonly IMapper _mapper;
        private readonly IPromocionService _promocionService;
        private readonly PromocionController _controller;

        public PromocionesControllerTest()
        {
            //IUnitOfWork iow = new UnitOfWorkMock();
            var mapperconfig = new MapperConfiguration(cfg => {
                cfg.AddProfile<AutoMapperProfile>();
            });
            _mapper = new Mapper(mapperconfig);
            //_promocionService = new PromocionService();
            //_controller = new StockController(_stockService, _mapper);
        }


    }
}
