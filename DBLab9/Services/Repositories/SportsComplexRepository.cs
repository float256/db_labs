using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DBLab9.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace DBLab9.Services.Repositories
{
    public class SportsComplexRepository
    {
        private readonly Context _context;

        public SportsComplexRepository(Context context)
        {
            _context = context;
        }

        public void Add(SportsComplex sportsComplex)
        {
            _context.SportsComplexes.Add(sportsComplex);
            _context.SaveChanges();
        }
        
        public SportsComplex Get(int id)
        {
            return _context.SportsComplexes.SingleOrDefault(item => item.Id == id);
        }

        public List<SportsComplex> Get(Func<SportsComplex, bool> predicate)
        {
            return _context.SportsComplexes.Where(predicate).ToList();
        }
        
        public List<SportsComplex> GetAll()
        {
            return _context.SportsComplexes.ToList();
        }

        public void Update(SportsComplex sportsComplex)
        {
            _context.SportsComplexes.Update(sportsComplex);
            _context.Entry(sportsComplex).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.SportsComplexes.Remove(new SportsComplex {Id = id});
            _context.SaveChanges();
        }
    }
}