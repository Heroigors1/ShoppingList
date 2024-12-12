using ShoppingList.Views;

namespace ShoppingList;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();		
		Routing.RegisterRoute("products", typeof(ProductsPage));
		Routing.RegisterRoute("addproduct", typeof(AddProductPage));
		Routing.RegisterRoute("list", typeof(ListPage));
	}
}
