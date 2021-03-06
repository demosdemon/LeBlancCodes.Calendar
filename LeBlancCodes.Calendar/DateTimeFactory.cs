﻿// ***********************************************************************
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
        internal static readonly IDateTimeFactory Instance = new DateTimeFactory();

        public DateTimeOffset CreateDateTimeThisYear(Month month, int date = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0) =>
            CreateDateTimeOffset(DateTimeOffset.Now.Year, month, date, hour, minute, second, millisecond);

        public DateTimeOffset CreateDateTimeThisYear(int month, int date = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0) =>
            CreateDateTimeOffset(DateTimeOffset.Now.Year, month, date, hour, minute, second, millisecond);

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
        /// <exception cref="System.NotImplementedException"></exception>
        public DateTimeOffset CreateDateTimeOffset(int year, Month month, int date = 1, int hour = 0, int minute = 0, int second = 0, int millisecond = 0) =>
            CreateDateTimeOffset(year, (int) month, date, hour, minute, second, millisecond);

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
            millisecond = SetValue(millisecond, 0, 1000, second, s => second = s);
            second = SetValue(second, 0, 60, minute, m => minute = m);
            minute = SetValue(minute, 0, 60, hour, h => hour = h);
            hour = SetValue(hour, 0, 24, date, d => date = d);
            date = SetValue(date, 1, () => DateTime.DaysInMonth(year, month), month, m => month = SetValue(m, 1, 12, year, y => year = y));

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
        /// <param name="setParent">The set parent.</param>
        /// <returns>System.Int32.</returns>
        private static int SetValue(int value, int minValue, [InstantHandle] Func<int> maxValue, int parentValue, Func<int, int> setParent)
        {
            var direction = value < minValue ? -1 : 1;
            while (value < minValue || maxValue() + minValue <= value)
            {
                parentValue = setParent(parentValue + direction);
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
        /// <param name="setParent">The set parent.</param>
        /// <returns>System.Int32.</returns>
        private static int SetValue(int value, int minValue, int maxValue, int parentValue, Func<int, int> setParent) =>
            SetValue(value, minValue, () => maxValue, parentValue, setParent);
    }
}
