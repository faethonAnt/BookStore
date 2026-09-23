using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BulkyBook.Models;

public class Product
{
    public int Id { get; set; }
    
    [Required]
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public string ISBN { get; set; } = string.Empty;
    
    [Required]
    public string Author { get; set; } = string.Empty;
    
    [Required]
    [Display(Name = "List Price")]
    [Range(0, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
    public double ListPrice { get; set; }
    
    [Required]
    [Display(Name = "Price for 1-50")]
    [Range(0, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
    public double Price { get; set; }
    
    [Required]
    [Display(Name = "Price for 50+")]
    [Range(0, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
    public double Price50 { get; set; }
    
    [Required]
    [Display(Name = "Price for 100+")]
    [Range(0, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
    public double Price100 { get; set; }
    
    [Display(Name = "Category")]
    public int CategoryId { get; set; }
    [ValidateNever]
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    
    [Display(Name = "Product Image")]
    public string? ImageUrl { get; set; }
}
