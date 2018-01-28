// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-27-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-28-2018
// ***********************************************************************
// <copyright file="BaseRecurringEvent`1.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
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
        private readonly SharedCollection<int, DateTimeOffset> _cache;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseYearlyRecurringEvent{T}" /> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        protected BaseYearlyRecurringEvent(IDateTimeFactory factory)
        {
            _cache = new SharedCollection<int, DateTimeOffset>(GetDateValue);
            Factory = factory;
        }

        /// <summary>
        ///     Gets the dto comparer.
        /// </summary>
        /// <value>The dto comparer.</value>
        private static IComparer<DateTimeOffset> DtoComparer => Comparer<DateTimeOffset>.Default;

        /// <summary>
        ///     Gets the hash code.
        /// </summary>
        /// <value>The hash code.</value>
        protected abstract int HashCode { get; }

        /// <summary>
        ///     Compares to.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns>System.Int32.</returns>
        public int CompareTo(IYearlyRecurringEvent other)
        {
            if (other is null) return 1;
            if (OnCompare(other, out var virt))
                return virt;

            // in an extremely rare chance, year could change between invocations, so lets make sure its the same for both calls
            var year = DateTimeOffset.UtcNow.Year;
            return DtoComparer.Compare(GetDate(year), other.GetDate(year));
        }

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
        /// <param name="dto">The dto.</param>
        /// <returns>DateTimeOffset.</returns>
        public virtual DateTimeOffset GetPreviousEvent(DateTimeOffset dto)
        {
            var year = dto.Year;
            while (dto < GetDate(year)) --year;

            return GetDate(year);
        }

        /// <summary>
        ///     Gets the next event.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>DateTimeOffset.</returns>
        public virtual DateTimeOffset GetNextEvent(DateTimeOffset dto)
        {
            var year = dto.Year;
            while (GetDate(year) < dto) ++year;

            return GetDate(year);
        }

        /// <summary>
        ///     Gets the date time factory.
        /// </summary>
        /// <value>The date time factory.</value>
        public IDateTimeFactory Factory { get; set; }

        /// <summary>
        ///     Gets the date.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>DateTimeOffset.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public DateTimeOffset GetDate(int year) => _cache.GetValue(year);

        /// <summary>
        ///     Returns a hash code for this instance.
        /// </summary>
        /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
        public override int GetHashCode() => HashCode;

        /// <summary>
        ///     Gets the date value.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>DateTimeOffset.</returns>
        protected abstract DateTimeOffset GetDateValue(int year);

        /// <summary>
        ///     Called when [compare].
        /// </summary>
        /// <param name="other">The other.</param>
        /// <param name="compare">The compare.</param>
        /// <returns>System.Int32.</returns>
        protected virtual bool OnCompare(IYearlyRecurringEvent other, out int compare)
        {
            compare = 0;
            return false;
        }
    }
}
