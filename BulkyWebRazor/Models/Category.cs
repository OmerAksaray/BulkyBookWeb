using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BulkyWebRazor.Models
{
    public class Category
    {
        //-->primary key olmasını sağlar
        [Key]
        public int CategoryId { get; set; }
        //-->null veya boşluk olarak geçilememsini sağlar
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "Olamz Custom Message Must be between 1-100!")]
        public int DisplayOrder { get; set; }
    }
}
