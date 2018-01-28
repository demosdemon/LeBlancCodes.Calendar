// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-27-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="IYearlyRecurringEvent.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;

namespace LeBlancCodes.Calendar.Interfaces
{
    /// <summary>
    ///     Interface IYearlyRecurringEvent
    /// </summary>
    public interface IYearlyRecurringEvent : IRecurringEvent, IComparable<IYearlyRecurringEvent>
    {
        /// <summary>
        ///     Gets the date.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>DateTimeOffset.</returns>
        DateTimeOffset GetDate(int year);
    }
}
