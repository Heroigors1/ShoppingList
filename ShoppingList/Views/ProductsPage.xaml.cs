using System.Collections.ObjectModel;
using System.Xml.Linq;
using AuthenticationServices;
using Microsoft.Maui.Controls.Generated;
using ShoppingList.Models;
using ShoppingList.Services;

namespace ShoppingList.Views;

public partial class ProductsPage : ContentPage
{
	public ObservableCollection<ProductModel> Products { get; set; }
	public ObservableCollection<Category> Categories { get; set; }
	public FileService fileService { get; set; }
	public class ProductModel
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Category { get; set; }
	}
	public ProductsPage()
	{
		Products = new ObservableCollection<ProductModel>();
		Categories = new ObservableCollection<Category>();
		fileService = new FileService();
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
		DisplayProducts();
		DisplayCategories();
	}

	private void DisplayProducts()
	{
		var products = fileService.LoadProducts();

		Products.Clear();
		foreach (var product in products)
		{
			Products.Add(new ProductModel{
				Id = product.Id,
				Name = product.Name,
				Category = fileService.GetCategoryNameById(product.CategoryId),
			});
		}
	}

	private void DisplayCategories()
	{
		var categories = fileService.LoadCategories();

		Categories.Clear();
		foreach (var category in categories)
		{
			Categories.Add(category);
		}
	}
	
}