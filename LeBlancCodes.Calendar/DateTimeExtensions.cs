// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-27-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-29-2018
// ***********************************************************************
// <copyright file="DateTimeExtensions.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;

namespace LeBlancCodes.Calendar
{
    /// <summary>
    ///     Class DateTimeExtensions.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///     Determines whether the specified <see cref="DateTimeOffset" /> is a weekday.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns><c>true</c> if the specified dto is weekday; otherwise, <c>false</c>.</returns>
        public static bool IsWeekday(this DateTimeOffset dto) => dto.DayOfWeek.IsWeekday();

        /// <summary>
        ///     Determines whether the specified <see cref="DayOfWeek" /> is a weekday.
        /// </summary>
        /// <param name="dow">The dow.</param>
        /// <returns><c>true</c> if the specified dow is weekday; otherwise, <c>false</c>.</returns>
        public static bool IsWeekday(this DayOfWeek dow) => DayOfWeek.Sunday < dow && dow < DayOfWeek.Saturday;

        /// <summary>
        ///     Sets the time zone.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="timeZone">The time zone.</param>
        /// <returns>DateTimeOffset.</returns>
        public static DateTimeOffset SetTimeZone(this DateTimeOffset dto, TimeZoneInfo timeZone)
        {
            timeZone.Should().NotBeNull();
            var offset = timeZone.GetUtcOffset(dto.DateTime);
            return new DateTimeOffset(dto.DateTime, offset);
        }

        /// <summary>
        ///     Sets the specified time.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="time">The time.</param>
        /// <returns>DateTimeOffset.</returns>
        public static DateTimeOffset Set(this DateTimeOffset dto, TimeSpan time)
        {
            var date = dto.Date;
            return new DateTimeOffset(date + time, dto.Offset);
        }

        /// <summary>
        ///     Returns a new <see cref="DateTimeOffset" /> with the specified <paramref name="hour" />, <paramref name="minute" />
        ///     , <paramref name="second" />, and <paramref name="millisecond" />. Defaults to midnight.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <param name="hour">The hour.</param>
        /// <param name="minute">The minute.</param>
        /// <param name="second">The second.</param>
        /// <param name="millisecond">The millisecond.</param>
        /// <returns>DateTimeOffset.</returns>
        public static DateTimeOffset Set(this DateTimeOffset dto, int hour = 0, int minute = 0, int second = 0, int millisecond = 0) => Set(dto,
            TimeSpan.FromMilliseconds(millisecond) +
            TimeSpan.FromSeconds(second) +
            TimeSpan.FromMinutes(minute) +
            TimeSpan.FromHours(hour));
    }

    /// <summary>
    ///     Class EnumerableExtensions.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Ases the dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the t key.</typeparam>
        /// <typeparam name="TValue">The type of the t value.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="keyComparer">The key comparer.</param>
        /// <returns>IDictionary&lt;TKey, TValue&gt;.</returns>
        public static IDictionary<TKey, TValue> AsDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, IEqualityComparer<TKey> keyComparer = null) =>
            source.ToDictionary(kvp => kvp.Key, kvp => kvp.Value, keyComparer);
    }
}
