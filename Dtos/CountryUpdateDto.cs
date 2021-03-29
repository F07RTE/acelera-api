using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class CountryUpdateDto
    {
        [Required]
        [MaxLength(250)]
        public string name { get; set; }
        
        [Required]
        public string capital { get; set; }

        [Required]
        public bool isAvalible { get; set; }
    }
}