// ***********************************************************************
// Assembly         : LeBlancCodes.Calendar
// Author           : Brandon LeBlanc
// Created          : 01-26-2018
//
// Last Modified By : Brandon LeBlanc
// Last Modified On : 01-29-2018
// ***********************************************************************
// <copyright file="YearlyRecurringEventCollection.cs" company="Brandon LeBlanc">
//     Copyright © 2018 LeBlanc Codes, LLC
// </copyright>
// <summary></summary>
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using LeBlancCodes.Calendar.Interfaces;

namespace LeBlancCodes.Calendar
{
    /// <summary>
    ///     Class YearlyRecurringEventCollection.
    /// </summary>
    public class YearlyRecurringEventCollection : IYearlyRecurringEventCollection
    {
        /// <summary>
        ///     The events by month
        /// </summary>
        private readonly IReadOnlyList<MonthStore> _eventsByMonth = Enumerable.Range(1, 12)
            .Cast<Month>()
            .Select(x => new MonthStore(x))
            .ToArray();

        /// <summary>
        ///     Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Add(IYearlyRecurringEvent item)
        {
            var store = GetStore(item);
            store.Should().NotBeNull();
            if (store.Add(item))
                ++Count;
        }

        /// <summary>
        ///     Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Clear()
        {
            foreach (var store in _eventsByMonth)
                store.Clear();

            Count = 0;
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
        public bool Contains(IYearlyRecurringEvent item) => GetStore(item).Contains(item);

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
            arrayIndex.Should().BeGreaterOrEqualTo(0);
            array.Should().NotBeNull();
            array.IsReadOnly.Should().BeFalse();
            array.Length.Should().BeGreaterOrEqualTo(Count + arrayIndex);

            foreach (var item in this) array[arrayIndex++] = item;
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
        public bool Remove(IYearlyRecurringEvent item)
        {
            if (GetStore(item).Remove(item))
            {
                --Count;
                return true;
            }

            return false;
        }

        /// <summary>
        ///     Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
        /// </summary>
        /// <value>The count.</value>
        public int Count { get; private set; }

        /// <summary>
        ///     Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> is read-only.
        /// </summary>
        /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
        public bool IsReadOnly => false;

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
        /// <exception cref="System.NotImplementedException"></exception>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerator<IYearlyRecurringEvent> GetEnumerator() => new EventEnumerator(_eventsByMonth, (int) Month.January, Direction.Forward);

        /// <summary>
        ///     Gets the previous event.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>RecurringEventInstance.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public RecurringEventInstance GetPreviousEvent(DateTimeOffset dto) => GetEvent(Direction.Reverse, dto);

        /// <summary>
        ///     Gets the next event.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>RecurringEventInstance.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public RecurringEventInstance GetNextEvent(DateTimeOffset dto) => GetEvent(Direction.Forward, dto);

        /// <summary>
        ///     Gets or sets the factory.
        /// </summary>
        /// <value>The factory.</value>
        public IDateTimeFactory Factory { get; set; } = new DateTimeFactory();

        /// <summary>
        ///     Gets the store.
        /// </summary>
        /// <param name="event">The event.</param>
        /// <returns>MonthStore.</returns>
        private MonthStore GetStore(IYearlyRecurringEvent @event)
        {
            @event.Should().NotBeNull();
            var month = (int) @event.EarliestOccurrenceMonth - 1;
            month.Should().BeInRange(0, 11);
            return _eventsByMonth[month];
        }

        /// <summary>
        ///     Gets the event.
        /// </summary>
        /// <param name="direction">The direction.</param>
        /// <param name="epoch">The epoch.</param>
        /// <returns>RecurringEventInstance.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException"></exception>
        /// <exception cref="System.InvalidOperationException">No event found, is collection empty?</exception>
        private RecurringEventInstance GetEvent(Direction direction, DateTimeOffset epoch)
        {
            using (var enumerator = new EventEnumerator(_eventsByMonth, epoch.Month, direction))
            {
                RecurringEventInstance? result = null;
                var halt = false;

                // ReSharper disable once AccessToModifiedClosure
                enumerator.MonthChanged += (o, e) => halt = result.HasValue;

                while (enumerator.MoveNext())
                {
                    if (halt) break;
                    var dto = enumerator.GetOccurrence(Factory, epoch);
                    if (dto.HasValue && (!result.HasValue || dto.Value < result.Value.EventTime))
                        result = new RecurringEventInstance(enumerator.Current, dto.Value);
                }

                return result ?? throw new InvalidOperationException("No event found, is collection empty?");
            }
        }

        private class EventEnumerator : IEnumerator<IYearlyRecurringEvent>, IComparer<DateTimeOffset>
        {
            private readonly IComparer<DateTimeOffset> _dateTimeComparer;
            private readonly Direction _direction;
            private readonly IReadOnlyList<MonthStore> _eventsByMonths;
            private readonly int _startMonth;
            private IEnumerator<IYearlyRecurringEvent> _innerEnumerator;

            private int _offset = -1;

            public EventEnumerator(IReadOnlyList<MonthStore> eventsByMonths, int startMonth, Direction direction, IComparer<DateTimeOffset> dateTimeComparer = null)
            {
                _eventsByMonths = eventsByMonths;
                _startMonth = startMonth;
                _direction = direction;
                _dateTimeComparer = dateTimeComparer ?? Comparer<DateTimeOffset>.Default;
            }

            private int Index => (_startMonth - 1 + _offset * (int) _direction) % 12;

            public int Compare(DateTimeOffset x, DateTimeOffset y)
            {
                switch (_direction)
                {
                    case Direction.Forward:
                        return _dateTimeComparer.Compare(x, y);
                    case Direction.Reverse:
                        return _dateTimeComparer.Compare(y, x);
                    default:
                        throw new InvalidOperationException();
                }
            }

            public void Dispose()
            {
                _innerEnumerator?.Dispose();
            }

            public void Reset()
            {
                _offset = -1;
            }

            public bool MoveNext()
            {
                while (true)
                {
                    if (_offset < 0) IterateMonths();

                    if (_innerEnumerator.MoveNext()) return true;

                    if (IterateMonths())
                    {
                        OnMonthChanged();
                        continue;
                    }

                    return false;
                }
            }

            object IEnumerator.Current => Current;

            public IYearlyRecurringEvent Current => _innerEnumerator.Current;

            public event EventHandler<MonthChangedEvent> MonthChanged;

            public DateTimeOffset? GetOccurrence(IDateTimeFactory factory, DateTimeOffset epoch)
            {
                _offset.Should().BeGreaterOrEqualTo(0);
                Current.Should().NotBeNull();

                switch (_direction)
                {
                    case Direction.Forward:
                        return Current.GetNextOccurrence(factory, epoch);
                    case Direction.Reverse:
                        return Current.GetPreviousOccurrence(factory, epoch);
                    default:
                        throw new InvalidOperationException();
                }
            }

            private MonthStore GetMonthStore() => _eventsByMonths[Index];

            private bool IterateMonths()
            {
                _innerEnumerator?.Dispose();
                ++_offset;
                if (12 < _offset)
                    return false;

                _innerEnumerator = GetMonthStore().GetEnumerator();
                return true;
            }

            private void OnMonthChanged()
            {
                var month = Index + 1;
                MonthChanged?.Invoke(this, new MonthChangedEvent(month));
            }
        }

        private sealed class MonthChangedEvent : EventArgs
        {
            public MonthChangedEvent(int month) => Month = month;

            public int Month { get; }

            public Month MonthValue => (Month) (Month + 1);
        }

        /// <summary>
        ///     Enum Direction
        /// </summary>
        private enum Direction
        {
            /// <summary>
            ///     The reverse
            /// </summary>
            Reverse = -1,

            /// <summary>
            ///     The forward
            /// </summary>
            Forward = 1,
        }

        /// <summary>
        ///     Struct MonthStore
        /// </summary>
        private struct MonthStore : ISet<IYearlyRecurringEvent>
        {
            /// <summary>
            ///     The events
            /// </summary>
            private readonly HashSet<IYearlyRecurringEvent> _events;

            /// <summary>
            ///     The month
            /// </summary>
            public readonly Month Month;

            /// <summary>
            ///     Initializes a new instance of the <see cref="MonthStore" /> struct.
            /// </summary>
            /// <param name="month">The month.</param>
            public MonthStore(Month month)
            {
                _events = new HashSet<IYearlyRecurringEvent>();
                Month = month;
            }

            /// <summary>
            ///     Returns an enumerator that iterates through the collection.
            /// </summary>
            /// <returns>An enumerator that can be used to iterate through the collection.</returns>
            public IEnumerator<IYearlyRecurringEvent> GetEnumerator() => _events.GetEnumerator();

            /// <summary>
            ///     Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>
            ///     An <see cref="T:System.Collections.IEnumerator"></see> object that can be used to iterate through the
            ///     collection.
            /// </returns>
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            /// <summary>
            ///     Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
            /// </summary>
            /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
            void ICollection<IYearlyRecurringEvent>.Add(IYearlyRecurringEvent item) => _events.Add(item);

            /// <summary>
            ///     Removes all elements in the specified collection from the current set.
            /// </summary>
            /// <param name="other">The collection of items to remove from the set.</param>
            public void ExceptWith(IEnumerable<IYearlyRecurringEvent> other) => _events.ExceptWith(other);

            /// <summary>
            ///     Modifies the current set so that it contains only elements that are also in a specified collection.
            /// </summary>
            /// <param name="other">The collection to compare to the current set.</param>
            public void IntersectWith(IEnumerable<IYearlyRecurringEvent> other) => _events.IntersectWith(other);

            /// <summary>
            ///     Determines whether the current set is a proper (strict) subset of a specified collection.
            /// </summary>
            /// <param name="other">The collection to compare to the current set.</param>
            /// <returns>true if the current set is a proper subset of <paramref name="other">other</paramref>; otherwise, false.</returns>
            public bool IsProperSubsetOf(IEnumerable<IYearlyRecurringEvent> other) => _events.IsProperSubsetOf(other);

            /// <summary>
            ///     Determines whether the current set is a proper (strict) superset of a specified collection.
            /// </summary>
            /// <param name="other">The collection to compare to the current set.</param>
            /// <returns>true if the current set is a proper superset of <paramref name="other">other</paramref>; otherwise, false.</returns>
            public bool IsProperSupersetOf(IEnumerable<IYearlyRecurringEvent> other) => _events.IsProperSupersetOf(other);

            /// <summary>
            ///     Determines whether a set is a subset of a specified collection.
            /// </summary>
            /// <param name="other">The collection to compare to the current set.</param>
            /// <returns>true if the current set is a subset of <paramref name="other">other</paramref>; otherwise, false.</returns>
            public bool IsSubsetOf(IEnumerable<IYearlyRecurringEvent> other) => _events.IsSubsetOf(other);

            /// <summary>
            ///     Determines whether the current set is a superset of a specified collection.
            /// </summary>
            /// <param name="other">The collection to compare to the current set.</param>
            /// <returns>true if the current set is a superset of <paramref name="other">other</paramref>; otherwise, false.</returns>
            public bool IsSupersetOf(IEnumerable<IYearlyRecurringEvent> other) => _events.IsSupersetOf(other);

            /// <summary>
            ///     Determines whether the current set overlaps with the specified collection.
            /// </summary>
            /// <param name="other">The collection to compare to the current set.</param>
            /// <returns>
            ///     true if the current set and <paramref name="other">other</paramref> share at least one common element;
            ///     otherwise, false.
            /// </returns>
            public bool Overlaps(IEnumerable<IYearlyRecurringEvent> other) => _events.Overlaps(other);

            /// <summary>
            ///     Determines whether the current set and the specified collection contain the same elements.
            /// </summary>
            /// <param name="other">The collection to compare to the current set.</param>
            /// <returns>true if the current set is equal to <paramref name="other">other</paramref>; otherwise, false.</returns>
            public bool SetEquals(IEnumerable<IYearlyRecurringEvent> other) => _events.SetEquals(other);

            /// <summary>
            ///     Modifies the current set so that it contains only elements that are present either in the current set or in the
            ///     specified collection, but not both.
            /// </summary>
            /// <param name="other">The collection to compare to the current set.</param>
            public void SymmetricExceptWith(IEnumerable<IYearlyRecurringEvent> other) => _events.SymmetricExceptWith(other);

            /// <summary>
            ///     Modifies the current set so that it contains all elements that are present in the current set, in the specified
            ///     collection, or in both.
            /// </summary>
            /// <param name="other">The collection to compare to the current set.</param>
            public void UnionWith(IEnumerable<IYearlyRecurringEvent> other) => _events.UnionWith(other);

            /// <summary>
            ///     Adds an element to the current set and returns a value to indicate if the element was successfully added.
            /// </summary>
            /// <param name="item">The element to add to the set.</param>
            /// <returns>true if the element is added to the set; false if the element is already in the set.</returns>
            public bool Add(IYearlyRecurringEvent item) => _events.Add(item);

            /// <summary>
            ///     Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1"></see>.
            /// </summary>
            public void Clear() => _events.Clear();

            /// <summary>
            ///     Determines whether the <see cref="T:System.Collections.Generic.ICollection`1"></see> contains a specific value.
            /// </summary>
            /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1"></see>.</param>
            /// <returns>
            ///     true if <paramref name="item">item</paramref> is found in the
            ///     <see cref="T:System.Collections.Generic.ICollection`1"></see>; otherwise, false.
            /// </returns>
            public bool Contains(IYearlyRecurringEvent item) => _events.Contains(item);

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
            public void CopyTo(IYearlyRecurringEvent[] array, int arrayIndex) => _events.CopyTo(array, arrayIndex);

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
            public bool Remove(IYearlyRecurringEvent item) => _events.Remove(item);

            /// <summary>
            ///     Gets the count.
            /// </summary>
            /// <value>The count.</value>
            public int Count => _events.Count;

            /// <summary>
            ///     Gets a value indicating whether this instance is read only.
            /// </summary>
            /// <value><c>true</c> if this instance is read only; otherwise, <c>false</c>.</value>
            public bool IsReadOnly => ((ICollection<IYearlyRecurringEvent>) _events).IsReadOnly;
        }
    }
}
