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
        double lineWidth = DeviceDisplay.MainDisplayInfo.Width;

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
                Years = Convert.ToDouble(years),
                Months = Convert.ToDouble(months)
            };

            var response = _loanService.CalculateLoan(loanDto);

            MonthlyPayment = String.Format("${0:n2}", response.MonthlyPayment);
            MonthlyInterest = String.Format("${0:n2}", response.MonthlyInterest);
            TotalInterest = String.Format("${0:n2}", response.TotalInterest);
            TotalAmount = String.Format("${0:n2}", response.TotalAmount);
        }
	}
}

