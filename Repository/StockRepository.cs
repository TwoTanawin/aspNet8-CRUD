using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDBContext _context;

        // Constructor only needs the ApplicationDBContext
        public StockRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stock.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(stockModel == null){
                return null;
            }

            _context.Stock.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stock.ToListAsync();
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stock.FindAsync(id);
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto stockDto)
        {
            var exisitingStock = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            if(exisitingStock == null){
                return null;
            }

            exisitingStock.Symbol = exisitingStock.Symbol;
            exisitingStock.CompanyName = exisitingStock.CompanyName;
            exisitingStock.Purchase = exisitingStock.Purchase;
            exisitingStock.LastDiv = exisitingStock.LastDiv;
            exisitingStock.Industry = exisitingStock.Industry;
            exisitingStock.MarketCap = exisitingStock.MarketCap;

            await _context.SaveChangesAsync();

            return exisitingStock;
        }
    }
}
