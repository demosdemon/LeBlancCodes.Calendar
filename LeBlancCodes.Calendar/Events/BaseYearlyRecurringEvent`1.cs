// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-27-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-29-2018
// ***********************************************************************
// <copyright file="BaseRecurringEvent`1.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using LeBlancCodes.Calendar.Interfaces;

namespace LeBlancCodes.Calendar.Events
{
    /// <summary>
    ///     Class BaseYearlyRecurringEvent.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    public abstract class BaseYearlyRecurringEvent<T> : IYearlyRecurringEvent, IEquatable<T> where T : BaseYearlyRecurringEvent<T>
    {
        /// <summary>
        ///     The cache
        /// </summary>
        private readonly IDictionary<int, DateTimeOffset> _cache = new ConcurrentDictionary<int, DateTimeOffset>();

        /// <summary>
        ///     Gets the hash code.
        /// </summary>
        /// <value>The hash code.</value>
        protected abstract int HashCode { get; }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise,
        ///     false.
        /// </returns>
        public abstract bool Equals(T other);

        /// <summary>
        ///     Gets the previous event.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="dto">The dto.</param>
        /// <returns>DateTimeOffset.</returns>
        public virtual DateTimeOffset? GetPreviousOccurrence(IDateTimeFactory factory, DateTimeOffset dto)
        {
            var year = dto.Year;
            while (dto < GetOccurrenceForYear(factory, year)) --year;

            return GetOccurrenceForYear(factory, year);
        }

        /// <summary>
        ///     Gets the next event.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="dto">The dto.</param>
        /// <returns>DateTimeOffset.</returns>
        public virtual DateTimeOffset? GetNextOccurrence(IDateTimeFactory factory, DateTimeOffset dto)
        {
            var year = dto.Year;
            while (GetOccurrenceForYear(factory, year) < dto) ++year;

            return GetOccurrenceForYear(factory, year);
        }

        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets the type of the recurring event.
        /// </summary>
        /// <value>The type of the recurring event.</value>
        public abstract RecurringEventType RecurringEventType { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is indefinite.
        /// </summary>
        /// <value><c>true</c> if this instance is indefinite; otherwise, <c>false</c>.</value>
        public virtual bool IsIndefinite => true;

        /// <summary>
        ///     Gets the tags.
        /// </summary>
        /// <value>The tags.</value>
        public ICollection<string> Tags { get; } = new List<string>();

        /// <summary>
        ///     Gets the examples used for test purposes.
        /// </summary>
        /// <value>The examples.</value>
        public IReadOnlyCollection<DateTimeOffset> Examples { get; protected set; }

        /// <summary>
        /// Gets the earliest occurrence month.
        /// </summary>
        /// <value>The earliest occurrence month.</value>
        public abstract Month EarliestOccurrenceMonth { get; }

        /// <summary>
        ///     Gets the date.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="year">The year.</param>
        /// <returns>DateTimeOffset.</returns>
        public DateTimeOffset GetOccurrenceForYear(IDateTimeFactory factory, int year)
        {
            if (_cache.TryGetValue(year, out var result))
                return result;

            result = GetDate(factory, year);
            _cache[year] = result;
            return result;
        }

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() => HashCode;

        /// <summary>
        ///     Gets the date value.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="year">The year.</param>
        /// <returns>DateTimeOffset.</returns>
        protected abstract DateTimeOffset GetDate(IDateTimeFactory factory, int year);
    }
}
