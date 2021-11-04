using PromocionesFravega.Core.Entities;
using MongoDB.Driver;

namespace PromocionesFravega.Infrastructure.Data.Interfaces
{
    public interface IPromocionContext
    {
        IMongoCollection<Promocion> Promociones { get; }
    }
}
