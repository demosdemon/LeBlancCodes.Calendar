// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-28-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-28-2018
// ***********************************************************************
// <copyright file="IRecurringEvent.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace LeBlancCodes.Calendar.Interfaces
{
    /// <summary>
    ///     Interface IRecurringEvent
    /// </summary>
    public interface IRecurringEvent
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
        /// <returns>DateTimeOffset.</returns>
        DateTimeOffset GetPreviousEvent(DateTimeOffset dto);

        /// <summary>
        ///     Gets the next event.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>DateTimeOffset.</returns>
        DateTimeOffset GetNextEvent(DateTimeOffset dto);
    }
}
