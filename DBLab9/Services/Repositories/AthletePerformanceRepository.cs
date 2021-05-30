using System;
using System.Collections.Generic;
using System.Linq;
using DBLab9.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace DBLab9.Services.Repositories
{
    public class AthletePerformanceRepository
    {
        private readonly Context _context;

        public AthletePerformanceRepository(Context context)
        {
            _context = context;
        }

        public void Add(AthletePerformance athletePerformance)
        {
            _context.AthletePerformances.Add(athletePerformance);
            _context.SaveChanges();
        }
        
        public AthletePerformance Get(int id)
        {
            return _context.AthletePerformances.SingleOrDefault(item => item.Id == id);
        }

        public List<AthletePerformance> Get(Func<AthletePerformance, bool> predicate)
        {
            return _context.AthletePerformances.Where(predicate).ToList();
        }
        
        public List<AthletePerformance> GetAll()
        {
            return _context.AthletePerformances.ToList();
        }

        public void Update(AthletePerformance athletePerformance)
        {
            _context.AthletePerformances.Update(athletePerformance);
            _context.Entry(athletePerformance).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.AthletePerformances.Remove(new AthletePerformance {Id = id});
            _context.SaveChanges();
        }
    }
}