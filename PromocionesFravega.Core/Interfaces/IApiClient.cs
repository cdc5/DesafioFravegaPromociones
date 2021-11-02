using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PromocionesFravega.Core.ApiClient.Dtos;


namespace PromocionesFravega.Core.Interfaces
{
    public interface IApiClient
    {
        Task<ItemResponse> Items(string Producto);
    }
}
