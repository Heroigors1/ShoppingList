using System.Collections.ObjectModel;
using ShoppingList.Models;
using ShoppingList.Services;

namespace ShoppingList.Views;

public partial class AddProductPage : ContentPage
{
	public ObservableCollection<Category> Categories { get; set; }
	private FileService fileService = new FileService();
	public AddProductPage()
	{
		InitializeComponent();
		Categories = new ObservableCollection<Category>();
		CategoryPicker.ItemsSource = Categories;
        CategoryPicker.ItemDisplayBinding = new Binding("Name");
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		DisplayCategories();
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