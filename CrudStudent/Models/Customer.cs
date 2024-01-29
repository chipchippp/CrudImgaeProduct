using System.ComponentModel.DataAnnotations;

namespace CrudStudent.Models
{
    public class Customer
    {
        [Key]
        [StringLength(20)]
        public string CustomerId { get; set; }
        [Required]
        [StringLength(70)]
        public string Firstname { get; set; }
        [Required]
        [StringLength(70)]

        public string Lastname { get; set; }
        public string PhotoUrl { get; set; }
    }
}
