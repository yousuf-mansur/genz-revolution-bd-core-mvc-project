using GenZRevolutionBD.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GenZRevolutionBD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SuperHeroDbContext _db;

        public HomeController(ILogger<HomeController> logger, SuperHeroDbContext db)
        {
            _logger = logger;
            _db = db;
        }


        public IActionResult Index()
        {
            var data = _db.SuperHeroes.ToList();
            ViewBag.MinAge = data.Min(s => s.Age);
            ViewBag.MaxAge = data.Max(s => s.Age);
            ViewBag.SumAge = data.Sum(s => s.Age);
            ViewBag.AvgAge = data.Average(s => s.Age);
            ViewBag.Count = data.Count();

            return View(data);

        }



    }
}
