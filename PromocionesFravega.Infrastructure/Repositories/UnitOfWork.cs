using PromocionesFravega.Core.Entities;
using PromocionesFravega.Core.Interfaces;
using PromocionesFravega.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using PromocionesFravega.Infrastructure.Repositories;

namespace PromocionesFravega.Infrastructure.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly MLControlStockContext _context;
        private readonly IStockRepository _stockRepository;
       

        public UnitOfWork(MLControlStockContext context)
        {
            _context = context;
        }

        public IStockRepository StockRepository => _stockRepository ?? new StockRepository(_context);
        
        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
