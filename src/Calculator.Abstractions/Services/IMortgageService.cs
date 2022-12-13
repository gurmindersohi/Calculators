// <copyright file="IMortgageService.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

namespace Calculator.Abstractions.Services
{
    using Calculator.DataTransferModels.Mortgage;

    /// <summary>
    /// Interace defining the methods the mortgage service contains.
    /// </summary>
    public interface IMortgageService
    {
        /// <summary>
        /// Calculates the mortgage.
        /// </summary>
        ResponseDto CalculateMortgage(MortgageDto mortgageDto);
    }
}