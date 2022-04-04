using PocMaui.ViewModels;

namespace PocMaui;

public partial class ColorGeneratorPage : ContentPage
{
	public ColorGeneratorPage()
	{
		BindingContext = new ColorGeneratorViewModel(Navigation);
		InitializeComponent();
	}
}