using System.ComponentModel.DataAnnotations;

namespace CustomerWebApi.Models;

public class Customer
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Address { get; set; }
}