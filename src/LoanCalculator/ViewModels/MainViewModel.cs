using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Calculator.Abstractions.Services;
using Calculator.Services;
using Calculator.DataTransferModels.Loan;

namespace LoanCalculator.ViewModels
{
	public partial class MainViewModel : ObservableObject
    {
        private readonly ILoanService _loanService;

        public MainViewModel(ILoanService loanService)
        {
            _loanService = loanService;
        }

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
            var loanDto = new LoanDto()
            {
                Principal = Amount,
                Rate = rate,
                Years = years,
                Months = months
            };

            var response = _loanService.CalculateLoan(loanDto);

            MonthlyPayment = response.MonthlyPayment;
            MonthlyInterest = response.MonthlyInterest;
            TotalInterest = response.TotalInterest;
            TotalAmount = response.TotalAmount;
        }
	}
}

