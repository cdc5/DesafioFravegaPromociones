using PromocionesFravega.Infrastructure.Data;
using PromocionesFravega.Core.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using PromocionesFravega.Infrastructure.Data.Interfaces;

namespace PromocionesFravega.Infrastructure.Data
{
    public class PromocionContext : IPromocionContext
    {
        public IMongoCollection<Promocion> Promociones { get; }
        public PromocionContext(IConfiguration configuration)
        {            
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Promociones = database.GetCollection<Promocion>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            //PromocionContextSeed.SeedData(Promociones);
        }        
    }
}
