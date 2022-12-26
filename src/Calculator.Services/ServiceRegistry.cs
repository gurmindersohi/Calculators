// <copyright file="ServiceRegistry.cs" company="Sohi"
// Copyright (c) Sohi. All rights reserved.
// </copyright>

namespace Calculator.Services
{
    using Calculator.Abstractions.Services;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Register the necessary dependency injection items for the calculator.
    /// </summary>
    public static class ServiceRegistry
    {
        /// <summary>
        /// Configure the calculator services.
        /// </summary>
        /// <param name="services">A <see cref="IServiceCollection"/> to add the calculator services to.</param>
        /// <param name="configuration">A <see cref="IConfiguration"/> to add the calculator services to.</param>
        /// <returns>Service collection.</returns>
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            //configuration.NotNull(nameof(configuration));

            services.AddTransient(typeof(ILoanService), typeof(LoanService));
            services.AddTransient(typeof(IMortgageService), typeof(MortgageService));

            return services;
        }
    }
}