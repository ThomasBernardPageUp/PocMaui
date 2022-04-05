using PocMaui.ViewModels;

namespace PocMaui;

public partial class SutomPage : ContentPage
{
	public SutomPage()
	{
		BindingContext = new SutomViewModel(Navigation);
		InitializeComponent();
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

		var viewModel = (SutomViewModel)BindingContext;
		viewModel.OnNavigatedTo(args);
    }
}