using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using eTickets.Data;
using System.Linq;

namespace eTickets.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _service;
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NewsController(INewsService service, AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _service = service;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var allNews = await _service.GetAllAsync();
            return View(allNews);
        }

        public async Task<IActionResult> Details(int id)
        {
            var newsDetails = await _service.GetByIdAsync(id);
            if (newsDetails == null) return View("NotFound");

            // Получаем Id всех пользователей из комментариев
            var userIds = newsDetails.Comments.Select(c => c.UserId).Distinct().ToList();
            var users = await _context.Users
                .Where(u => userIds.Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.FullName);
            ViewBag.UserNames = users;

            return View(newsDetails);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Title,Description,ImageURL,Date")] News news)
        {
            if (!ModelState.IsValid) return View(news);

            await _service.AddAsync(news);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var newsDetails = await _service.GetByIdAsync(id);
            if (newsDetails == null) return View("NotFound");
            return View(newsDetails);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,ImageURL,Date")] News news)
        {
            if (!ModelState.IsValid) return View(news);

            await _service.UpdateAsync(id, news);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var newsDetails = await _service.GetByIdAsync(id);
            if (newsDetails == null) return View("NotFound");
            return View(newsDetails);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var newsDetails = await _service.GetByIdAsync(id);
            if (newsDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
} 