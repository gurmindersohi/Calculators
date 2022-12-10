// <copyright file="ILoanService.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

namespace Calculator.Abstractions.Services
{
    using Calculator.DataTransferModels.Loan;

    /// <summary>
    /// Interace defining the methods the loan service contains.
    /// </summary>
    public interface ILoanService
    {
        /// <summary>
        /// Calculates the loan.
        /// </summary>
        ResponseDto CalculateLoan(LoanDto loanDto);
    }
}

