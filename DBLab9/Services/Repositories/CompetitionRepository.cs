using System;
using System.Collections.Generic;
using System.Linq;
using DBLab9.Models.Domains;
using Microsoft.EntityFrameworkCore;

namespace DBLab9.Services.Repositories
{
    public class CompetitionRepository
    {
        private readonly Context _context;

        public CompetitionRepository(Context context)
        {
            _context = context;
        }
        public void Add(Competition competition)
        {
            _context.Competitions.Add(competition);
            _context.SaveChanges();
        }

        public Competition Get(int id)
        {
            return _context.Competitions.SingleOrDefault(item => item.Id == id);
        }

        public List<Competition> Get(Func<Competition, bool> predicate)
        {
            return _context.Competitions.Where(predicate).ToList();
        }
        
        public List<Competition> GetAll()
        {
            return _context.Competitions.ToList();
        }

        public void Update(Competition competition)
        {
            _context.Competitions.Update(competition);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Competitions.Remove(new Competition {Id = id});
            _context.SaveChanges();
        }
    }
}