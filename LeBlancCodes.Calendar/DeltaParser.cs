// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-27-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="DeltaParser.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace LeBlancCodes.Calendar
{
    /// <summary>
    ///     Class DeltaParser.
    /// </summary>
    public static class DeltaParser
    {
        /// <summary>
        ///     The delta
        /// </summary>
        private static readonly Regex Delta;

        /// <summary>
        ///     Initializes static members of the <see cref="DeltaParser" /> class.
        /// </summary>
        static DeltaParser()
        {
            var signs = new[] {"+", "-"};
            var tokens = new[] {"days", "day", "d", "hours", "hour", "h", "minutes", "minute", "m", "seconds", "second", "s"};

            var regex = $"({OrGroup("sign", signs)}?(?<amount>\\d+)(\\s*{OrGroup("token", tokens)}))";
            Delta = new Regex(regex, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);
        }

        /// <summary>
        ///     Parses the delta.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>TimeSpan.</returns>
        public static TimeSpan ParseDelta(string input)
        {
            var totalSeconds = Delta.Matches(input).OfType<Match>().Select(match => new
            {
                negative = match.Groups["sign"].Value == "-",
                amount = int.Parse(match.Groups["amount"].Value),
                multiplier = GetMultiplier(match.Groups["token"].Value)
            }).Sum(x => (x.negative ? -1 : 1) * x.amount * x.multiplier);

            return TimeSpan.FromSeconds(totalSeconds);
        }

        /// <summary>
        ///     Ors the group.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="tokens">The tokens.</param>
        /// <returns>System.String.</returns>
        private static string OrGroup(string name, IEnumerable<string> tokens) => $"(?<{name}>{string.Join("|", tokens.Select(Regex.Escape))})";

        /// <summary>
        ///     Gets the multiplier.
        /// </summary>
        /// <param name="token">The token.</param>
        /// <returns>System.Int32.</returns>
        private static int GetMultiplier(string token)
        {
            switch (token.ToLowerInvariant())
            {
                case "days":
                case "day":
                case "d":
                    return 24 * 60 * 60;
                case "hours":
                case "hour":
                case "h":
                    return 60 * 60;
                case "minutes":
                case "minute":
                case "m":
                    return 60;
                case "seconds":
                case "second":
                case "s":
                    return 1;
                default: return 0;
            }
        }
    }
}
