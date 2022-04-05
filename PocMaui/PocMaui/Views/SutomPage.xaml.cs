using PocMaui.ViewModels;

namespace PocMaui;

public partial class SutomPage : ContentPage
{
	public SutomPage()
	{
		BindingContext = new SutomViewModel(Navigation);
		InitializeComponent();
	}
}