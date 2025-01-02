using System;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using ShoppingList.Models;

namespace ShoppingList.Services;

public class FileService
{
    ObservableCollection<Product> Products = new();
    public ObservableCollection<Product> LoadProducts()
    {
        var fullpath = Path.Combine(FileSystem.AppDataDirectory, "products.xml");
        
        if(!File.Exists(fullpath))
        {
            GenerateDefaultProducts();
        }
        else
        {
            GenerateDefaultProducts();
            var xDoc = XDocument.Load(fullpath);
            var products = xDoc.Root.Elements("Product")
                            .Select(x => new Product{
                                Id = (int)x.Element("Id"),
                                Name = (string)x.Element("Name"),
                                CategoryId = (int)x.Element("CategoryId"),
                                Quantity = (int)x.Element("Quantity"),
                                Unit = (string)x.Element("Unit"),
                                Store = (string)x.Element("Store"),
                                IsBought = (bool)x.Element("IsBought"),
                                IsOptional = (bool)x.Element("IsOptional")
                            });
            Products.Clear();
            foreach(var product in products)
            {
                Products.Add(product);
            }
        }
        return Products;
    }

    public void GenerateDefaultProducts()
    {
        var fullpath = Path.Combine(FileSystem.AppDataDirectory, "products.xml");

        var products = new ObservableCollection<Product> 
        {
            new Product{ Id = 1, Name = "Onion", Quantity = 1, Unit = "kg", Store = "Biedronka", CategoryId = 1, IsBought = false, IsOptional = false},
            new Product{ Id = 2, Name = "Water", Quantity = 1, Unit = "l", Store = "Lidl", CategoryId = 2, IsBought = false, IsOptional = false}
        };

        var xDoc = new XDocument(
            new XElement("Products",
                products.Select(p => 
                    new XElement("Product",
                        new XElement("Id", p.Id),
                        new XElement("Name", p.Name),
                        new XElement("Quantity", p.Quantity),
                        new XElement("Unit", p.Unit),
                        new XElement("Store", p.Store),
                        new XElement("CategoryId", p.CategoryId),
                        new XElement("IsBought", p.IsBought),
                        new XElement("IsOptional", p.IsOptional)
                    )
                )
            )
        );

        xDoc.Save(fullpath);
    }
}
