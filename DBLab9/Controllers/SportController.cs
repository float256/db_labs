using System.Linq;
using DBLab9.Models.Domains;
using DBLab9.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DBLab9.Controllers
{
    public class SportController : Controller
    {
        private readonly SportRepository _sportRepository;

        public SportController(SportRepository sportRepository)
        {
            _sportRepository = sportRepository;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View(_sportRepository.GetAll());
        }
        
        [HttpGet]
        public IActionResult AddPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Sport sport)
        {
            _sportRepository.Add(sport);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult UpdatePage(int id)
        {
            return View(_sportRepository.Get(id));
        }

        [HttpPost]
        public IActionResult Update(Sport sport)
        {
            _sportRepository.Update(sport);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _sportRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}