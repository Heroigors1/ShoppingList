using System;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using ShoppingList.Models;
using ShoppingList.Views;

namespace ShoppingList.Services;

public class FileService
{
    ObservableCollection<Product> Products = new ObservableCollection<Product>();
    ObservableCollection<Category> Categories = new ObservableCollection<Category>();
	ObservableCollection<Volume> Volumes = new ObservableCollection<Volume>();
    public ObservableCollection<Product> LoadProducts()
    {
		Products.Clear();
        var filename = "products.xml";

        var fullpath = Path.Combine(FileSystem.AppDataDirectory, filename);

        if(!File.Exists(fullpath))
        {
            GenerateDefaultProducts();
        }
        else
		{
			//GenerateDefaultProducts();
			var xmlDoc = XDocument.Load(fullpath);
			var products = xmlDoc.Root.Elements("Product")
				.Select(x => new Product
				{
					Id = (int)x.Element("Id"),
					Name = (string)x.Element("Name"),
					CategoryId = (int)x.Element("CategoryId"),
				});

			Products.Clear();
			foreach (var product in products)
			{
				Products.Add(product);
			}
		}
        
        return Products;
    }

    private void GenerateDefaultProducts()
	{
		var filepath = "products.xml";

		var fullpath = Path.Combine(FileSystem.AppDataDirectory, filepath);

		List<Product> DefaultProducts = new List<Product>
		{
			new Product{Id = 1, Name = "Bread", CategoryId = 1},
			new Product{Id = 2, Name = "Water", CategoryId = 2},
			new Product{Id = 3, Name = "Wine", CategoryId = 2}
		};

		var xml = new XDocument(
			new XElement("Products",
				DefaultProducts.Select(product => 
					new XElement("Product",
						new XElement("Id", product.Id),
						new XElement("Name", product.Name),
						new XElement("CategoryId", product.CategoryId)
					))
			)
		);

		Products.Clear();
		foreach (var product in DefaultProducts)
		{
			Products.Add(product);
		}

		xml.Save(fullpath);
	}

    public ObservableCollection<Category> LoadCategories()
    {
        var filename = "categories.xml";

        var fullpath = Path.Combine(FileSystem.AppDataDirectory, filename);

        if(!File.Exists(fullpath))
        {
            GenerateDefaultCategories();
        }
        else
        {
            //GenerateDefaultCategories();
			var xmlDoc = XDocument.Load(fullpath);
			var categories = xmlDoc.Root.Elements("Category")
				.Select(x => new Category
				{
					Id = (int)x.Element("Id"),
					Name = (string)x.Element("Name")
				});

			Categories.Clear();
			foreach (var category in categories)
			{
				Categories.Add(category);
			}
        }

        return Categories;
    }

    private void GenerateDefaultCategories()
    {
        var filename = "categories.xml";

        var fullpath = Path.Combine(FileSystem.AppDataDirectory, filename);

        List<Category> DefaultCategories = new List<Category>{
            new Category{Id = 1, Name = "Bread"},
			new Category{Id = 2, Name = "Drinks"},
			new Category{Id = 3, Name = "Vegetables"}
        };

        var xml = new XDocument(
			new XElement("Categories",
					DefaultCategories.Select(c => 
						new XElement("Category",
							new XElement("Id", c.Id),
							new XElement("Name", c.Name)
						)
					))
			);

		Categories.Clear();
		foreach(var category in DefaultCategories)
		{
			Categories.Add(category);
		}

		xml.Save(fullpath);
    }

	public int GetCategoryIdByName(string categoryName)
	{
		if (Categories.Count == 0)
		{
			LoadCategories();
		}

		var category = Categories.FirstOrDefault(c => string.Equals(c.Name, categoryName, StringComparison.OrdinalIgnoreCase));

		return category.Id;
	}

	public string GetCategoryNameById(int categoryId)
	{
		if (Categories.Count == 0)
		{
			LoadCategories();
		}

		var category = Categories.FirstOrDefault(c => c.Id == categoryId);

		return category.Name;
	}

	public ObservableCollection<Volume> LoadVolumes()
	{
		var filename = "volumes.xml";

        var fullpath = Path.Combine(FileSystem.AppDataDirectory, filename);

		if(!File.Exists(fullpath))
        {
            GenerateDefaultVolumes();
        }
        else
        {
            //GenerateDefaultVolumes();
			var xmlDoc = XDocument.Load(fullpath);
			var volumes = xmlDoc.Root.Elements("Volume")
				.Select(x => new Volume
				{
					Name = (string)x.Element("Name")
				});

			Volumes.Clear();
			foreach (var volume in volumes)
			{
				Volumes.Add(volume);
			}
        }

		return Volumes;
	}

	private void GenerateDefaultVolumes()
	{
		var filename = "volumes.xml";

        var fullpath = Path.Combine(FileSystem.AppDataDirectory, filename);

        List<Volume> DefaultVolumes = new List<Volume>{
            new Volume{Name = "psc"},
			new Volume{Name = "l"},
			new Volume{Name = "ml"},
			new Volume{Name = "g"},
			new Volume{Name = "kg"},
        };

        var xml = new XDocument(
			new XElement("Categories",
					DefaultVolumes.Select(v => 
						new XElement("Volume",
							new XElement("Name", v.Name)
						)
					))
			);

		Volumes.Clear();
		foreach(var volume in DefaultVolumes)
		{
			Volumes.Add(volume);
		}

		xml.Save(fullpath);
	}
}
