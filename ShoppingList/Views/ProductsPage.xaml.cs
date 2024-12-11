using System.Collections.ObjectModel;
using System.Xml.Linq;
using Microsoft.Maui.Controls.Generated;
using ShoppingList.Models;

namespace ShoppingList.Views;

public partial class ProductsPage : ContentPage
{
	public ObservableCollection<Product> Products { get; set; }
	public ProductsPage()
	{
		Products = Products = new ObservableCollection<Product>();
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