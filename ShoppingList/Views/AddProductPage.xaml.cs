using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Linq;
using ShoppingList.Models;
using ShoppingList.Services;

namespace ShoppingList.Views;

public partial class AddProductPage : ContentPage
{
	public ObservableCollection<Product> Products { get; set; }
	public ObservableCollection<Category> Categories { get; set; }
	public ObservableCollection<Volume> Volumes{ get; set; }
	private FileService fileService = new FileService();
	
	public AddProductPage()
	{
		InitializeComponent();
		fileService = new();
		Categories = new ObservableCollection<Category>();
		Products = new ObservableCollection<Product>();
		Volumes = new ObservableCollection<Volume>();
		CategoryPicker.ItemsSource = Categories;
        CategoryPicker.ItemDisplayBinding = new Binding("Name");
		VolumePicker.ItemsSource = Volumes;
		VolumePicker.ItemDisplayBinding = new Binding("Name");
	}

	private void OnAddBtnClicked(object sender, EventArgs e)
	{
		string filepath = "products.xml";
		string fullpath = Path.Combine(FileSystem.AppDataDirectory, filepath);
		var products = fileService.LoadProducts();
		var maxId = products.Count;
		string productName = productNameEntry.Text;
		var selectedCategory = CategoryPicker.SelectedItem as Category;
		if (selectedCategory == null)
		{
			DisplayAlert("Błąd", "Nie wybrano kategorii!", "OK");
			return;
		}
		var newProduct = new Product
		{
			Id = maxId + 1,
			Name = productName,
			CategoryId = selectedCategory.Id,
		};
		var xmlDoc = XDocument.Load(fullpath);
		xmlDoc.Root.Add(new XElement("Product",
			new XElement("Id", newProduct.Id),
			new XElement("Name", newProduct.Name),
			new XElement("CategoryId", newProduct.CategoryId)
		));
		xmlDoc.Save(fullpath);
		DisplayAlert("Dodano nowy produkt", "Produkt został dodany pomyślnie.", "OK");
		Shell.Current.GoToAsync("products");
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		DisplayCategories();
		DisplayVolumes();
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

	private void DisplayVolumes()
	{
		var volumes = fileService.LoadVolumes();

		Volumes.Clear();
		foreach(var volume in volumes)
		{
			Volumes.Add(volume);
		}
	}
}