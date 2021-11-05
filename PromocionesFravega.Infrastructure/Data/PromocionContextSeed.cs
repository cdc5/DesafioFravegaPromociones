using PromocionesFravega.Core.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace PromocionesFravega.Infrastructure.Data
{
    public class PromocionContextSeed
    {
        public static void SeedData(IMongoCollection<Promocion> promocionCollection)
        {
            bool existPromo = promocionCollection.Find(p => true).Any();
            if (!existPromo)
            {
                promocionCollection.InsertManyAsync(GetPromciones());
            }
        }

        private static IEnumerable<Promocion> GetPromciones()
        {
            return new List<Promocion>()
            {
                
            };
        }
    }
}
