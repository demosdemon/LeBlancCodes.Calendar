// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-28-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-28-2018
// ***********************************************************************
// <copyright file="IYearlyRecurringEventFactory.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using JetBrains.Annotations;

namespace LeBlancCodes.Calendar.Interfaces
{
    /// <summary>
    ///     Interface IYearlyRecurringEventFactory
    /// </summary>
    public interface IYearlyRecurringEventFactory
    {
        /// <summary>
        ///     Gets or sets the factory.
        /// </summary>
        /// <value>The factory.</value>
        [NotNull]
        IDateTimeFactory Factory { get; set; }

        /// <summary>
        ///     Creates the easter based event.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <returns>IYearlyRecurringEvent.</returns>
        IYearlyRecurringEvent CreateEasterBasedEvent(int offset);

        /// <summary>
        ///     Creates the week based event.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="week">The week.</param>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns>IYearlyRecurringEvent.</returns>
        IYearlyRecurringEvent CreateWeekBasedEvent(Month month, int week, DayOfWeek dayOfWeek);

        /// <summary>
        ///     Creates the fixed date event.
        /// </summary>
        /// <param name="month">The month.</param>
        /// <param name="date">The date.</param>
        /// <param name="observance">The observance.</param>
        /// <returns>IYearlyRecurringEvent.</returns>
        IYearlyRecurringEvent CreateFixedDateEvent(Month month, int date, Func<DayOfWeek, int> observance);

        /// <summary>
        ///     Creates the relative event.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>IYearlyRecurringEvent.</returns>
        IYearlyRecurringEvent CreateRelativeEvent(IYearlyRecurringEvent @event, int offset);
    }
}
