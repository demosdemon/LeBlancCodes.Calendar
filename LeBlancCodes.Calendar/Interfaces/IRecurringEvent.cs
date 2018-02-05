// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-28-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-29-2018
// ***********************************************************************
// <copyright file="IRecurringEvent.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace LeBlancCodes.Calendar.Interfaces
{
    /// <summary>
    ///     Interface IRecurringEvent
    /// </summary>
    public interface IRecurringEvent
    {
        /// <summary>
        ///     Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        string Name { get; set; }

        /// <summary>
        ///     Gets the type of the recurring event.
        /// </summary>
        /// <value>The type of the recurring event.</value>
        RecurringEventType RecurringEventType { get; }

        /// <summary>
        ///     Gets the tags.
        /// </summary>
        /// <value>The tags.</value>
        ICollection<string> Tags { get; }

        /// <summary>
        ///     Gets or sets a value indicating whether this instance is indefinite.
        /// </summary>
        /// <value><c>true</c> if this instance is indefinite; otherwise, <c>false</c>.</value>
        bool IsIndefinite { get; }

        /// <summary>
        ///     Gets the examples used for test purposes.
        /// </summary>
        /// <value>The examples.</value>
        IReadOnlyCollection<DateTimeOffset> Examples { get; }

        /// <summary>
        ///     Gets the previous occurrence, or <langword>null</langword> if no previous event exists.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="dto">The dto.</param>
        /// <returns>DateTimeOffset.</returns>
        DateTimeOffset? GetPreviousOccurrence(IDateTimeFactory factory, DateTimeOffset dto);

        /// <summary>
        ///     Gets the next occurrence, or <langword>null</langword> if no next event exists.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="dto">The dto.</param>
        /// <returns>DateTimeOffset.</returns>
        DateTimeOffset? GetNextOccurrence(IDateTimeFactory factory, DateTimeOffset dto);
    }
}
