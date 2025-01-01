using ShoppingList.Views;

namespace ShoppingList;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute("list", typeof(List));
		Routing.RegisterRoute("products", typeof(Products));
	}
}
