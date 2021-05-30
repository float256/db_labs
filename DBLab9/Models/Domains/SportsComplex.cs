using System.ComponentModel.DataAnnotations;

namespace DBLab9.Models.Domains
{
    public class SportsComplex
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Address { get; set; }
        
        [Required]
        public uint Capacity { get; set; }
    }
}