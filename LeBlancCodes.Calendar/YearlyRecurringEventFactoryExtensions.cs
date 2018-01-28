// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-28-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-28-2018
// ***********************************************************************
// <copyright file="YearlyRecurringEventFactoryExtensions.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using LeBlancCodes.Calendar.Interfaces;

namespace LeBlancCodes.Calendar
{
    /// <summary>
    ///     Class YearlyRecurringEventFactoryExtensions.
    /// </summary>
    public static class YearlyRecurringEventFactoryExtensions
    {
        /// <summary>
        ///     Gets the nearest weekday.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns>System.Int32.</returns>
        public static int GetNearestWeekday(DayOfWeek dayOfWeek)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (dayOfWeek)
            {
                case DayOfWeek.Saturday:
                    return -1;
                case DayOfWeek.Sunday:
                    return 1;
                default:
                    return 0;
            }
        }

        /// <summary>
        ///     Gets the first of a two day holiday.
        /// </summary>
        /// <param name="dayOfWeek">The day of week.</param>
        /// <returns>System.Int32.</returns>
        public static int GetFirstOfTwoDayHoliday(DayOfWeek dayOfWeek)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (dayOfWeek)
            {
                case DayOfWeek.Friday:
                case DayOfWeek.Saturday:
                    return -1;
                case DayOfWeek.Sunday:
                    return -2;
                default:
                    return 0;
            }
        }

        /// <summary>
        ///     Creates the nearest weekday event.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="month">The month.</param>
        /// <param name="date">The date.</param>
        /// <returns>IYearlyRecurringEvent.</returns>
        public static IYearlyRecurringEvent CreateNearestWeekdayEvent(this IYearlyRecurringEventFactory factory, Month month, int date) =>
            factory.CreateFixedDateEvent(month, date, GetNearestWeekday);

        /// <summary>
        ///     Creates the first of two day holiday.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="month">The month.</param>
        /// <param name="date">The date.</param>
        /// <returns>IYearlyRecurringEvent.</returns>
        public static IYearlyRecurringEvent CreateFirstOfTwoDayHoliday(this IYearlyRecurringEventFactory factory, Month month, int date) =>
            factory.CreateFixedDateEvent(month, date, GetFirstOfTwoDayHoliday);
    }
}
