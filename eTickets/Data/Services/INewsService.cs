using eTickets.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface INewsService
    {
        Task<IEnumerable<News>> GetAllAsync();
        Task<News> GetByIdAsync(int id);
        Task AddAsync(News news);
        Task<News> UpdateAsync(int id, News news);
        Task DeleteAsync(int id);
    }
} 