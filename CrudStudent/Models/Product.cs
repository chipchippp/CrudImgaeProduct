using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrudProduct.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Group { get; set; }
        [Required]
        [StringLength(20)]
        public string RollNumber { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Comment { get; set; }
        [Required]
        [StringLength(20)]
        public string Category { get; set; }
        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Please choose Front image")]
        [Display(Name = "Front Image")]
        [NotMapped]
        public IFormFile FrontImage { get; set; }
        public string BackImageUrl { get; set; }
        [Display(Name = "Back Image")]
        [NotMapped]
        public IFormFile BackImage { get; set; }
    }
}
