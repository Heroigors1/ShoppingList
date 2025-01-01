using System;

namespace ShoppingList.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public int Quantity { get; set; }
    public string Unit { get; set; }
    public string Store { get; set; }
    public bool IsOptional { get; set; }
    public bool IsBought { get; set; }
}
