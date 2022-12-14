// <copyright file="MortgageService.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

namespace Calculator.Services
{
    using System.Security.Principal;
    using Calculator.Abstractions.Services;
    using Calculator.DataTransferModels.Enums;
    using Calculator.DataTransferModels.Mortgage;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    /// <summary>
    /// Interace defining the methods the mortgage service contains.
    /// </summary>
    public class MortgageService : IMortgageService
    {
        /// <summary>
        /// Calculates the mortgage.
        /// </summary>
        public MortgageResponse CalculateMortgage(MortgageDto mortgageDto)
        {
            try
            {
                var period = TotalPeriod(mortgageDto.Years, mortgageDto.Months, mortgageDto.PaymentFrequency);
                var interestRate = CalculateInterestRate(mortgageDto.InterestRate, mortgageDto.PaymentFrequency);
                var monthlyPayment = CalculateMonthlyPayment(mortgageDto.MortgageAmount, period, interestRate);
                var totalAmount = CalculateTotalAmount(monthlyPayment, period);
                var totalInterest = CalculateTotalInterest(totalAmount, mortgageDto.MortgageAmount);
                var monthlyInterest = CalculateMonthlyInterest(totalInterest, period);

                var response = new ResponseDto()
                {
                    Payment = Math.Round(monthlyPayment, 2),
                    Interest = Math.Round(monthlyInterest, 2),
                    TotalInterest = Math.Round(totalInterest, 2),
                    TotalAmount = Math.Round(totalAmount, 2),
                };

                return new MortgageResponse(response);
            }
            catch (Exception ex)
            {
                return new MortgageResponse($"An error occurred while calculating the mortgage: {ex.Message}");
            }
        }

        private static double CalculateMonthlyPayment(double mortgageAmount, int months, double interestRate)
        {
            if (interestRate == 0)
            {
                return mortgageAmount / months;
            }

            return interestRate / (1 - Math.Pow((1 + interestRate), -(months))) * mortgageAmount;
        }

        private static double CalculateTotalAmount(double monthlyPayment, int months)
        {
            return monthlyPayment * months;
        }

        private static double CalculateTotalInterest(double totalAmount, double mortgageAmount)
        {
            return totalAmount - mortgageAmount;
        }

        private static double CalculateMonthlyInterest(double totalInterest, int months)
        {
            var monthlyInterest = totalInterest / months;
            return monthlyInterest;
        }

        private static int TotalPeriod(int year, int months, PaymentFrequency paymentFrequency)
        {
            var totalMonths = months + (year * 12);
            switch (paymentFrequency)
            {
                case PaymentFrequency.Monthly:
                    return totalMonths;
                case PaymentFrequency.SemiMonthly:
                    return totalMonths * 2;
                case PaymentFrequency.BiWeekly:
                    return (totalMonths / 12) * 26;
                case PaymentFrequency.Weekly:
                    return (totalMonths / 12) * 52;
                default:
                    return 0;
            }
        }

        private static double CalculateInterestRate(double interestRate, PaymentFrequency paymentFrequency)
        {
            switch (paymentFrequency)
            {
                case PaymentFrequency.Monthly:
                    return (interestRate / 100) / 12;
                case PaymentFrequency.SemiMonthly:
                    return (interestRate / 100) / 24;
                case PaymentFrequency.BiWeekly:
                    return (interestRate / 100) / 26;
                case PaymentFrequency.Weekly:
                    return (interestRate / 100) / 52;
                default:
                    return 0;
            }
        }
    }
}