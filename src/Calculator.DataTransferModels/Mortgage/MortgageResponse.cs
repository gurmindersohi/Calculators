// <copyright file="MortgageResponse.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

namespace Calculator.DataTransferModels.Mortgage
{
    using Calculator.DataTransferModels.Response;

    // <summary>
    // The response data transfer model for mortgage calculator.
    // </summary>
    public class MortgageResponse : BaseResponse
    {
        // <summary>
        // Gets or Sets the response dto.
        // </summary>
        public ResponseDto ResponseDto { get; private set; }

        private MortgageResponse(bool success, string message, ResponseDto responseDto) : base(success, message)
        {
            ResponseDto = responseDto;
        }

        public MortgageResponse(ResponseDto responseDto) : this(true, string.Empty, responseDto)
        { }

        public MortgageResponse(string message) : this(false, message, null)
        { }
    }
}