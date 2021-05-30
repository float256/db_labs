using System.Collections.Generic;
using System.Linq;
using DBLab9.Models.Domains;
using DBLab9.Models.Dto;
using DBLab9.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DBLab9.Controllers
{
    public class AthleteController : Controller
    {
        private readonly AthleteRepository _athleteRepository;
        private readonly SportRepository _sportRepository;

        public AthleteController(AthleteRepository athleteRepository, SportRepository sportRepository)
        {
            _athleteRepository = athleteRepository;
            _sportRepository = sportRepository;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            List<Athlete> allAthletes = _athleteRepository.GetAll();
            List<Sport> allSports = _sportRepository.GetAll();
            List<AthleteDto> result = (from athlete in allAthletes
                join sport in allSports on athlete.SportId equals sport.Id
                select new AthleteDto
                {
                    Id = athlete.Id,
                    Name = athlete.Name,
                    Surname = athlete.Surname,
                    PatronymicName = athlete.PatronymicName,
                    SportName = sport.Name
                }).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult AddPage()
        {
            return View(_sportRepository.GetAll().ToList());
        }
        
        [HttpPost]
        public IActionResult Add(Athlete athlete)
        {
            _athleteRepository.Add(athlete);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _athleteRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}