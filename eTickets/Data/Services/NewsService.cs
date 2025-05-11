using eTickets.Data;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class NewsService : INewsService
    {
        private readonly AppDbContext _context;

        public NewsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetAllAsync()
        {
            var result = await _context.News.OrderByDescending(n => n.Date).ToListAsync();
            return result;
        }

        public async Task<News> GetByIdAsync(int id)
        {
            var result = await _context.News.FirstOrDefaultAsync(n => n.Id == id);
            return result;
        }

        public async Task AddAsync(News news)
        {
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();
        }

        public async Task<News> UpdateAsync(int id, News news)
        {
            _context.Update(news);
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.News.FirstOrDefaultAsync(n => n.Id == id);
            _context.News.Remove(result);
            await _context.SaveChangesAsync();
        }
    }
} 