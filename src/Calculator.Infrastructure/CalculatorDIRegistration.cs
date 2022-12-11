// <copyright file="CalculatorDIRegistration.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

namespace Calculator.Infrastructure
{
    using Calculator.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Configure all of calculator services.
    /// </summary>
	public static class CalculatorDIRegistration
    {
        public static IServiceCollection AddCalculatorServices(this IServiceCollection services)
        {
            services.AddServices();
            return services;
        }
    }
}

