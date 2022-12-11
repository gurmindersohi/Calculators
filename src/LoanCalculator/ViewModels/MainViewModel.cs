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
        string monthlyPayment = "-";

        [ObservableProperty]
        string monthlyInterest = "-";

        [ObservableProperty]
        string totalInterest = "-";

        [ObservableProperty]
        string totalAmount = "-";

        [ObservableProperty]
        string amount;

        [ObservableProperty]
        string rate;

        [ObservableProperty]
        string years;

        [ObservableProperty]
        string months;

        [RelayCommand]
		void CalculateLoan()
        {
            if (string.IsNullOrWhiteSpace(amount))
            {
                amount = "0";
            }

            if (string.IsNullOrWhiteSpace(rate))
            {
                rate = "0";
            }

            if (string.IsNullOrWhiteSpace(years))
            {
                years = "0";
            }

            if (string.IsNullOrWhiteSpace(months))
            {
                months = "0";
            }

            var loanDto = new LoanDto()
            {
                Principal = Convert.ToDouble(Amount),
                Rate = Convert.ToDouble(rate),
                Years = Convert.ToInt32(years),
                Months = Convert.ToInt32(months)
            };

            var response = _loanService.CalculateLoan(loanDto);

            MonthlyPayment = Convert.ToString(response.MonthlyPayment);
            MonthlyInterest = Convert.ToString(response.MonthlyInterest);
            TotalInterest = Convert.ToString(response.TotalInterest);
            TotalAmount = Convert.ToString(response.TotalAmount);
        }
	}
}

