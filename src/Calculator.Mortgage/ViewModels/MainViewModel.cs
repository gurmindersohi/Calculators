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
    using Calculator.Mortgage.Extensions;

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
        string payment = "-";

        [ObservableProperty]
        string interest = "-";

        [ObservableProperty]
        string totalInterest = "-";

        [ObservableProperty]
        string totalAmount = "-";

        [ObservableProperty]
        string period = "Monthly";

        [ObservableProperty]
        double? amount;

        [ObservableProperty]
        double? rate;

        [ObservableProperty]
        int? years;

        [ObservableProperty]
        int? months;

        [ObservableProperty]
        int paymentFrequency = 0;

        [RelayCommand]
        void CalculateLoan()
        {
            var mortgageDto = new MortgageDto()
            {
                MortgageAmount = Amount ?? 0,
                Years = years ?? 0,
                Months = months ?? 0,
                PaymentFrequency = (PaymentFrequency)paymentFrequency,
                InterestRate = rate ?? 0
            };

            var response = _mortgageService.CalculateMortgage(mortgageDto);

            if (response.Success)
            {
                Payment = String.Format("${0:n2}", response.ResponseDto.Payment);
                Interest = String.Format("${0:n2}", response.ResponseDto.Interest);
                TotalInterest = String.Format("${0:n2}", response.ResponseDto.TotalInterest);
                TotalAmount = String.Format("${0:n2}", response.ResponseDto.TotalAmount);
                var paymentPeriod = (PaymentFrequency)paymentFrequency;
                Period = $"{paymentPeriod.GetDisplayName()}";
            }
        }
    }
}
