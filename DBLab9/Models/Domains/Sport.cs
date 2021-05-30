using System.ComponentModel.DataAnnotations;

namespace DBLab9.Models.Domains
{
    public class Sport
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public bool IsOlympic { get; set; }
    }
}