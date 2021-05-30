using System;
using System.Collections.Generic;
using System.Linq;
using DBLab9.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace DBLab9.Services.Repositories
{
    public class AthleteRepository
    {
        private readonly Context _context;

        public AthleteRepository(Context context)
        {
            _context = context;
        }
        
        public void Add(Athlete athlete)
        {
            _context.Athletes.Add(athlete);
            _context.SaveChanges();
        }

        public Athlete Get(int id)
        {
            return _context.Athletes.SingleOrDefault(item => item.Id == id);
        }

        public List<Athlete> Get(Func<Athlete, bool> predicate)
        {
            return _context.Athletes.Where(predicate).ToList();
        }
        
        public List<Athlete> GetAll()
        {
            return _context.Athletes.ToList();
        }

        public void Update(Athlete athlete)
        {
            _context.Athletes.Update(athlete);
            _context.Entry(athlete).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Athletes.Remove(new Athlete {Id = id});
            _context.SaveChanges();
        }
    }
}