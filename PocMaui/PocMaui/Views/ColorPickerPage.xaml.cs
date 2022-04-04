using PocMaui.ViewModels;

namespace PocMaui;

public partial class ColorPickerPage : ContentPage
{
	public ColorPickerPage()
	{
		BindingContext = new ColorPickerViewModel(Navigation);
		InitializeComponent();
	}
}