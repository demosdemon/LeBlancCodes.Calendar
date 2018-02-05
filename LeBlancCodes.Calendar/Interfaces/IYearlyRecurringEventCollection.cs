// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-28-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-28-2018
// ***********************************************************************
// <copyright file="IYearlyRecurringEventCollection.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace LeBlancCodes.Calendar.Interfaces
{
    /// <summary>
    ///     Interface IYearlyRecurringEventCollection
    /// </summary>
    public interface IYearlyRecurringEventCollection : ICollection<IYearlyRecurringEvent>
    {
        /// <summary>
        ///     Gets or sets the factory.
        /// </summary>
        /// <value>The factory.</value>
        IDateTimeFactory Factory { get; set; }

        /// <summary>
        ///     Gets the previous event.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>RecurringEventInstance.</returns>
        RecurringEventInstance GetPreviousEvent(DateTimeOffset dto);

        /// <summary>
        ///     Gets the next event.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>RecurringEventInstance.</returns>
        RecurringEventInstance GetNextEvent(DateTimeOffset dto);
    }
}
