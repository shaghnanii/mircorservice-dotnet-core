using System.ComponentModel.DataAnnotations;

namespace ProductWebApi.Models;

public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string SKU { get; set; }
    public decimal Price { get; set; }
}