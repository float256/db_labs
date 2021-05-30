using System;

namespace DBLab9.Models.Dto
{
    public class CompetitionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string SportsComplexName { get; set; }
        public int NumberOfParticipants { get; set; }
    }
}