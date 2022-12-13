// <copyright file="MortgageDto.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

namespace Calculator.DataTransferModels.Mortgage
{
    using System.ComponentModel.DataAnnotations;
    using Calculator.DataTransferModels.Enums;

    // <summary>
    // The data transfer model for mortgage calculator.
    // </summary>
    public class MortgageDto
    {
        // <summary>
        // Gets or Sets the mortgage amount.
        // </summary>
        [Required]
        [Range(0, double.MaxValue)]
        public double MortgageAmount { get; set; }

        // <summary>
        // Gets or Sets the total years of mortgage.
        // </summary>
        [Required]
        [Range(0, int.MinValue)]
        public int Years { get; set; }

        // <summary>
        // Gets or Sets the total months of mortgage.
        // </summary>
        [Required]
        [Range(0, int.MinValue)]
        public int Months { get; set; }

        // <summary>
        // Gets or Sets the mortgage payment frequency.
        // </summary>
        [Required]
        public PaymentFrequency PaymentFrequency { get; set; }

        // <summary>
        // Gets or Sets the mortgage interest rate.
        // </summary>
        [Required]
        [Range(0, double.MaxValue)]
        public double InterestRate { get; set; }
    }
}

