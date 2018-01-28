// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-26-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-28-2018
// ***********************************************************************
// <copyright file="YearlyRecurringEventCollection.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using LeBlancCodes.Calendar.Interfaces;

namespace LeBlancCodes.Calendar
{
    /// <summary>
    ///     Class YearlyRecurringEventCollection.
    /// </summary>
    public class YearlyRecurringEventCollection : IYearlyRecurringEventCollection
    {
        /// <summary>
        ///     Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Add(IYearlyRecurringEvent item)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        ///     true if <paramref name="item">item</paramref> is found in the
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Contains(IYearlyRecurringEvent item) => throw new NotImplementedException();

        /// <summary>
        ///     Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1"></see> to an
        ///     <see cref="T:System.Array"></see>, starting at a particular <see cref="T:System.Array"></see> index.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array"></see> that is the destination of the elements
        ///     copied from <see cref="T:System.Collections.Generic.ICollection`1"></see>. The <see cref="T:System.Array"></see>
        ///     must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void CopyTo(IYearlyRecurringEvent[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Removes the first occurrence of a specific object from the
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <returns>
        ///     true if <paramref name="item">item</paramref> was successfully removed from the
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false. This method also returns false if
        ///     <paramref name="item">item</paramref> is not found in the original
        ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool Remove(IYearlyRecurringEvent item) => throw new NotImplementedException();

        /// <summary>
        ///     Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; }

        /// <summary>
        ///     Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly { get; }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the
        ///     collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerator<IYearlyRecurringEvent> GetEnumerator() => throw new NotImplementedException();

        /// <summary>
        ///     Gets the previous event.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>RecurringEventInstance.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public RecurringEventInstance GetPreviousEvent(DateTimeOffset dto) => throw new NotImplementedException();

        /// <summary>
        ///     Gets the next event.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>RecurringEventInstance.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public RecurringEventInstance GetNextEvent(DateTimeOffset dto) => throw new NotImplementedException();

        /// <summary>
        ///     Gets or sets the time zone.
        /// </summary>
        /// <value>The time zone.</value>
        public TimeZoneInfo TimeZone { get; set; } = TimeZoneInfo.Local;

        /// <summary>
        ///     Gets or sets the factory.
        /// </summary>
        /// <value>The factory.</value>
        public IDateTimeFactory Factory { get; set; } = new DateTimeFactory();
    }
}
