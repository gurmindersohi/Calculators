// <copyright file="MainViewModel.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

namespace Calculator.Mortgage.ViewModels
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using Calculator.Abstractions.Services;
    using Calculator.Services;
    using Calculator.DataTransferModels.Mortgage;
    using Calculator.DataTransferModels.Enums;

    public partial class MainViewModel : ObservableObject
    {
        private readonly IMortgageService _mortgageService;

        public MainViewModel(IMortgageService mortgageService)
        {
            _mortgageService = mortgageService;
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

        [ObservableProperty]
        int paymentFrequency;

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

            var mortgageDto = new MortgageDto()
            {
                MortgageAmount = Convert.ToDouble(Amount),
                Years = Convert.ToInt32(years),
                Months = Convert.ToInt32(months),
                PaymentFrequency = (PaymentFrequency)paymentFrequency,
                InterestRate = Convert.ToDouble(rate)
            };

            var response = _mortgageService.CalculateMortgage(mortgageDto);

            MonthlyPayment = String.Format("${0:n2}", response.ResponseDto.MonthlyPayment);
            MonthlyInterest = String.Format("${0:n2}", response.ResponseDto.MonthlyInterest);
            TotalInterest = String.Format("${0:n2}", response.ResponseDto.TotalInterest);
            TotalAmount = String.Format("${0:n2}", response.ResponseDto.TotalAmount);
        }
    }
}
