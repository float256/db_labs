using System.Collections.Generic;
using System.Linq;
using DBLab9.Models.Domains;
using DBLab9.Models.Dto;
using DBLab9.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DBLab9.Controllers
{
    public class CompetitionController : Controller
    {
        private readonly CompetitionRepository _competitionRepository;
        private readonly SportsComplexRepository _sportsComplexRepository;
        
        public CompetitionController(
            CompetitionRepository competitionRepository,
            SportsComplexRepository sportsComplexRepository)
        {
            _competitionRepository = competitionRepository;
            _sportsComplexRepository = sportsComplexRepository;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            List<Competition> allCompetitions = _competitionRepository.GetAll();
            List<SportsComplex> allSportsComplexes = _sportsComplexRepository.GetAll();
            List<CompetitionDto> result = (from competition in allCompetitions
                join sportsComplex in allSportsComplexes on competition.SportsComplexId equals sportsComplex.Id
                select new CompetitionDto
                {
                    Id = competition.Id,
                    Name = competition.Name,
                    StartDate = competition.StartDate,
                    EndDate = competition.EndDate,
                    SportsComplexName = sportsComplex.Name
                }).ToList();
            return View(result);
        }
        
        [HttpGet]
        public IActionResult AddPage()
        {
            return View(_sportsComplexRepository.GetAll());
        }

        [HttpPost]
        public IActionResult Add(Competition competition)
        {
            _competitionRepository.Add(competition);
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _competitionRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}