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
                // Period should be 65 but it is 52 Biweekly 2 year 6 monts test.
                var period = TotalPeriod(mortgageDto.Years, mortgageDto.Months, mortgageDto.PaymentFrequency);
                var monthlyPayment = CalculatePayment(mortgageDto.MortgageAmount, period, mortgageDto.InterestRate, mortgageDto.PaymentFrequency);
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

        public double CalculatePayment(double mortgageAmount, int periods, double interestRate, PaymentFrequency paymentFrequency)
        {
            if (interestRate == 0)
            {
                return mortgageAmount / periods;
            }

            var rate = CalculateInterestRate(interestRate, paymentFrequency);

            return rate / (1 - Math.Pow((1 + rate), -(periods))) * mortgageAmount;
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

        public int TotalPeriod(int years, int months, PaymentFrequency paymentFrequency)
        {
            decimal totalMonths = months + (years * 12);
            var periods = decimal.MinValue;
            switch (paymentFrequency)
            {
                case PaymentFrequency.Monthly:
                    periods = totalMonths;
                    break;
                case PaymentFrequency.SemiMonthly:
                    periods = totalMonths * 2;
                    break;
                case PaymentFrequency.BiWeekly:
                    periods = totalMonths / 12 * 26;
                    break;
                case PaymentFrequency.Weekly:
                    periods = (totalMonths / 12) * 52;
                    break;
                default:
                    periods = 0;
                    break;
            }

            return Convert.ToInt32(periods);
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