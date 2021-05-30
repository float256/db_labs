using Microsoft.EntityFrameworkCore;
using DBLab9.Models.Domains;

namespace DBLab9.Services
{
    public class Context: DbContext
    {
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<AthletePerformance> AthletePerformances { get; set; }
        public DbSet<Competition> Competitions { get; set; }
        public DbSet<SportsComplex> SportsComplexes { get; set; }

        public Context(DbContextOptions<Context> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder builder) { }
    }
}