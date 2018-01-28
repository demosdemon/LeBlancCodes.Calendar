// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-27-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="EasterBasedYearlyRecurringEvent.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using LeBlancCodes.Calendar.Interfaces;

namespace LeBlancCodes.Calendar.Events
{
    /// <summary>
    ///     Class EasterBasedYearlyRecurringEvent. This class cannot be inherited.
    /// </summary>
    public sealed class EasterBasedYearlyRecurringEvent : BaseYearlyRecurringEvent<EasterBasedYearlyRecurringEvent>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EasterBasedYearlyRecurringEvent" /> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        /// <param name="offset">The offset.</param>
        public EasterBasedYearlyRecurringEvent(IDateTimeFactory factory, int offset) : base(factory) => Offset = offset;

        /// <summary>
        ///     Gets the offset.
        /// </summary>
        /// <value>The offset.</value>
        public int Offset { get; }

        /// <summary>
        ///     Gets the hash code.
        /// </summary>
        /// <value>The hash code.</value>
        protected override int HashCode
        {
            get
            {
                unchecked
                {
                    var hashCode = 1555522868;
                    hashCode = hashCode * -1521134295 + typeof(EasterBasedYearlyRecurringEvent).GetHashCode();
                    hashCode = hashCode * -1521134295 + Offset.GetHashCode();
                    return hashCode;
                }
            }
        }

        /// <summary>
        ///     Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        ///     true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise,
        ///     false.
        /// </returns>
        public override bool Equals(EasterBasedYearlyRecurringEvent other) => Offset == other?.Offset;

        /// <summary>
        ///     Gets the date value.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>DateTimeOffset.</returns>
        protected override DateTimeOffset GetDateValue(int year)
        {
            var (month, day) = GetEaster(year);
            var easter = Factory.CreateDateTimeOffset(year, month, day);
            return easter.AddDays(Offset);
        }

        /// <summary>
        ///     Called when [compare].
        /// </summary>
        /// <param name="other">The other.</param>
        /// <param name="compare">The compare.</param>
        /// <returns>System.Int32.</returns>
        protected override bool OnCompare(IYearlyRecurringEvent other, out int compare)
        {
            compare = Offset.CompareTo(((EasterBasedYearlyRecurringEvent) other).Offset);
            return true;
        }

        /// <summary>
        ///     Gets the easter.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <returns>System.Int32.</returns>
        private static (int month, int day) GetEaster(int year)
        {
            // Golden Number - 1
            var g = year % 19;
            var c = (int) Math.Floor(year / 100.0);
            // related to Epact
            var h = (int) (c - Math.Floor(c / 4.0) - Math.Floor((8.0 * c + 13.0) / 25.0) + 19 * g + 15) % 30;
            // number of days from 21 March to the Paschal full moon
            var i = (int) (h - Math.Floor(h / 28.0) * (1 - Math.Floor(29.0 / (h + 1.0))) * Math.Floor((21.0 - g) / 11.0));
            // weekday for the Paschal full moon
            var j = (int) (year + Math.Floor(year / 4.0) + i + 2 - c + Math.Floor(c / 4.0)) % 7;
            // number of days from 21 March to the Sunday on or before the Paschal full moon
            var l = i - j;
            var month = (int) (3 + Math.Floor((l + 40.0) / 44.0));
            var day = (int) (l + 28 - 31 * Math.Floor(month / 4.0));

            return (month, day);
        }
    }
}
