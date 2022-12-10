// <copyright file="CalculateDto.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

namespace Calculator.DataTransferModels.Loan
{
    using System.ComponentModel.DataAnnotations;

    // <summary>
    // The data transfer model for loan calculator.
    // </summary>
    public class LoanDto
    {
        // <summary>
        // Gets or Sets the amount.
        // </summary>
        [Required]
        [Range(0, double.MaxValue)]
        public double Principal { get; set; }

        // <summary>
        // Gets or Sets the loan rate.
        // </summary>
        [Required]
        [Range(0, double.MaxValue)]
        public double Rate { get; set; }

        // <summary>
        // Gets or Sets the total years.
        // </summary>
        [Required]
        [Range(0, int.MaxValue)]
        public int Years { get; set; }

        // <summary>
        // Gets or Sets the total months.
        // </summary>
        [Required]
        [Range(0, int.MaxValue)]
        public int Months { get; set; }
    }
}

