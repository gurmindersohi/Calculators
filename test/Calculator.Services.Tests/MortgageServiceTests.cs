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
        [InlineData(1000, 2, 6, PaymentFrequency.BiWeekly, 10, 18.90, 30.07, 39089.57, 339089.57)]
        [InlineData(1000, 2, 0, PaymentFrequency.BiWeekly, 10, 17.10, 30.07, 39089.57, 339089.57)]
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
        [InlineData(10000, 60, 5, 193.33)]
        public void CalculateMonthlyPayment_ValidInput_ShouldSucceed(double mortgageAmount,
             int months,
             double interestRate,
             double expectedMonthlyPayment)
        {
            //// Arrange

            //// Act
            //var response = _service.CalculatePayment(mortgageAmount, months, interestRate);

            //// Assert
            //response.Should().NotBe(null);
            //response.Should().Be(expectedMonthlyPayment);
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
            var response = _service.TotalPeriod(years, months, paymentFrequency);

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
            var response = _service.CalculateInterestRate(interestRate, paymentFrequency);

            // Assert
            response.Should().NotBe(null);
            response.Should().Be(expectedInterestRate);
        }

        //[Theory]
        //[InlineData(39176.00, 7, 0, PaymentFrequency.Monthly, 3.79, 531.71, 65.33, 5487.68, 44663.68)]
        //[InlineData(300000, 25, 0, PaymentFrequency.Monthly, 1, 1130.62, 130.62, 39185.21, 339185.21)]
        //[InlineData(300000, 25, 6, PaymentFrequency.Monthly, 1, 1111.11, 130.71, 39998.17, 339998.17)]
        //[InlineData(39176.00, 7, 0, PaymentFrequency.SemiMonthly, 3.79, 265.67, 32.48, 5456.95, 44632.95)]
        //[InlineData(300000, 25, 0, PaymentFrequency.SemiMonthly, 1, 565.21, 65.21, 39123.05, 339123.05)]
        //[InlineData(39176.00, 7, 0, PaymentFrequency.BiWeekly, 3.79, 245.22, 29.97, 5454.59, 44630.59)]
        //[InlineData(300000, 25, 0, PaymentFrequency.BiWeekly, 1, 521.72, 60.18, 39118.26, 339118.26)]
        //[InlineData(39176.00, 7, 0, PaymentFrequency.Weekly, 3.79, 122.57, 14.95, 5440.4, 44616.4)]
        //[InlineData(300000, 25, 0, PaymentFrequency.Weekly, 1, 260.84, 30.07, 39089.57, 339089.57)]
        //public void CalculateLoan_ValidInput_ShouldSucceed(double mortgageAmount,
        //     int years,
        //     int months,
        //     PaymentFrequency paymentFrequency,
        //     double interestRate,
        //     double expectedMonthlyPayment,
        //     double expectedMonthlyInterest,
        //     double expectedTotalInterest,
        //     double expectedTotalAmount)
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
        //    response.Success.Should().Be(true);
        //    response.ResponseDto.Payment.Should().Be(expectedMonthlyPayment);
        //    response.ResponseDto.Interest.Should().Be(expectedMonthlyInterest);
        //    response.ResponseDto.TotalInterest.Should().Be(expectedTotalInterest);
        //    response.ResponseDto.TotalAmount.Should().Be(expectedTotalAmount);
        //}
    }
}

