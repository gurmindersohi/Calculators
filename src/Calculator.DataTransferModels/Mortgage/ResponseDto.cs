// <copyright file="ResponseDto.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

namespace Calculator.DataTransferModels.Mortgage
{
    // <summary>
    // The response data transfer model for mortgage calculator.
    // </summary>
    public class ResponseDto
    {
        // <summary>
        // Gets or Sets the monthly payment.
        // </summary>
        public double MonthlyPayment { get; set; }

        // <summary>
        // Gets or Sets the monthly interest
        // </summary>
        public double MonthlyInterest { get; set; }

        // <summary>
        // Gets or Sets the total interest.
        // </summary>
        public double TotalInterest { get; set; }

        // <summary>
        // Gets or Sets the total amount.
        // </summary>
        public double TotalAmount { get; set; }
    }
}

