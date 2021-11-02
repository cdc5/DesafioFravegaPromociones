using AutoMapper;
using PromocionesFravega.Core.ApiClient.Dtos;
using PromocionesFravega.Core.DTOs;
using PromocionesFravega.Core.Entities;
using PromocionesFravega.Core.Exceptions;
using PromocionesFravega.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PromocionesFravega.Core.Services
{
    public class StockService: IStockService
    {
        private IUnitOfWork _UnitOfWork;
        private IApiClient _ApiClient;
        private readonly IMapper _Mapper;

        public StockService(IUnitOfWork unitOfWork, IMapper mapper, IApiClient ApiClient)
        {
            _UnitOfWork = unitOfWork;
            _ApiClient = ApiClient;
            _Mapper = mapper;    
        }

        public IEnumerable<Stock> GetStock(string deposito, Ubicacion ubicacion)
        {
            var stock = _UnitOfWork.StockRepository.Get(x => x.Deposito == deposito && x.Area == ubicacion.Area && x.Pasillo == ubicacion.Pasillo
                                                        && x.Fila == ubicacion.Fila && x.Cara == ubicacion.Cara);
            return stock;
        }

        public IEnumerable<StockDto> GetStock(string deposito, string ubicacion)
        {
            ValidarUbicacion(ubicacion);
            Ubicacion ubi = new Ubicacion(ubicacion);
            var stock = GetStock(deposito, ubi);
            if (stock == null)
                throw new BusinessException(String.Format("No se encuentran productos en la ubicación {0}-{1}", deposito, ubicacion));
            var stockDto = _Mapper.Map<IEnumerable<StockDto>>(stock);
            return stockDto;
        }       

        public IEnumerable<StockPorProductoDto> GetStockPorProducto(string deposito, string producto)
        {
            StockPorProductoDto stockPorProducto = new StockPorProductoDto();
            List <StockPorProductoDto> ListaStock = new List<StockPorProductoDto>();
            var stock = _UnitOfWork.StockRepository.Get(x => x.Deposito == deposito && x.ProductId == producto);
            if (stock == null)
                throw new BusinessException(String.Format("No se encuentra el producto {0} en el depósito {1}", producto, deposito));

            
            foreach (var s in stock)
            {
                stockPorProducto = new StockPorProductoDto();
                stockPorProducto.deposito = s.Deposito;
                stockPorProducto.ubicacion = UbicacionAString(s.Area,s.Pasillo,s.Fila,s.Cara);
                stockPorProducto.cantidad = s.Cantidad;
                ListaStock.Add(stockPorProducto);
            }            
            return ListaStock;
        }

        public async Task<IEnumerable<StockPorProductoDto>> GetStockPorProductoSP(string deposito, string producto)
        {
            StockPorProductoDto stockPorProducto = new StockPorProductoDto();
            IEnumerable<StockPorProductoDto> ListaStock = new List<StockPorProductoDto>();
            var stock = await _UnitOfWork.StockRepository.GetStockPorProductoSP(deposito,producto);
            if (stock == null)
                throw new BusinessException(String.Format("No se encuentra el producto {0} en el depósito {1}", producto, deposito));
            ListaStock = _Mapper.Map<IEnumerable<StockPorProductoDto>>(stock);
            return ListaStock;
        }

        private string UbicacionAString(string Area, string Pasillo, string Fila, string Cara)
        {
            return string.Format("{0}-{1}-{2}-{3}", Area, Pasillo, Fila, Cara);
        }

        public Stock GetStockPorProductoUbicacion(string deposito, string ubicacion, string producto)
        {
            ValidarUbicacion(ubicacion);
            Ubicacion ubi = new Ubicacion(ubicacion);
            var stock = _UnitOfWork.StockRepository.Get(x => x.Deposito == deposito && x.Area == ubi.Area && x.Pasillo == ubi.Pasillo
                                                       && x.Fila == ubi.Fila && x.Cara == ubi.Cara && x.ProductId == producto);
            if (stock == null || stock.Count() == 0)
                throw new BusinessException(String.Format("No se encuentra el producto {0} en la ubicación:{1}-{2}", producto, deposito,ubicacion ));

            if (stock.Count() > 1)
                throw new BusinessException(String.Format("El producto {0} existe {1} veces en la ubicación:{2}-{3}", producto, stock.Count(),deposito, ubicacion));

            return stock.FirstOrDefault();
        }

        public async Task<Stock> AgregarProducto(string deposito, string ubicacion,string producto,int cantidad)
        {
            Stock stock;
            ValidarUbicacion(ubicacion);
            await EstaEnDeposito(producto);
            Ubicacion ubi = new Ubicacion(ubicacion);
            var stocks = GetStock(deposito, ubi);
            if (stocks != null)
            {
                ValidarLugarDisponibleEnUbicacion(stocks, ubicacion, producto,cantidad);            
                stock = stocks.Where(x => x.ProductId == producto).FirstOrDefault();
                if (stock == null)
                {
                    stock = NuevoStock(deposito, ubi, producto, cantidad);
                    await _UnitOfWork.StockRepository.Insert(stock);
                }
                else
                {
                    stock.Cantidad += cantidad;
                     _UnitOfWork.StockRepository.Update(stock);
                }                    
            }
            else
            {
                stock = NuevoStock(deposito, ubi, producto, cantidad);
                await _UnitOfWork.StockRepository.Insert(stock);
            }            
           
            await _UnitOfWork.SaveChangesAsync();
            return stock;
        }

        private Stock NuevoStock(string deposito, Ubicacion ubicacion, string producto, int cantidad)
        {
            var stock = new Stock();
            stock.Deposito = deposito;
            stock.Area = ubicacion.Area;
            stock.Pasillo = ubicacion.Pasillo;
            stock.Fila = ubicacion.Fila;
            stock.Cara = ubicacion.Cara;
            stock.ProductId = producto;
            stock.Cantidad = cantidad;
            return stock;
        }

        private void ValidarUbicacion(string ubicacion)
        {
            Regex ValidacionUbicacion = new Regex(@"\A[A-Z][A-Z]-[0-9][0-9]-[0-9][0-9]-(IZ|DE)\Z");
            if (!ValidacionUbicacion.IsMatch(ubicacion))
               throw new BusinessException(String.Format("La ubicación:{0} no cumple con el formato requerido {{Area}}-{{Pasillo}}-{{Fila}}-{{Cara}}", ubicacion));
        }

        private void ValidarLugarDisponibleEnUbicacion(IEnumerable<Stock> stock,string ubicacion, string producto,int cantidad)
        {
            //Se podría cambiar por una función que llame a un Stored Procedure que traiga la cantidad de items en la ubicación directamente 
            //o que devuelva si hay lugar disponible
            if (stock != null)
            {
                List<string> productos = stock.Select(x => x.ProductId).ToList();
                if (!productos.Contains(producto))
                {
                    if (stock.Count() > 2)
                        throw new BusinessException(String.Format("Existen más de tres productos en la ubicación: {0}", ubicacion));
                }               

                if (stock.Sum(x=>x.Cantidad) + cantidad > 100)
                    throw new BusinessException(String.Format("Existen más de cien productos en la ubicación: {0}", ubicacion));
            }
            //Se podría dividir este método para cumplir criterios de unicidad en función en dos, por ejemplo LugarDisponibleEnUbicacion y HayMenosDeCientProductos 
            //pero para hacer una sola llamada a la base se realizan bajo el mismo ámbito.                       
        }       


        private async Task EstaEnDeposito(string producto)
        {
            ItemResponse item = await _ApiClient.Items(producto);
            if (item != null)
                if (item.shipping.logistic_type != "fulfillment")
                    throw new BusinessException("El producto no se encuentra en nuestros depósitos (no es fulfillment).");            
        }

        public async Task<Stock> RetirarProducto(string deposito, string ubicacion, string producto, int cantidad)
        {
            var stock = GetStockPorProductoUbicacion(deposito,ubicacion,producto);
            
            if (cantidad < 0)
                throw new BusinessException(String.Format("La cantidad ({0}) a retirar debe ser un número positivo", cantidad));

            if (cantidad > stock.Cantidad)
                throw new BusinessException(String.Format("La cantidad ({0}) a retirar es mayor al stock disponible",cantidad));
                        
            if (cantidad < stock.Cantidad)
            {
                stock.Cantidad -= cantidad;
                _UnitOfWork.StockRepository.Update(stock);
            }
            else
            {
                stock.Cantidad -= cantidad;
                await _UnitOfWork.StockRepository.Delete(stock);
            }            
            await _UnitOfWork.SaveChangesAsync();
            return stock;
        }
    }
}
