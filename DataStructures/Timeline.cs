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
    public class Timeline<TValue>:
        IEnumerable<(DateTime Time, TValue Value)>,
        ICollection<(DateTime Time, TValue Value)>,
        IEquatable<Timeline<TValue>>
    {
        private List<(DateTime Time, TValue Value)> timeline;
        bool ICollection<(DateTime Time, TValue Value)>.IsReadOnly => false;

        /// <summary>
        /// The count of pairs.
        /// </summary>
        public int Count => timeline.Count;

        /// <summary>
        /// The count of times.
        /// </summary>
        public int TimesCount => GetAllTimes().Length;

        /// <summary>
        /// The count of values.
        /// </summary>
        public int ValuesCount => GetAllValues().Length;

        /// <summary>
        /// Get all values associated with <paramref name="time"/>.
        /// </summary>
        /// <returns>Values associated with <paramref name="time"/>.</returns>
        public TValue[] this[DateTime time]
        {
            get => GetValuesByTime(time);
            set
            {
                var c = -1;
                int i;
                for (i = 0; i < Count; i++)
                {
                    if (timeline[i].Time == time)
                    {
                        if (c == value.Length)
                            timeline.RemoveAt(i);
                        else
                            timeline[i] = (timeline[i].Time, value[++c]);
                    }
                }
                while (i < c)
                    Add(time, value[c]);
            }
        }

        /// <summary>
        /// Initialize the instance without items.
        /// </summary>
        public Timeline()
        {
            timeline = new List<(DateTime, TValue)>();
        }

        /// <summary>
        /// Initialize the instance with a <paramref name="time"/> and a <paramref name="value"/>.
        /// </summary>
        public Timeline(DateTime time, TValue value)
        {
            timeline = new List<(DateTime, TValue)> { (time, value) };
        }

        /// <summary>
        /// Initialize the instance with a <paramref name="value"/> associated with <see cref="DateTime.Now"/>.
        /// </summary>
        public Timeline(params TValue[] value)
        {
            timeline = new List<(DateTime, TValue)>();
            var now = DateTime.Now;
            foreach (var v in value)
                timeline.Add((now, v));
        }

        /// <summary>
        /// Initialize the instance with a set of time and value.
        /// </summary>>
        public Timeline(params (DateTime, TValue)[] timeline)
        {
            this.timeline = timeline.ToList();
            this.timeline = this.timeline.OrderBy(pair => pair.Time).ToList();
        }

        /// <summary>
        /// Returns true if <paramref name="other"/> is equal to this timeline.
        /// </summary>
        /// <param name="other">Another <see cref="Timeline{TValue}"/> object.</param>
        public bool Equals(Timeline<TValue> other)
        {
            return this == other;
        }

        /// <summary>
        /// Clean the timeline.
        /// </summary>
        public void Clear()
        {
            timeline.Clear();
        }

        /// <summary>
        /// Copy a value to an array.
        /// </summary>
        /// <param name="arrayIndex">The start index.</param>
        public void CopyTo((DateTime, TValue)[] array, int arrayIndex)
        {
            timeline.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Get all <see cref="DateTime"/> of the timeline.
        /// </summary>
        public DateTime[] GetAllTimes()
        {
            var result = new List<DateTime>();
            foreach (var (time, _) in timeline)
            {
                if (!result.Contains(time))
                    result.Add(time);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Get <see cref="DateTime"/> values of the timeline ​​that have this <paramref name="value"/>.
        /// </summary>
        public DateTime[] GetTimesByValue(TValue value)
        {
            return (from pair in timeline
                    where pair.Value.Equals(value)
                    select pair.Time).ToArray();
        }

        /// <summary>
        /// Get all <see cref="DateTime"/> before <paramref name="time"/>.
        /// </summary>
        public DateTime[] GetTimesBefore(DateTime time)
        {
            return (from t in GetAllTimes()
                    where t < time
                    select t).OrderBy(t => t).ToArray();
        }

        /// <summary>
        /// Get all <see cref="DateTime"/> after <paramref name="time"/>.
        /// </summary>
        public DateTime[] GetTimesAfter(DateTime time)
        {
            return (from t in GetAllTimes()
                    where t > time
                    select t).OrderBy(t => t).ToArray();
        }

        /// <summary>
        /// Get all <see cref="TValue"/> of the timeline.
        /// </summary>
        public TValue[] GetAllValues()
        {
            return (from pair in timeline
                    select pair.Value).ToArray();
        }

        /// <summary>
        /// Get all <see cref="TValue"/> associated with <paramref name="time"/>.
        /// </summary>
        public TValue[] GetValuesByTime(DateTime time)
        {
            return (from pair in timeline
                    where pair.Time == time
                    select pair.Value).ToArray();
        }

        /// <summary>
        /// Get all <see cref="TValue"/> before <paramref name="time"/>.
        /// </summary>
        public Timeline<TValue> GetValuesBefore(DateTime time)
        {
            return new Timeline<TValue>((from pair in this
                                         where pair.Time < time
                                         select pair).ToArray());
        }

        /// <summary>
        /// Get all <see cref="TValue"/> before <paramref name="time"/>.
        /// </summary>
        public Timeline<TValue> GetValuesAfter(DateTime time)
        {
            return new Timeline<TValue>((from pair in this
                                         where pair.Time > time
                                         select pair).ToArray());
        }

        public Timeline<TValue> GetValuesByMillisecond(int millisecond)
        {
            return new Timeline<TValue>((from pair in timeline
                                         where pair.Time.Millisecond == millisecond
                                         select pair).ToArray());
        }

        public Timeline<TValue> GetValuesBySecond(int second)
        {
            return new Timeline<TValue>((from pair in timeline
                                         where pair.Time.Second == second
                                         select pair).ToArray());
        }

        public Timeline<TValue> GetValuesByMinute(int minute)
        {
            return new Timeline<TValue>((from pair in timeline
                                         where pair.Time.Minute == minute
                                         select pair).ToArray());
        }

        public Timeline<TValue> GetValuesByHour(int hour)
        {
            return new Timeline<TValue>((from pair in timeline
                                         where pair.Time.Hour == hour
                                         select pair).ToArray());
        }

        public Timeline<TValue> GetValuesByDay(int day)
        {
            return new Timeline<TValue>((from pair in timeline
                                         where pair.Time.Day == day
                                         select pair).ToArray());
        }

        public Timeline<TValue> GetValuesByTimeOfDay(TimeSpan timeOfDay)
        {
            return new Timeline<TValue>((from pair in timeline
                                         where pair.Time.TimeOfDay == timeOfDay
                                         select pair).ToArray());
        }

        public Timeline<TValue> GetValuesByDayOfWeek(DayOfWeek dayOfWeek)
        {
            return new Timeline<TValue>((from pair in timeline
                                         where pair.Time.DayOfWeek == dayOfWeek
                                         select pair).ToArray());
        }

        public Timeline<TValue> GetValuesByDayOfYear(int dayOfYear)
        {
            return new Timeline<TValue>((from pair in timeline
                                         where pair.Time.DayOfYear == dayOfYear
                                         select pair).ToArray());
        }

        public Timeline<TValue> GetValuesByMonth(int month)
        {
            return new Timeline<TValue>((from pair in timeline
                                         where pair.Time.Month == month
                                         select pair).ToArray());
        }

        public Timeline<TValue> GetValuesByYear(int year)
        {
            return new Timeline<TValue>((from pair in timeline
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
                Add(now, v);
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
                result &= Contains(time, value);
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
            foreach (var TValue in time)
                result &= GetAllTimes().Contains(TValue);
            return result;
        }

        /// <summary>
        /// Returns true if the timeline constains <paramref name="value"/>.
        /// </summary>
        public bool ContainsValue(params TValue[] value)
        {
            var result = true;
            foreach (var v in value)
                result &= GetAllValues().Contains(v);
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
                result |= this.timeline.Remove((time, value));
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
            foreach (var TValue in time)
                result |= GetAllTimes().Contains(TValue);
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
                result |= GetAllValues().Contains(v);
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
                dictionary.Add(date, time);
            return dictionary;
        }

        /// <summary>
        /// Returns true if <paramref name="obj"/> is equal to this timeline.
        /// </summary>
        /// <param name="obj">A <see cref="Timeline{TValue}"/> instance.</param>
        public override bool Equals(object obj)
        {
            return this == obj as Timeline<TValue>;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        void ICollection<(DateTime Time, TValue Value)>.Add((DateTime Time, TValue Value) item)
        {
            Add(item.Time, item.Value);
        }

        bool ICollection<(DateTime Time, TValue Value)>.Contains((DateTime Time, TValue Value) item)
        {
            return Contains(item.Time, item.Value);
        }

        bool ICollection<(DateTime Time, TValue Value)>.Remove((DateTime Time, TValue Value) item)
        {
            return Remove(item.Time, item.Value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new TimelineEnumerator(this);
        }

        IEnumerator<(DateTime Time, TValue Value)> IEnumerable<(DateTime Time, TValue Value)>.GetEnumerator()
        {
            return new TimelineEnumerator(this);
        }

        /// <summary>
        /// Returns true if <paramref name="left"/> is equal to <paramref name="right"/>.
        /// </summary>
        public static bool operator ==(Timeline<TValue> left, Timeline<TValue> right)
        {
            var leftArray = left.ToArray();
            var rightArray = right.ToArray();
            if (leftArray.Length == rightArray.Length)
            {
                for (int i = 0; i < leftArray.Length; i++)
                {
                    if (leftArray[i].Time != rightArray[i].Time && !leftArray[i].Value.Equals(rightArray[i].Value))
                        return false;
                }
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Returns true if <paramref name="left"/> is unequal to <paramref name="right"/>.
        /// </summary>
        public static bool operator !=(Timeline<TValue> left, Timeline<TValue> right)
        {
            return !(left == right);
        }

        private sealed class TimelineEnumerator: IEnumerator<(DateTime Time, TValue Value)>, IDisposable
        {
            public int index;
            public Timeline<TValue> timeline;
            object IEnumerator.Current { get => timeline.timeline[index]; }
            (DateTime Time, TValue Value) IEnumerator<(DateTime Time, TValue Value)>.Current { get => timeline.timeline[index]; }
            public void Dispose() { /*Do nothing because is useless but necessary for the IEnumerator<bool> interface*/ }

            public TimelineEnumerator(Timeline<TValue> timeline)
            {
                index = -1;
                this.timeline = timeline;
            }

            bool IEnumerator.MoveNext()
            {
                index++;
                return index < timeline.Count;
            }

            void IEnumerator.Reset()
            {
                index = -1;
            }
        }
    }
}
