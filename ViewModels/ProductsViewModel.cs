using System;
using System.Collections.ObjectModel;
using ShoppingList.Models;
using ShoppingList.Services;

namespace ShoppingList.ViewModels;

public class ProductsViewModel
{
    private readonly FileService _fileService;
    public ObservableCollection<Product> Products { get; set; }
    public ProductsViewModel()
    {
        _fileService = new FileService();
        Products = _fileService.LoadProducts();
    }
}
