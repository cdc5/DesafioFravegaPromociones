using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PromocionesFravega.Core.DTOs;
using PromocionesFravega.Core.Entities;
using PromocionesFravega.Core.Interfaces;
using System.Linq;
using PromocionesFravega.Core.Exceptions;

namespace PromocionesFravega.Core.Services
{
    public class PromocionService: IPromocionService
    {
        private readonly IPromocionRepository _repository;
        private readonly IMapper _Mapper;

        public PromocionService(IPromocionRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<Promocion>> GetPromociones()
        {
            var promociones = await _repository.GetPromociones();
            return promociones;
        }

        public async Task<Promocion> GetPromocion(Guid id)
        {
            var promocion = await _repository.GetPromocion(id);
            return promocion;
        }

        public async Task<IEnumerable<Promocion>> GetPromocionesVigentes()
        {
            var hoy = DateTime.Now.Date;
            var promociones = await GetPromocionesVigentes(hoy);
            return promociones;
        }

        public async Task<IEnumerable<Promocion>> GetPromocionesVigentes(DateTime Fecha)
        {
            var promociones = await _repository.GetPromociones(x => x.FechaInicio <= Fecha && x.FechaFin >= Fecha && x.Activo == true);
            return promociones;
        }

        public async Task<IEnumerable<PromocionVigenteDto>> GetPromocionesVigentes(string medioDePago,string Banco,string categoriaProducto)
        {
            ValidarInput(medioDePago, Banco, categoriaProducto);
            List<PromocionVigenteDto> promocionesDto = new List<PromocionVigenteDto>();
            PromocionVigenteDto promoDto;
            var hoy = DateTime.Now.Date;
            var promociones = await _repository.GetPromocionesVigentes(medioDePago, Banco, categoriaProducto);
            foreach (var p in promociones)
            {
                promoDto = new PromocionVigenteDto();
                promoDto.Id = p.Id;
                promoDto.MedioDePago = medioDePago;
                promoDto.Banco = Banco;
                promoDto.CategoriaProducto = categoriaProducto;
                promoDto.MaximaCantidadDeCuotas = p.MaximaCantidadDeCuotas;
                promoDto.PorcentajeDeDescuento = p.PorcentajeDeDescuento;
                promoDto.ValorInteresCuotas = p.ValorInteresCuotas;
                promocionesDto.Add(promoDto);
            }
            return promocionesDto;
        }

        public async Task<Guid> CrearPromocion(PromocionDto promocion)
        {
            ValidarInputListas(promocion.MediosDePago, promocion.Bancos, promocion.CategoriasProductos);
            ValidarSolapamientoPromos(promocion.MediosDePago, promocion.Bancos, promocion.CategoriasProductos,
                                        promocion.FechaInicio.Value, promocion.FechaFin.Value);
            var promo = _Mapper.Map<Promocion>(promocion);
            promo.SetActivo(true);
            promo.SetFechaCreacion(DateTime.Now);            

            await _repository.InsertarPromocion(promo);
            return promo.Id;
        }

        public async Task<Guid> ActualizarPromocion(PromocionUpdateDto promocion)
        {
            ValidarInputListas(promocion.MediosDePago, promocion.Bancos, promocion.CategoriasProductos);
            ValidarSolapamientoPromos(promocion.MediosDePago, promocion.Bancos, promocion.CategoriasProductos,
                                        promocion.FechaInicio.Value, promocion.FechaFin.Value);
            var promo = _Mapper.Map<Promocion>(promocion);
            promo.SetFechaModificacion(DateTime.Now);
            await _repository.ActualizarPromocion(promo);
            return promo.Id;
        }

        public async Task<Guid> ActualizarPromocion(PromocionVigenciaUpdateDto promocionUpdDto)
        {
            var promocion = await _repository.GetPromocion(promocionUpdDto.Id);
            if (promocion == null)
                throw new BusinessException(String.Format("No se ha encontrado la promoción:{0}", promocionUpdDto.Id));
            
            if (ValidarSolapamientoPromos(promocion.MediosDePago, promocion.Bancos, promocion.CategoriasProductos,
                                      promocionUpdDto.FechaInicio.Value, promocionUpdDto.FechaFin.Value))
            {
                var promo = _Mapper.Map<Promocion>(promocion);
                promo.SetFechaModificacion(DateTime.Now);
                await _repository.ActualizarPromocion(promo);
                return promo.Id;
            }
            return default(Guid);
        }
        public async Task<Guid> EliminarPromocion(Guid id)
        {
            var promocion = await _repository.GetPromocion(id);
            if (promocion == null)
                throw new BusinessException(String.Format("No se ha encontrado la promoción:{0}", id));

            var promo = _Mapper.Map<Promocion>(promocion);
            promo.SetActivo(false);
            promo.SetFechaModificacion(DateTime.Now);
            await _repository.ActualizarPromocion(promo);
            return promo.Id;
        }

        private bool ValidarSolapamientoPromos(IEnumerable<string> MediosDePago, IEnumerable<string> Bancos, IEnumerable<string> Categorias, DateTime FechaInicio,DateTime FechaFin)
        {
            IEnumerable<Promocion> promos = _repository.GetPromocionesMediosDePago(MediosDePago, FechaInicio, FechaFin).Result;

            if (promos != null && promos.Count() > 0)
            {
                throw new BusinessException("Ya existe una promoción para las fechas y los medios de pagos seleccionados");                
            }

            promos = _repository.GetPromocionesBancos(Bancos, FechaInicio, FechaFin).Result;

            if (promos != null && promos.Count() > 0)
            {
                throw new BusinessException("Ya existe una promoción para las fechas y los bancos seleccionados");
            }

            promos = _repository.GetPromocionesCategorias(Categorias, FechaInicio, FechaFin).Result;

            if (promos != null && promos.Count() > 0)
            {
                throw new BusinessException("Ya existe una promoción para las fechas y las categorias de productos seleccionados");
            }
            return true;
        }

        

        private void ValidarMediosDePago(IEnumerable<string> MediosDePago)
        {
            foreach (var m in MediosDePago)
                Parametros.ExisteMedioDePago(m);
        }

        private void ValidarMedioDePago(string MedioDePago)
        {
            Parametros.ExisteMedioDePago(MedioDePago);
        }

        private void ValidarBancos(IEnumerable<string> Bancos)
        {
            foreach (var b in Bancos)
                Parametros.ExisteBanco(b);
        }

        private void ValidarBanco(string Banco)
        {
            Parametros.ExisteBanco(Banco);
        }

        private void ValidarCategoriasProductos(IEnumerable<string> CategoriasProductos)
        {
            foreach (var c in CategoriasProductos)
                Parametros.ExisteCategoriaProducto(c);
        }

        private void ValidarCategoriaProducto(string CategoriaProducto)
        {
            Parametros.ExisteCategoriaProducto(CategoriaProducto);
        }

        private void ValidarInputListas(IEnumerable<string> MediosDePago, IEnumerable<string> Bancos, IEnumerable<string> CategoriasProductos)
        {
            ValidarMediosDePago(MediosDePago);
            ValidarBancos(Bancos);
            ValidarCategoriasProductos(CategoriasProductos);
        }

        private void ValidarInput(string MediosDePago,string Bancos,string CategoriasProductos)
        {
            ValidarMedioDePago(MediosDePago);
            ValidarBanco(Bancos);
            ValidarCategoriaProducto(CategoriasProductos);
        }


    }
}
