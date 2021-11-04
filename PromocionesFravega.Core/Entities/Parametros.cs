using PromocionesFravega.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace PromocionesFravega.Core.Entities
{
    public static class Parametros
    {
        public static List<string> MediosDePago { set; get; }
        public static List<string> Bancos { set; get; }
        public static List<string> CategoriasProductos { set; get; }

        static Parametros()
        {
            AgregarMediosDePago();
            AgregarBancos();
            AgregarCategoriasProductos();
        }
        private static void AgregarMediosDePago()
        {
            MediosDePago = new List<string>();
            MediosDePago.Add("TARJETA_CREDITO");
            MediosDePago.Add("TARJETA_DEBITO");
            MediosDePago.Add("EFECTIVO");
            MediosDePago.Add("GIFT_CARD");
        }

        public static bool ExisteMedioDePago(string MedioDePago)
        {
            if (!MediosDePago.Contains(MedioDePago))
                throw new BusinessException(String.Format("No existe el medio de Pago {0}", MedioDePago));
            return true;

        }

        private static void AgregarBancos()
        {
            Bancos = new List<string>();
            Bancos.Add("Galicia");
            Bancos.Add("Santander Rio");
            Bancos.Add("Ciudad");
            Bancos.Add("Nacion");
            Bancos.Add("ICBC");
            Bancos.Add("BBVA");
            Bancos.Add("Macro");
        }

        public static bool ExisteBanco(string Banco)
        {
            if (!Bancos.Contains(Banco))
                throw new BusinessException(String.Format("No existe el Banco {0}", Banco));
            return true;

        }

        private static void AgregarCategoriasProductos()
        {
            CategoriasProductos = new List<string>();
            CategoriasProductos.Add("Hogar");
            CategoriasProductos.Add("Jardin");
            CategoriasProductos.Add("ElectroCocina");
            CategoriasProductos.Add("GrandesElectro");
            CategoriasProductos.Add("Colchones");
            CategoriasProductos.Add("Celulares");
            CategoriasProductos.Add("Tecnologia");
            CategoriasProductos.Add("Audio");
        }

        public static bool ExisteCategoriaProducto(string CategoriaProducto)
        {
            if (!CategoriasProductos.Contains(CategoriaProducto))
                throw new BusinessException(String.Format("No existe la categoria de producto {0}", CategoriaProducto));
            return true;

        }


    }
}
