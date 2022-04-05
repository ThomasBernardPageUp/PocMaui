using PocMaui.ViewModels;

namespace PocMaui;

public partial class ColorPickerPage : ContentPage
{
	public ColorPickerPage()
	{
		BindingContext = new ColorPickerViewModel(Navigation);
		InitializeComponent();
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		var viewModel = (ColorPickerViewModel)BindingContext;
		viewModel.OnNavigatedTo(args);
	}
}