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

        [Theory]
        [InlineData(39176.00, 7, 0, PaymentFrequency.Monthly, 3.79, 531.71, 65.33, 5487.64, 44663.64)]
        [InlineData(300000, 25, 0, PaymentFrequency.Monthly, 1, 1130.62, 130.62, 39186.0, 339186.0)]
        [InlineData(300000, 25, 6, PaymentFrequency.Monthly, 1, 1111.11, 130.72, 39999.66, 339999.66)]
        [InlineData(39176.00, 7, 0, PaymentFrequency.SemiMonthly, 3.79, 265.67, 32.48, 5456.56, 44632.56)]
        [InlineData(300000, 25, 0, PaymentFrequency.SemiMonthly, 1, 565.21, 65.21, 39126.0, 339126.0)]
        [InlineData(39176.00, 7, 0, PaymentFrequency.BiWeekly, 3.79, 245.22, 29.97, 5454.04, 44630.04)]
        [InlineData(300000, 25, 0, PaymentFrequency.BiWeekly, 1, 521.72, 60.18, 39118.0, 339118.0)]
        [InlineData(39176.00, 7, 0, PaymentFrequency.Weekly, 3.79, 122.57, 14.94, 5439.48, 44615.48)]
        [InlineData(300000, 25, 0, PaymentFrequency.Weekly, 1, 260.84, 30.07, 39092.0, 339092.0)]
        public void CalculateLoan_ValidInput_ShouldSucceed(double mortgageAmount,
             int years,
             int months,
             PaymentFrequency paymentFrequency,
             double interestRate,
             double expectedMonthlyPayment,
             double expectedMonthlyInterest,
             double expectedTotalInterest,
             double expectedTotalAmount)
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
            response.Success.Should().Be(true);
            response.ResponseDto.Payment.Should().Be(expectedMonthlyPayment);
            response.ResponseDto.Interest.Should().Be(expectedMonthlyInterest);
            response.ResponseDto.TotalInterest.Should().Be(expectedTotalInterest);
            response.ResponseDto.TotalAmount.Should().Be(expectedTotalAmount);
        }

        [Theory]
        //[InlineData(10000, 60, 5, PaymentFrequency.Monthly, 193.33)]
        [InlineData(39176.00, 84, 3.79, PaymentFrequency.Monthly, 531.71)]
        [InlineData(39176.00, 182, 3.79, PaymentFrequency.BiWeekly, 245.22)]
        public void CalculatePayment_ValidInput_ShouldSucceed(double mortgageAmount,
             int periods,
             double interestRate,
             PaymentFrequency paymentFrequency,
             double expectedPayment)
        {
            // Arrange

            // Act
            var response = _service.CalculatePayment(mortgageAmount, periods, interestRate, paymentFrequency);

            // Assert
            response.Should().NotBe(null);
            response.Should().Be(expectedPayment);
        }

        [Theory]
        [InlineData(10, 0, PaymentFrequency.Monthly, 120)]
        [InlineData(10, 6, PaymentFrequency.Monthly, 126)]
        [InlineData(10, 0, PaymentFrequency.SemiMonthly, 240)]
        [InlineData(10, 6, PaymentFrequency.SemiMonthly, 252)]
        [InlineData(10, 0, PaymentFrequency.BiWeekly, 260)]
        [InlineData(10, 6, PaymentFrequency.BiWeekly, 273)]
        [InlineData(10, 0, PaymentFrequency.Weekly, 520)]
        [InlineData(10, 6, PaymentFrequency.Weekly, 546)]
        public void TotalPeriod_ValidInput_ShouldSucceed(int years,
             int months,
             PaymentFrequency paymentFrequency,
             int expectedTotalPeriod)
        {
            // Arrange

            // Act
            var response = MortgageService.TotalPeriod(years, months, paymentFrequency);

            // Assert
            response.Should().NotBe(null);
            response.Should().Be(expectedTotalPeriod);
        }

        [Theory]
        [InlineData(0, PaymentFrequency.Monthly, 0)]
        [InlineData(0.49, PaymentFrequency.Monthly, 0.0004083333333333333)]
        [InlineData(10, PaymentFrequency.Monthly, 0.008333333333333333)]
        [InlineData(3.79, PaymentFrequency.Monthly, 0.0031583333333333337)]
        [InlineData(10, PaymentFrequency.SemiMonthly, 0.0041666666666666666)]
        [InlineData(3.79, PaymentFrequency.SemiMonthly, 0.0015791666666666669)]
        [InlineData(10, PaymentFrequency.BiWeekly, 0.0038461538461538464)]
        [InlineData(3.79, PaymentFrequency.BiWeekly, 0.0014576923076923078)]
        [InlineData(10, PaymentFrequency.Weekly, 0.0019230769230769232)]
        [InlineData(3.79, PaymentFrequency.Weekly, 0.0007288461538461539)]
        public void CalculateInterestRate_ValidInput_ShouldSucceed(double interestRate,
             PaymentFrequency paymentFrequency,
             double expectedInterestRate)
        {
            // Arrange

            // Act
            var response = MortgageService.CalculateInterestRate(interestRate, paymentFrequency);

            // Assert
            response.Should().NotBe(null);
            response.Should().Be(expectedInterestRate);
        }
    }
}
