// <copyright file="LoanService.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

namespace Calculator.Services
{
    using System.Security.Principal;
    using Calculator.Abstractions.Services;
    using Calculator.DataTransferModels.Loan;
    using static System.Runtime.InteropServices.JavaScript.JSType;

    /// <summary>
    /// Interace defining the methods the loan service contains.
    /// </summary>
    public class LoanService : ILoanService
    {
        /// <summary>
        /// Calculates the loan.
        /// </summary>
        public ResponseDto CalculateLoan(LoanDto loanDto)
        {
            var months = TotalMonths(loanDto.Years, loanDto.Months);
            var monthlyPayment = CalculateMonthlyPayment(loanDto.Principal, months, loanDto.Rate);
            var totalAmount = CalculateTotalAmount(monthlyPayment, months);
            var totalInterest = CalculateTotalInterest(totalAmount, loanDto.Principal);
            var monthlyInterest = CalculateMonthlyInterest(totalInterest, months);
            var response = new ResponseDto()
            {
                MonthlyPayment = Math.Round(monthlyPayment, 2),
                MonthlyInterest = Math.Round(monthlyInterest, 2),
                TotalInterest = Math.Round(totalInterest, 2),
                TotalAmount = Math.Round(totalAmount, 2),
            };

            return response;
        }

        private static double CalculateMonthlyPayment(double principal, int months, double rate)
        {
            if (rate == 0)
            {
                return principal / months;
            }

            var monthlyRate = (rate / 100) / 12;
            return monthlyRate / (1 - Math.Pow((1 + monthlyRate), -(months))) * principal;
        }

        private static double CalculateTotalAmount(double monthlyPayment, int months)
        {
            return monthlyPayment * months;
        }

        private static double CalculateTotalInterest(double totalAmount, double principal)
        {
            return totalAmount - principal;
        }

        private static double CalculateMonthlyInterest(double totalInterest, int months)
        {
            var monthlyInterest = totalInterest / months;
            return monthlyInterest;
        }

        private static int TotalMonths(double year, double months)
        {
            return (int)months + (int)(year * 12);
        }
    }
}

