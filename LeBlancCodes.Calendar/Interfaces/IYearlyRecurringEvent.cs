// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-27-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-29-2018
// ***********************************************************************
// <copyright file="IYearlyRecurringEvent.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace LeBlancCodes.Calendar.Interfaces
{
    /// <summary>
    ///     Enum RecurringEventType
    /// </summary>
    public enum RecurringEventType
    {
        /// <summary>
        ///     The undefined
        /// </summary>
        Undefined,

        /// <summary>
        ///     The fixed date based yearly recurring event
        /// </summary>
        FixedDateBasedYearlyRecurringEvent,

        /// <summary>
        ///     The month week based yearly recurring event
        /// </summary>
        MonthWeekBasedYearlyRecurringEvent,

        /// <summary>
        ///     The easter based yearly recurring event
        /// </summary>
        EasterBasedYearlyRecurringEvent,

        /// <summary>
        ///     The reference based yearly recurring event
        /// </summary>
        ReferenceBasedYearlyRecurringEvent
    }

    /// <summary>
    ///     Interface IYearlyRecurringEvent
    /// </summary>
    public interface IYearlyRecurringEvent : IRecurringEvent
    {
        /// <summary>
        ///     Gets the occurrence for the specified year.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="year">The year.</param>
        /// <returns>DateTimeOffset.</returns>
        DateTimeOffset GetOccurrenceForYear(IDateTimeFactory factory, int year);

        /// <summary>
        /// Gets the earliest occurrence month.
        /// </summary>
        /// <value>The earliest occurrence month.</value>
        Month EarliestOccurrenceMonth { get; }
    }
}
