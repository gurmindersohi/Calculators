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
                var periods = TotalPeriod(mortgageDto.Years, mortgageDto.Months, mortgageDto.PaymentFrequency);
                var payment = CalculatePayment(mortgageDto.MortgageAmount, periods, mortgageDto.InterestRate, mortgageDto.PaymentFrequency);
                var totalAmount = CalculateTotalAmount(payment, periods);
                var totalInterest = CalculateTotalInterest(totalAmount, mortgageDto.MortgageAmount);
                var interest = CalculateInterest(totalInterest, periods);

                var response = new ResponseDto()
                {
                    Payment = Math.Round(payment, 2),
                    Interest = Math.Round(interest, 2),
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

            var payment = rate / (1 - Math.Pow((1 + rate), -(periods))) * mortgageAmount;
            return Math.Round(payment, 2);
        }

        private static double CalculateTotalAmount(double payment, int periods)
        {
            return payment * periods;
        }

        private static double CalculateTotalInterest(double totalAmount, double mortgageAmount)
        {
            return totalAmount - mortgageAmount;
        }

        private static double CalculateInterest(double totalInterest, int periods)
        {
            var interest = totalInterest / periods;
            return interest;
        }

        public static int TotalPeriod(int years, int months, PaymentFrequency paymentFrequency)
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

        public static double CalculateInterestRate(double interestRate, PaymentFrequency paymentFrequency)
        {
            var rate = interestRate / 100;
            switch (paymentFrequency)
            {
                case PaymentFrequency.Monthly:
                    return rate / 12;
                case PaymentFrequency.SemiMonthly:
                    return rate / 24;
                case PaymentFrequency.BiWeekly:
                    return rate / 26;
                case PaymentFrequency.Weekly:
                    return rate / 52;
                default:
                    return 0;
            }
        }
    }
}