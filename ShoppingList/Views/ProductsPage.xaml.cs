using System.Collections.ObjectModel;
using System.Xml.Linq;
using Microsoft.Maui.Controls.Generated;
using ShoppingList.Models;

namespace ShoppingList.Views;

public partial class ProductsPage : ContentPage
{
	public ObservableCollection<Product> Products { get; set; }
	public ObservableCollection<Category> Categories { get; set; }
	public ProductsPage()
	{
		Products = new ObservableCollection<Product>();
		Categories = new ObservableCollection<Category>();
		InitializeComponent();
		BindingContext = this;
	}

	private void OnAddClicked(object sender, EventArgs e)
	{
		Shell.Current.GoToAsync("addproduct");
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		LoadProducts();
	}

	private void LoadProducts()
	{
		var filepath = "products.xml";

		var fullpath = Path.Combine(FileSystem.AppDataDirectory, filepath);

		if(!File.Exists(fullpath))
		{
			GenerateDefaultProducts();
		}
		else
		{
			GenerateDefaultProducts();
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

	}

	private void LoadCategories()
	{
		var filepath = "categories.xml";

		var fullpath = Path.Combine(FileSystem.AppDataDirectory, filepath);

		List<Category> DefaultCategories = new List<Category>
		{
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
}