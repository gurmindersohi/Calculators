// <copyright file="PaymentFrequency.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Calculator.DataTransferModels.Enums
{
    /// <summary>
    /// Enum for extra payment frequency.
    /// </summary>
	public enum ExtraPaymentFrequency
    {
        /// <summary>
        /// Annually extra payment frequency.
        /// </summary>
        [Display(Name = "Annually")]
        Annually,

        /// <summary>
        /// Semi-Annually extra payment frequency.
        /// </summary>
        [Display(Name = "Semi-Annually")]
        SemiAnnually,

        /// <summary>
        /// Once extra payment frequency.
        /// </summary>
        [Display(Name = "Once")]
        Once,
    }
}

