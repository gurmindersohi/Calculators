// <copyright file="LoanServiceTests.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

namespace Calculator.Services.Tests
{
    using System.Security.Principal;
    using Calculator.DataTransferModels.Loan;
    using FluentAssertions;

    /// <summary>
    /// Test Case for loan calculator service.
    /// </summary>
    public class LoanServiceTests
    {
        private readonly LoanService _service;

        public LoanServiceTests()
        {
            _service = new LoanService();
        }

        [Theory]
        [InlineData(1000, 10, 2, 0, 46.14, 4.48, 107.48, 1107.48)]
        [InlineData(300000, 3.5, 30, 0, 1347.13, 513.80, 184968.26, 484968.26)]
        [InlineData(750000, 5.99, 30, 1, 4487.35, 2409.79, 869935.0, 1619935.0)]
        [InlineData(999797, 5.89, 25, 0, 6374.65, 3041.99, 912597.0, 1912394.0)]
        [InlineData(1800000, 6.49, 25, 6, 12047.61, 6165.25, 1886567.47, 3686567.47)]
        public void CalculateLoan_ValidInput_ShouldSucceed(double principal,
             float rate,
             int years,
             int months,
             double expectedMonthlyPayment,
             double expectedMonthlyInterest,
             double expectedTotalInterest,
             double expectedTotalAmount)
        {
            // Arrange
            var item = new LoanDto()
            {
                Principal = principal,
                Rate = rate,
                Years = years,
                Months = months
            };

            // Act
            var response = _service.CalculateLoan(item);

            // Assert
            response.MonthlyPayment.Should().Be(expectedMonthlyPayment);
            response.MonthlyInterest.Should().Be(expectedMonthlyInterest);
            response.TotalInterest.Should().Be(expectedTotalInterest);
            response.TotalAmount.Should().Be(expectedTotalAmount);
        }
    }
}

