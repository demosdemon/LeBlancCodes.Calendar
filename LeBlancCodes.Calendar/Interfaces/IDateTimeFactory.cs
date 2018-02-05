// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-27-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="IDateTimeFactory.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace LeBlancCodes.Calendar.Interfaces
{
    /// <summary>
    ///     Interface IDateTimeFactory
    /// </summary>
    public interface IDateTimeFactory
    {
        /// <summary>
        ///     Gets or sets the default time zone the new date objects are scoped to.
        /// </summary>
        /// <value>The time zone.</value>
        TimeZoneInfo TimeZone { get; set; }

        DateTimeOffset CreateDateTimeThisYear(Month month, int date = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0);

        DateTimeOffset CreateDateTimeThisYear(int month, int date = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0);

        /// <summary>
        ///     Creates the date time offset.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month.</param>
        /// <param name="date">The date.</param>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        /// <param name="second">The second.</param>
        /// <param name="millisecond">The millisecond.</param>
        /// <returns>DateTimeOffset.</returns>
        DateTimeOffset CreateDateTimeOffset(int year, Month month, int date = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0);

        /// <summary>
        ///     Creates the date time offset.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month, default 1 (January).</param>
        /// <param name="date">The date, default 1.</param>
        /// <param name="hour">The hour, default 0 (12 am).</param>
        /// <param name="minute">The minute, default 0.</param>
        /// <param name="second">The second, default 0.</param>
        /// <param name="millisecond">The millisecond, default 0.</param>
        /// <returns>DateTimeOffset.</returns>
        DateTimeOffset CreateDateTimeOffset(int year, int month = 1, int date = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0);
    }
}
