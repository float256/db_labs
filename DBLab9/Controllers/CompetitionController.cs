using System;
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
        private readonly AthletePerformanceRepository _athletePerformanceRepository;
        
        public CompetitionController(
            CompetitionRepository competitionRepository,
            SportsComplexRepository sportsComplexRepository,
            AthletePerformanceRepository athletePerformanceRepository)
        {
            _competitionRepository = competitionRepository;
            _sportsComplexRepository = sportsComplexRepository;
            _athletePerformanceRepository = athletePerformanceRepository;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            List<Competition> allCompetitions = _competitionRepository.GetAll();
            List<SportsComplex> allSportsComplexes = _sportsComplexRepository.GetAll();
            Dictionary<int, int> numberOfParticipants = (
                from athlete in _athletePerformanceRepository.GetAll()
                group athlete by athlete.CompetitionId into athleteGroup
                select new {
                    CompetitionId = athleteGroup.Key,
                    NumberOfParticipants = athleteGroup.Count()
                }).ToDictionary(
                    pair => pair.CompetitionId, 
                    pair => pair.NumberOfParticipants);
            List<CompetitionDto> result = (from competition in allCompetitions
                join sportsComplex in allSportsComplexes on competition.SportsComplexId equals sportsComplex.Id
                select new CompetitionDto
                {
                    Id = competition.Id,
                    Name = competition.Name,
                    StartDate = competition.StartDate,
                    EndDate = competition.EndDate,
                    SportsComplexName = sportsComplex.Name,
                    NumberOfParticipants = numberOfParticipants.ContainsKey(competition.Id) 
                        ? numberOfParticipants[competition.Id]
                        : 0
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
        public IActionResult UpdatePage(int id)
        {
            return View(new Tuple<Competition, List<SportsComplex>>(
                _competitionRepository.Get(id),
                _sportsComplexRepository.GetAll()));
        }

        [HttpPost]
        public IActionResult Update(Competition competition)
        {
            _competitionRepository.Update(competition);
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