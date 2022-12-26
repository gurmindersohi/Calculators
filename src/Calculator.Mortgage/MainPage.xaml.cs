namespace Calculator.Mortgage;
using Calculator.Mortgage.ViewModels;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}


