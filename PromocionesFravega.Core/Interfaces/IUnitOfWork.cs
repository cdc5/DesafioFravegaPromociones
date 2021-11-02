using PromocionesFravega.Core.Entities;
using System;
using System.Threading.Tasks;

namespace PromocionesFravega.Core.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IStockRepository StockRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
