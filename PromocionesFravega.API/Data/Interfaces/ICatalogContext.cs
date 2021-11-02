using PromocionesFravega.API.Entities;
using MongoDB.Driver;

namespace PromocionesFravega.API.Data.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
