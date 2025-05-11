using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _service;

        public NewsController(INewsService service)
        {
            _service = service;
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