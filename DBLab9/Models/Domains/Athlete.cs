using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBLab9.Models.Domains
{
    public class Athlete
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }
        
        [Required]
        public string PatronymicName { get; set; }
        
        [Required]
        [ForeignKey("Sport")]
        public int SportId { get; set; }
    }
}