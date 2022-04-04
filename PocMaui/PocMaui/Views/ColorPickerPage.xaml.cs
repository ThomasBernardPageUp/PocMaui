using PocMaui.ViewModels;

namespace PocMaui;

public partial class ColorPickerPage : ContentPage
{
	public ColorPickerPage()
	{
		// Use ColorPickerViewModel to bind
		this.BindingContext = new ColorPickerViewModel();

		InitializeComponent();
	}
}