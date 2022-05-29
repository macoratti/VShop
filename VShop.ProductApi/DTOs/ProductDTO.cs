using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using VShop.ProductApi.Models;

namespace VShop.ProductApi.DTOs;

public class ProductDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The Name is Required")]
    [MinLength(3)]
    [MaxLength(100)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "The Price is Required")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "The Description is Required")]
    [MinLength(5)]
    [MaxLength(200)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "The Stock is Required")]
    [Range(1, 9999)]
    public long Stock { get; set; }

    public string? ImageURL { get; set; }
    public string? CategoryName { get; set; }

    [JsonIgnore]
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
}
