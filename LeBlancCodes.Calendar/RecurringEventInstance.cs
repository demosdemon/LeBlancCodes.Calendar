// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-28-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-28-2018
// ***********************************************************************
// <copyright file="RecurringEventInstance.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using LeBlancCodes.Calendar.Interfaces;

namespace LeBlancCodes.Calendar
{
    /// <summary>
    ///     Struct RecurringEventInstance
    /// </summary>
    public struct RecurringEventInstance
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RecurringEventInstance" /> struct.
        /// </summary>
        /// <param name="recurringEvent">The recurring event.</param>
        /// <param name="eventTime">The event time.</param>
        public RecurringEventInstance(IRecurringEvent recurringEvent, DateTimeOffset eventTime)
        {
            RecurringEvent = recurringEvent;
            EventTime = eventTime;
        }

        /// <summary>
        ///     The recurring event
        /// </summary>
        public readonly IRecurringEvent RecurringEvent;

        /// <summary>
        ///     The event time
        /// </summary>
        public readonly DateTimeOffset EventTime;
    }
}
