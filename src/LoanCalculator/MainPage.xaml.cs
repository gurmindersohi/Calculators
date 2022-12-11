namespace LoanCalculator;
using LoanCalculator.ViewModels;

public partial class MainPage : ContentPage
{
	public MainPage(MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}


