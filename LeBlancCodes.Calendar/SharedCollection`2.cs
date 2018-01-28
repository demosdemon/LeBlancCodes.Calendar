// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-27-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-27-2018
// ***********************************************************************
// <copyright file="SharedCollection`2.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections.Generic;

namespace LeBlancCodes.Calendar
{
    /// <summary>
    ///     Class SharedCollection.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public class SharedCollection<TKey, TValue>
    {
        /// <summary>
        ///     The factory
        /// </summary>
        private readonly Func<TKey, TValue> _factory;

        /// <summary>
        ///     The lock
        /// </summary>
        private readonly object _lock = new object();

        /// <summary>
        ///     The values
        /// </summary>
        private readonly IDictionary<TKey, TValue> _values = new Dictionary<TKey, TValue>();

        /// <summary>
        ///     Initializes a new instance of the <see cref="SharedCollection{TKey, TValue}" /> class.
        /// </summary>
        /// <param name="factory">The factory.</param>
        public SharedCollection(Func<TKey, TValue> factory) => _factory = factory;

        /// <summary>
        ///     Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>TValue.</returns>
        public TValue GetValue(TKey key)
        {
            TValue value;
            lock (_lock)
            {
                if (_values.TryGetValue(key, out value))
                    return value;
            }

            value = _factory(key);
            lock (_lock) _values[key] = value;

            return value;
        }

        /// <summary>
        ///     Determines whether the shared collection contains the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if the specified key contains key; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(TKey key)
        {
            lock (_lock) return _values.ContainsKey(key);
        }
    }
}
