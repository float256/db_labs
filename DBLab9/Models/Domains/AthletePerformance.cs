using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBLab9.Models.Domains
{
    public class AthletePerformance
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        [ForeignKey("Athlete")]
        public int AthleteId { get; set; }
        
        [Required]
        [ForeignKey("Competition")]
        public int CompetitionId { get; set; }
        
        [Required]
        public DateTime PerformanceDate { get; set; }
        
        [Required]
        public int Score { get; set; }
    }
}