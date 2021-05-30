using System;
using System.Collections.Generic;
using System.Linq;
using DBLab9.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace DBLab9.Services.Repositories
{
    public class SportRepository
    {
        private readonly Context _context;

        public SportRepository(Context context)
        {
            _context = context;
        }
        
        public void Add(Sport sport)
        {
            _context.Sports.Add(sport);
            _context.SaveChanges();
        }

        public Sport Get(int id)
        {
            return _context.Sports.SingleOrDefault(item => item.Id == id);
        }

        public List<Sport> Get(Func<Sport, bool> predicate)
        {
            return _context.Sports.Where(predicate).ToList();
        }
        
        public List<Sport> GetAll()
        {
            return _context.Sports.ToList();
        }

        public void Update(Sport sport)
        {
            _context.Sports.Update(sport);
            _context.Entry(sport).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Sports.Remove(new Sport {Id = id});
            _context.SaveChanges();
        }
    }
}