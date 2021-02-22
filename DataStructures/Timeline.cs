/*
    Author: Lorenzo Lotti
    Name: Timeline (DataStructures.Timeline<TValue>)
    Type: Data structure (class)
    Description: A collection of dates/times and values sorted by dates/times easy to query.
    Usage:
        this data structure can be used to represent an ordered series of dates or times with which to associate values.
        An example is a chronology of events:
            306: Constantine is the new emperor,
            312: Battle of the Milvian Bridge,
            313: Edict of Milan,
            330: Constantine move the capital to Constantinople.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures
{
    /// <summary>
    /// A collection of <see cref="DateTime"/> and <see cref="TValue"/> sorted by <see cref="DateTime"/> field.
    /// </summary>
    /// <typeparam name="TValue">Value associated with a <see cref="DateTime"/>.</typeparam>
    public class Timeline<TValue> :
        ICollection<(DateTime Time, TValue Value)>,
        IEquatable<Timeline<TValue>>
    {
        private List<(DateTime Time, TValue Value)> timeline = new ();

        public Timeline()
        {
            // todo: improve performance and consider removing unnecessary methods
            timeline = new List<(DateTime, TValue)>();
        }

        public Timeline(DateTime time, TValue value)
        {
            timeline = new List<(DateTime, TValue)> { (time, value) };
        }

        public Timeline(params TValue[] value)
        {
            var now = DateTime.Now;
            foreach (var v in value)
            {
                timeline.Add((now, v));
            }
        }

        public Timeline(params (DateTime, TValue)[] timeline)
        {
            this.timeline = timeline.ToList();
            this.timeline = this.timeline.OrderBy(pair => pair.Time).ToList();
        }

        bool ICollection<(DateTime Time, TValue Value)>.IsReadOnly => false;

        /// <summary>
        /// Gets the count of pairs.
        /// </summary>
        public int Count => timeline.Count;

        public int TimesCount => GetAllTimes().Length;

        public int ValuesCount => GetAllValues().Length;

        /// <summary>
        /// Get all values associated with <paramref name="time"/>.
        /// </summary>
        /// <param name="time">Time to get values for.</param>
        /// <returns>Values associated with <paramref name="time"/>.</returns>
        public TValue[] this[DateTime time]
        {
            get => GetValuesByTime(time);
            set
            {
                for (var i = 0; i < Count; i++)
                {
                    if (timeline[i].Time == time)
                    {
                        timeline.RemoveAt(i);
                    }
                }

                foreach (var v in value)
                {
                    Add(time, v);
                }
            }
        }

        public static bool operator ==(Timeline<TValue> left, Timeline<TValue> right)
        {
            var leftArray = left.ToArray();
            var rightArray = right.ToArray();
            if (leftArray.Length == rightArray.Length)
            {
                for (var i = 0; i < leftArray.Length; i++)
                {
                    if (leftArray[i].Time != rightArray[i].Time && !leftArray[i].Value!.Equals(rightArray[i].Value))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public static bool operator !=(Timeline<TValue> left, Timeline<TValue> right) => !(left == right);

        public bool Equals(Timeline<TValue>? other) => other is not null && this == other;

        public void Clear() => timeline.Clear();

        /// <summary>
        /// Copy a value to an array.
        /// </summary>
        /// <param name="array">Destination array.</param>
        /// <param name="arrayIndex">The start index.</param>
        public void CopyTo((DateTime, TValue)[] array, int arrayIndex)
        {
            timeline.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Get all <see cref="DateTime"/> of the timeline.
        /// </summary>
        public DateTime[] GetAllTimes() => timeline.Select(t => t.Time).Distinct().ToArray();

        /// <summary>
        /// Get <see cref="DateTime"/> values of the timeline that have this <paramref name="value"/>.
        /// </summary>
        public DateTime[] GetTimesByValue(TValue value) =>
            timeline.Where(pair => pair.Value!.Equals(value)).Select(pair => pair.Time).ToArray();

        /// <summary>
        /// Get all <see cref="DateTime"/> before <paramref name="time"/>.
        /// </summary>
        public DateTime[] GetTimesBefore(DateTime time) => GetAllTimes().Where(t => t < time).OrderBy(t => t).ToArray();

        /// <summary>
        /// Get all <see cref="DateTime"/> after <paramref name="time"/>.
        /// </summary>
        public DateTime[] GetTimesAfter(DateTime time) => GetAllTimes().Where(t => t > time).OrderBy(t => t).ToArray();

        /// <summary>
        /// Get all <see cref="TValue"/> of the timeline.
        /// </summary>
        public TValue[] GetAllValues() => timeline.Select(pair => pair.Value).ToArray();

        /// <summary>
        /// Get all <see cref="TValue"/> associated with <paramref name="time"/>.
        /// </summary>
        public TValue[] GetValuesByTime(DateTime time) => timeline.Where(pair => pair.Time == time).Select(pair => pair.Value).ToArray();

        /// <summary>
        /// Get all <see cref="TValue"/> before <paramref name="time"/>.
        /// </summary>
        public Timeline<TValue> GetValuesBefore(DateTime time)
        {
            return new ((from pair in this
                                         where pair.Time < time
                                         select pair).ToArray());
        }

        /// <summary>
        /// Get all <see cref="TValue"/> before <paramref name="time"/>.
        /// </summary>
        public Timeline<TValue> GetValuesAfter(DateTime time)
        {
            return new ((from pair in this
                                         where pair.Time > time
                                         select pair).ToArray());
        }

        /// <summary>
        /// Gets all values that happened at specified millisecond.
        /// </summary>
        /// <param name="millisecond">Value to look for.</param>
        /// <returns>Array of values.</returns>
        public Timeline<TValue> GetValuesByMillisecond(int millisecond)
        {
            return new ((from pair in timeline
                                         where pair.Time.Millisecond == millisecond
                                         select pair).ToArray());
        }

        /// <summary>
        /// Gets all values that happened at specified second.
        /// </summary>
        /// <param name="second">Value to look for.</param>
        /// <returns>Array of values.</returns>
        public Timeline<TValue> GetValuesBySecond(int second)
        {
            return new ((from pair in timeline
                                         where pair.Time.Second == second
                                         select pair).ToArray());
        }

        /// <summary>
        /// Gets all values that happened at specified minute.
        /// </summary>
        /// <param name="minute">Value to look for.</param>
        /// <returns>Array of values.</returns>
        public Timeline<TValue> GetValuesByMinute(int minute)
        {
            return new ((from pair in timeline
                                         where pair.Time.Minute == minute
                                         select pair).ToArray());
        }

        /// <summary>
        /// Gets all values that happened at specified hour.
        /// </summary>
        /// <param name="hour">Value to look for.</param>
        /// <returns>Array of values.</returns>
        public Timeline<TValue> GetValuesByHour(int hour)
        {
            return new ((from pair in timeline
                                         where pair.Time.Hour == hour
                                         select pair).ToArray());
        }

        /// <summary>
        /// Gets all values that happened at specified day.
        /// </summary>
        /// <param name="day">Value to look for.</param>
        /// <returns>Array of values.</returns>
        public Timeline<TValue> GetValuesByDay(int day)
        {
            return new ((from pair in timeline
                                         where pair.Time.Day == day
                                         select pair).ToArray());
        }

        /// <summary>
        /// Gets all values that happened at specified time of the day.
        /// </summary>
        /// <param name="timeOfDay">Value to look for.</param>
        /// <returns>Array of values.</returns>
        public Timeline<TValue> GetValuesByTimeOfDay(TimeSpan timeOfDay)
        {
            return new ((from pair in timeline
                                         where pair.Time.TimeOfDay == timeOfDay
                                         select pair).ToArray());
        }

        /// <summary>
        /// Gets all values that happened at specified day of the week.
        /// </summary>
        /// <param name="dayOfWeek">Value to look for.</param>
        /// <returns>Array of values.</returns>
        public Timeline<TValue> GetValuesByDayOfWeek(DayOfWeek dayOfWeek)
        {
            return new ((from pair in timeline
                                         where pair.Time.DayOfWeek == dayOfWeek
                                         select pair).ToArray());
        }

        /// <summary>
        /// Gets all values that happened at specified day of the year.
        /// </summary>
        /// <param name="dayOfYear">Value to look for.</param>
        /// <returns>Array of values.</returns>
        public Timeline<TValue> GetValuesByDayOfYear(int dayOfYear)
        {
            return new ((from pair in timeline
                                         where pair.Time.DayOfYear == dayOfYear
                                         select pair).ToArray());
        }

        /// <summary>
        /// Gets all values that happened at specified month.
        /// </summary>
        /// <param name="month">Value to look for.</param>
        /// <returns>Array of values.</returns>
        public Timeline<TValue> GetValuesByMonth(int month)
        {
            return new ((from pair in timeline
                                         where pair.Time.Month == month
                                         select pair).ToArray());
        }

        /// <summary>
        /// Gets all values that happened at specified year.
        /// </summary>
        /// <param name="year">Value to look for.</param>
        /// <returns>Array of values.</returns>
        public Timeline<TValue> GetValuesByYear(int year)
        {
            return new ((from pair in timeline
                                         where pair.Time.Year == year
                                         select pair).ToArray());
        }

        /// <summary>
        /// Add a <see cref="DateTime"/> and a <see cref="TValue"/> to the timeline.
        /// </summary>
        public void Add(DateTime time, TValue value)
        {
            timeline.Add((time, value));
            timeline = timeline.OrderBy(pair => pair.Time).ToList();
        }

        /// <summary>
        /// Add a set of <see cref="DateTime"/> and <see cref="TValue"/> to the timeline.
        /// </summary>
        public void Add(params (DateTime, TValue)[] timeline)
        {
            this.timeline.AddRange(timeline);
            this.timeline = this.timeline.OrderBy(pair => pair.Time).ToList();
        }

        /// <summary>
        /// Add an existing timeline to this timeline.
        /// </summary>
        public void Add(Timeline<TValue> timeline)
        {
            Add(timeline.ToArray());
        }

        /// <summary>
        /// Add a <paramref name="value"/> associated with <see cref="DateTime.Now"/> to the timeline.
        /// </summary>
        public void AddNow(params TValue[] value)
        {
            var now = DateTime.Now;
            foreach (var v in value)
            {
                Add(now, v);
            }
        }

        /// <summary>
        /// Returns true if the timeline contains this value pair.
        /// </summary>
        public bool Contains(DateTime time, TValue value)
        {
            return timeline.Contains((time, value));
        }

        /// <summary>
        /// Returns true if the timeline contains this set of value pairs.
        /// </summary>
        public bool Contains(params (DateTime, TValue)[] timeline)
        {
            var result = true;
            foreach (var (time, value) in timeline)
            {
                result &= Contains(time, value);
            }

            return result;
        }

        /// <summary>
        /// Returns true if this timeline contains an existing timeline.
        /// </summary>
        public bool Contains(Timeline<TValue> timeline)
        {
            return Contains(timeline.ToArray());
        }

        /// <summary>
        /// Returns true if the timeline constains <paramref name="time"/>.
        /// </summary>
        public bool ContainsTime(params DateTime[] time)
        {
            var result = true;
            foreach (var value in time)
            {
                result &= GetAllTimes().Contains(value);
            }

            return result;
        }

        /// <summary>
        /// Returns true if the timeline constains <paramref name="value"/>.
        /// </summary>
        public bool ContainsValue(params TValue[] value)
        {
            var result = true;
            foreach (var v in value)
            {
                result &= GetAllValues().Contains(v);
            }

            return result;
        }

        /// <summary>
        /// Remove a value pair from the timeline.
        /// </summary>
        /// <returns>Returns true if the operation completed successfully.</returns>
        public bool Remove(DateTime time, TValue value)
        {
            return timeline.Remove((time, value));
        }

        /// <summary>
        /// Remove a set of value pairs from the timeline.
        /// </summary>
        /// <returns>Returns true if the operation completed successfully.</returns>
        public bool Remove(params (DateTime, TValue)[] timeline)
        {
            var result = false;
            foreach (var (time, value) in timeline)
            {
                result |= this.timeline.Remove((time, value));
            }

            return result;
        }

        /// <summary>
        /// Remove an existing timeline from this timeline.
        /// </summary>
        /// <returns>Returns true if the operation completed successfully.</returns>
        public bool Remove(Timeline<TValue> timeline)
        {
            return Remove(timeline.ToArray());
        }

        /// <summary>
        /// Remove a value pair from the timeline if the time is equal to <paramref name="time"/>.
        /// </summary>
        /// <returns>Returns true if the operation completed successfully.</returns>
        public bool RemoveTime(params DateTime[] time)
        {
            var result = false;
            foreach (var value in time)
            {
                result |= GetAllTimes().Contains(value);
            }

            if (result)
            {
                timeline = (from pair in timeline
                            where !time.Contains(pair.Time)
                            select pair).ToList();
            }

            return result;
        }

        /// <summary>
        /// Remove a value pair from the timeline if the value is equal to <paramref name="value"/>.
        /// </summary>
        /// <returns>Returns true if the operation completed successfully.</returns>
        public bool RemoveValue(params TValue[] value)
        {
            var result = false;
            foreach (var v in value)
            {
                result |= GetAllValues().Contains(v);
            }

            if (result)
            {
                timeline = (from pair in timeline
                            where !value.Contains(pair.Value)
                            select pair).ToList();
            }

            return result;
        }

        /// <summary>
        /// Convert the timeline to an array.
        /// </summary>
        public (DateTime Time, TValue Value)[] ToArray()
        {
            return timeline.ToArray();
        }

        /// <summary>
        /// Convert the timeline to a list.
        /// </summary>
        public IList<(DateTime Time, TValue Value)> ToList()
        {
            return timeline;
        }

        /// <summary>
        /// Convert the timeline to a dictionary.
        /// </summary>
        public IDictionary<DateTime, TValue> ToDictionary()
        {
            var dictionary = new Dictionary<DateTime, TValue>();
            foreach (var (date, time) in timeline)
            {
                dictionary.Add(date, time);
            }

            return dictionary;
        }

        public override bool Equals(object? obj) => obj is Timeline<TValue> timeline && this == timeline;

        public override int GetHashCode() => timeline.GetHashCode();

        void ICollection<(DateTime Time, TValue Value)>.Add((DateTime Time, TValue Value) item) => Add(item.Time, item.Value);

        bool ICollection<(DateTime Time, TValue Value)>.Contains((DateTime Time, TValue Value) item) => Contains(item.Time, item.Value);

        bool ICollection<(DateTime Time, TValue Value)>.Remove((DateTime Time, TValue Value) item) => Remove(item.Time, item.Value);

        IEnumerator IEnumerable.GetEnumerator() => timeline.GetEnumerator();

        IEnumerator<(DateTime Time, TValue Value)> IEnumerable<(DateTime Time, TValue Value)>.GetEnumerator() => timeline.GetEnumerator();
    }
}
