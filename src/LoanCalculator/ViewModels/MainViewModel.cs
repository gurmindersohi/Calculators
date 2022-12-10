using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace LoanCalculator.ViewModels
{
	public partial class MainViewModel : ObservableObject
	{
        [ObservableProperty]
        double monthlyPayment;

        [ObservableProperty]
        double monthlyInterest;

        [ObservableProperty]
        double totalInterest;

        [ObservableProperty]
        double totalAmount;

        [ObservableProperty]
        double amount;

        [ObservableProperty]
        double rate;

        [ObservableProperty]
        int years;

        [ObservableProperty]
        int months;

        [RelayCommand]
		void CalculateLoan()
		{
            MonthlyPayment = 101;
            MonthlyInterest = 102;
            TotalInterest = 103;
            TotalAmount = 104;
        }
	}
}

