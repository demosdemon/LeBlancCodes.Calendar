// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-27-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-28-2018
// ***********************************************************************
// <copyright file="DateTimeFactory.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using JetBrains.Annotations;
using LeBlancCodes.Calendar.Interfaces;

namespace LeBlancCodes.Calendar
{
    /// <summary>
    ///     Class DateTimeFactory.
    /// </summary>
    internal class DateTimeFactory : IDateTimeFactory
    {
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
        public DateTimeOffset CreateDateTimeOffset(int year, int month = 1, int date = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0)
        {
            millisecond = SetValue(millisecond, 0, 1000, ref second);
            second = SetValue(second, 0, 60, ref minute);
            minute = SetValue(minute, 0, 60, ref hour);
            hour = SetValue(hour, 0, 24, ref date);
            date = SetValue(date, 1, () => DateTime.DaysInMonth(year, month), ref month);
            month = SetValue(month, 1, 12, ref year);

            var dt = new DateTime(year, month, date, hour, minute, second, millisecond, DateTimeKind.Unspecified);
            var utc = TimeZone.GetUtcOffset(dt);
            return new DateTimeOffset(dt, utc);
        }

        /// <summary>
        ///     Gets or sets the default time zone the new date objects are scoped to.
        /// </summary>
        /// <value>The time zone.</value>
        public TimeZoneInfo TimeZone { get; set; } = TimeZoneInfo.Local;

        /// <summary>
        ///     Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="parentValue">The parent value.</param>
        /// <returns>System.Int32.</returns>
        private static int SetValue(int value, int minValue, [InstantHandle] Func<int> maxValue, ref int parentValue)
        {
            var direction = value < minValue ? -1 : 1;
            while (value < minValue && maxValue() + minValue <= value)
            {
                parentValue += direction;
                value -= maxValue() * direction;
            }

            return value;
        }

        /// <summary>
        ///     Sets the value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="minValue">The minimum value.</param>
        /// <param name="maxValue">The maximum value.</param>
        /// <param name="parentValue">The parent value.</param>
        /// <returns>System.Int32.</returns>
        private static int SetValue(int value, int minValue, int maxValue, ref int parentValue) => SetValue(value, minValue, () => maxValue, ref parentValue);
    }
}
