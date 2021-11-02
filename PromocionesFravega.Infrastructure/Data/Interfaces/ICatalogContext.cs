using PromocionesFravega.Core.Entities;
using MongoDB.Driver;

namespace PromocionesFravega.Infrastructure.Data.Interfaces
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}
