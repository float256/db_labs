using System.Linq;
using DBLab9.Models.Domains;
using DBLab9.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DBLab9.Controllers
{
    public class SportsComplexController : Controller
    {
        private readonly SportsComplexRepository _sportsComplexRepository;

        public SportsComplexController(SportsComplexRepository sportsComplexRepository)
        {
            _sportsComplexRepository = sportsComplexRepository;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View(_sportsComplexRepository.GetAll());
        }
        
        [HttpGet]
        public IActionResult AddPage()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Add(SportsComplex athlete)
        {
            _sportsComplexRepository.Add(athlete);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _sportsComplexRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}