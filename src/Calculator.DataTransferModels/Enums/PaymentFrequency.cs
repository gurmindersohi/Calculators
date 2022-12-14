// <copyright file="PaymentFrequency.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

namespace Calculator.DataTransferModels.Enums
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Xml.Linq;

    /// <summary>
    /// Enum for mortgage payment frequency.
    /// </summary>
	public enum PaymentFrequency
    {
        /// <summary>
        /// Monthly mortgage payment frequency.
        /// </summary>
        [Display(Name = "Monthly")]
        Monthly,

        /// <summary>
        /// Semi-Monthly mortgage payment frequency.
        /// </summary>
        [Display(Name = "Semi-Monthly")]
        SemiMonthly,

        /// <summary>
        /// Bi-Weekly mortgage payment frequency.
        /// </summary>
        [Display(Name = "Bi-Weekly")]
        BiWeekly,

        /// <summary>
        /// Weekly mortgage payment frequency.
        /// </summary>
        [Display(Name = "Weekly")]
        Weekly
    }
}
