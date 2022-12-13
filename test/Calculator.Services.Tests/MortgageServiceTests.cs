// <copyright file="MortgageServiceTests.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

namespace Calculator.Services.Tests
{
    using System.Security.Principal;
    using Calculator.DataTransferModels.Enums;
    using Calculator.DataTransferModels.Loan;
    using Calculator.DataTransferModels.Mortgage;
    using FluentAssertions;
    using FluentAssertions.Common;

    /// <summary>
    /// Test Case for mortgage calculator service.
    /// </summary>
	public class MortgageServiceTests
    {
        private readonly MortgageService _service;

        public MortgageServiceTests()
        {
            _service = new MortgageService();
        }

        //[Theory]
        //[InlineData(1000, 2, 0, PaymentFrequency.Monthly, 10, 46.14)]
        //[InlineData(1000, 2, 6, PaymentFrequency.Monthly, 10, 37.81)]
        //[InlineData(10000, 2, 0, PaymentFrequency.Monthly, 10, 461.45)]
        //[InlineData(10000, 2, 0, PaymentFrequency.SemiMonthly, 10, 230.29)]
        //[InlineData(300000, 25, 0, PaymentFrequency.Monthly, 10, 2726.10)]
        //[InlineData(300000, 25, 0, PaymentFrequency.Monthly, 6.49, 2023.75)]
        //[InlineData(300000, 25, 3, PaymentFrequency.Monthly, 3.79, 1539.73)]
        //[InlineData(300000, 25, 3, PaymentFrequency.SemiMonthly, 3.79, 769.5)]
        //[InlineData(300000, 25, 3, PaymentFrequency.BiWeekly, 3.79, 714.53)]
        //[InlineData(300000, 25, 0, PaymentFrequency.BiWeekly, 3.79, 714.53)]
        //[InlineData(300000, 25, 0, PaymentFrequency.Weekly, 3.79, 357.19)]
        //[InlineData(300000, 25, 3, PaymentFrequency.Weekly, 3.79, 384)]
        //public void CalculateLoan_ValidInput_ShouldSucceed(double mortgageAmount,
        //     double years,
        //     double months,
        //     PaymentFrequency paymentFrequency,
        //     double interestRate,
        //     double expectedMonthlyPayment)
        //{
        //    // Arrange
        //    var item = new MortgageDto()
        //    {
        //        MortgageAmount = mortgageAmount,
        //        Years = years,
        //        Months = months,
        //        PaymentFrequency = paymentFrequency,
        //        InterestRate = interestRate,
        //    };

        //    // Act
        //    var response = _service.CalculateMortgage(item);

        //    // Assert
        //    response.MonthlyPayment.Should().Be(expectedMonthlyPayment);
        //}

        [Theory]
        [InlineData(39176.00, 7, 0, PaymentFrequency.Monthly, 3.79, 531.71)]
        [InlineData(300000, 25, 0, PaymentFrequency.Monthly, 1, 1130.62)]
        [InlineData(300000, 25, 6, PaymentFrequency.Monthly, 1, 1111.11)]
        [InlineData(39176.00, 7, 0, PaymentFrequency.SemiMonthly, 3.79, 265.67)]
        [InlineData(300000, 25, 0, PaymentFrequency.SemiMonthly, 1, 565.21)]
        [InlineData(39176.00, 7, 0, PaymentFrequency.BiWeekly, 3.79, 245.22)]
        [InlineData(300000, 25, 0, PaymentFrequency.BiWeekly, 1, 521.72)]
        [InlineData(39176.00, 7, 0, PaymentFrequency.Weekly, 3.79, 122.57)]
        [InlineData(300000, 25, 0, PaymentFrequency.Weekly, 1, 260.84)]
        public void CalculateLoan_ValidInput_ShouldSucceed(double mortgageAmount,
             int years,
             int months,
             PaymentFrequency paymentFrequency,
             double interestRate,
             double expectedMonthlyPayment)
        {
            // Arrange
            var item = new MortgageDto()
            {
                MortgageAmount = mortgageAmount,
                Years = years,
                Months = months,
                PaymentFrequency = paymentFrequency,
                InterestRate = interestRate,
            };

            // Act
            var response = _service.CalculateMortgage(item);

            // Assert
            response.MonthlyPayment.Should().Be(expectedMonthlyPayment);
        }
    }
}

