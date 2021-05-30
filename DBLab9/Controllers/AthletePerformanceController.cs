using System;
using System.Collections.Generic;
using System.Linq;
using DBLab9.Models.Domains;
using DBLab9.Models.Dto;
using DBLab9.Services.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DBLab9.Controllers
{
    public class AthletePerformanceController : Controller
    {
        private readonly AthleteRepository _athleteRepository;
        private readonly AthletePerformanceRepository _athletePerformanceRepository;
        private readonly CompetitionRepository _competitionRepository;

        public AthletePerformanceController(
            AthleteRepository athleteRepository,
            AthletePerformanceRepository athletePerformanceRepository,
            CompetitionRepository competitionRepository)
        {
            _athleteRepository = athleteRepository;
            _athletePerformanceRepository = athletePerformanceRepository;
            _competitionRepository = competitionRepository;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            List<Athlete> allAthletes = _athleteRepository.GetAll();
            List<AthletePerformance> allPerformances = _athletePerformanceRepository.GetAll();
            List<Competition> allCompetitions = _competitionRepository.GetAll();
            List<AthletePerformanceDto> result = (from performance in allPerformances
                join competition in allCompetitions on performance.CompetitionId equals competition.Id
                join athlete in allAthletes on performance.AthleteId equals athlete.Id
                select new AthletePerformanceDto
                {
                    Id = performance.Id,
                    AthleteName = athlete.Name,
                    AthleteSurname = athlete.Surname,
                    AthletePatronymicName = athlete.PatronymicName,
                    CompetitionName = competition.Name,
                    PerformanceDate = performance.PerformanceDate,
                    Score = performance.Score
                }).ToList();
            return View(result);
        }

        [HttpGet]
        public IActionResult AddPage()
        {
            return View(new Tuple<List<Athlete>, List<Competition>>(
                    _athleteRepository.GetAll(),
                    _competitionRepository.GetAll()));
        }
        
        [HttpPost]
        public IActionResult Add(AthletePerformance athletePerformance)
        {
            _athletePerformanceRepository.Add(athletePerformance);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdatePage(int id)
        {
            return View(new Tuple<AthletePerformance, List<Athlete>, List<Competition>>(
                _athletePerformanceRepository.Get(id),
                _athleteRepository.GetAll(),
                _competitionRepository.GetAll()));
        }
        
        [HttpPost]
        public IActionResult Update(AthletePerformance athletePerformance)
        {
            _athletePerformanceRepository.Update(athletePerformance);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            _athletePerformanceRepository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}