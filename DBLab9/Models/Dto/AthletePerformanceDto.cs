using System;

namespace DBLab9.Models.Dto
{
    public class AthletePerformanceDto
    {
        public int Id { get; set; }
        public string AthleteName { get; set; }
        public string AthleteSurname { get; set; }
        public string AthletePatronymicName { get; set; }
        public string CompetitionName { get; set; }
        public DateTime PerformanceDate { get; set; }
        public int Score { get; set; }
    }
}