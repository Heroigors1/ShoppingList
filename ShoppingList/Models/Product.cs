using System;

namespace ShoppingList.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public string Volume { get; set; }
}
