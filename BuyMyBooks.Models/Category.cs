using System.ComponentModel.DataAnnotations;

namespace BuyMyBooks.Models;

public class Category
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Display(Name="Category Name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Range(0,100,ErrorMessage = "Range must be between 0-100!")]
    [Display(Name="Display Order")]
    public int? DisplayOrder { get; set; }
}
