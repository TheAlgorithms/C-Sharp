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

namespace DataStructures;

/// <summary>
///     A collection of <see cref="DateTime" /> and <see cref="TValue" />
///     sorted by <see cref="DateTime" /> field.
/// </summary>
/// <typeparam name="TValue">Value associated with a <see cref="DateTime" />.</typeparam>
public class Timeline<TValue> : ICollection<(DateTime Time, TValue Value)>, IEquatable<Timeline<TValue>>
{
    /// <summary>
    ///     Inner collection storing the timeline events as key-tuples.
    /// </summary>
    private readonly List<(DateTime Time, TValue Value)> timeline = new();

    /// <summary>
    ///     Initializes a new instance of the <see cref="Timeline{TValue}"/> class.
    /// </summary>
    public Timeline()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Timeline{TValue}"/> class populated with an initial event.
    /// </summary>
    /// <param name="time">The time at which the given event occurred.</param>
    /// <param name="value">The event's content.</param>
    public Timeline(DateTime time, TValue value)
        => timeline = new List<(DateTime, TValue)>
        {
            (time, value),
        };

    /// <summary>
    ///     Initializes a new instance of the <see cref="Timeline{TValue}"/> class containing the provided events
    ///     ordered chronologically.
    /// </summary>
    /// <param name="timeline">The timeline to represent.</param>
    public Timeline(params (DateTime, TValue)[] timeline)
        => this.timeline = timeline
            .OrderBy(pair => pair.Item1)
            .ToList();

    /// <summary>
    /// Gets he number of unique times within this timeline.
    /// </summary>
    public int TimesCount
        => GetAllTimes().Length;

    /// <summary>
    ///     Gets all events that has occurred in this timeline.
    /// </summary>
    public int ValuesCount
        => GetAllValues().Length;

    /// <summary>
    ///     Get all values associated with <paramref name="time" />.
    /// </summary>
    /// <param name="time">Time to get values for.</param>
    /// <returns>Values associated with <paramref name="time" />.</returns>
    public TValue[] this[DateTime time]
    {
        get => GetValuesByTime(time);
        set
        {
            var overridenEvents = timeline.Where(@event => @event.Time == time).ToList();
            foreach (var @event in overridenEvents)
            {
                timeline.Remove(@event);
            }

            foreach (var v in value)
            {
                Add(time, v);
            }
        }
    }

    /// <inheritdoc />
    bool ICollection<(DateTime Time, TValue Value)>.IsReadOnly
        => false;

    /// <summary>
    ///     Gets the count of pairs.
    /// </summary>
    public int Count
        => timeline.Count;

    /// <summary>
    ///     Clear the timeline, removing all events.
    /// </summary>
    public void Clear()
        => timeline.Clear();

    /// <summary>
    ///     Copy a value to an array.
    /// </summary>
    /// <param name="array">Destination array.</param>
    /// <param name="arrayIndex">The start index.</param>
    public void CopyTo((DateTime, TValue)[] array, int arrayIndex)
        => timeline.CopyTo(array, arrayIndex);

    /// <summary>
    ///     Add an event at a given time.
    /// </summary>
    /// <param name="item">The tuple containing the event date and value.</param>
    void ICollection<(DateTime Time, TValue Value)>.Add((DateTime Time, TValue Value) item)
        => Add(item.Time, item.Value);

    /// <summary>
    ///     Check whether or not a event exists at a specific date in the timeline.
    /// </summary>
    /// <param name="item">The tuple containing the event date and value.</param>
    /// <returns>True if this event exists at the given date, false otherwise.</returns>
    bool ICollection<(DateTime Time, TValue Value)>.Contains((DateTime Time, TValue Value) item)
        => Contains(item.Time, item.Value);

    /// <summary>
    ///     Remove an event at a specific date.
    /// </summary>
    /// <param name="item">The tuple containing the event date and value.</param>
    /// <returns>True if the event was removed, false otherwise.</returns>
    bool ICollection<(DateTime Time, TValue Value)>.Remove((DateTime Time, TValue Value) item)
        => Remove(item.Time, item.Value);

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
        => timeline.GetEnumerator();

    /// <inheritdoc />
    IEnumerator<(DateTime Time, TValue Value)> IEnumerable<(DateTime Time, TValue Value)>.GetEnumerator()
        => timeline.GetEnumerator();

    /// <inheritdoc />
    public bool Equals(Timeline<TValue>? other)
        => other is not null && this == other;

    /// <summary>
    ///     Checks whether or not two <see cref="Timeline{TValue}"/> are equals.
    /// </summary>
    /// <param name="left">The first timeline.</param>
    /// <param name="right">The other timeline to be checked against the <paramref name="left"/> one.</param>
    /// <returns>True if both timelines are similar, false otherwise.</returns>
    public static bool operator ==(Timeline<TValue> left, Timeline<TValue> right)
    {
        var leftArray = left.ToArray();
        var rightArray = right.ToArray();

        if (left.Count != rightArray.Length)
        {
            return false;
        }

        for (var i = 0; i < leftArray.Length; i++)
        {
            if (leftArray[i].Time != rightArray[i].Time
                && !leftArray[i].Value!.Equals(rightArray[i].Value))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///     Checks whether or not two <see cref="Timeline{TValue}"/> are not equals.
    /// </summary>
    /// <param name="left">The first timeline.</param>
    /// <param name="right">The other timeline to be checked against the <paramref name="left"/> one.</param>
    /// <returns>False if both timelines are similar, true otherwise.</returns>
    public static bool operator !=(Timeline<TValue> left, Timeline<TValue> right)
        => !(left == right);

    /// <summary>
    ///     Get all <see cref="DateTime" /> of the timeline.
    /// </summary>
    public DateTime[] GetAllTimes()
        => timeline.Select(t => t.Time)
            .Distinct()
            .ToArray();

    /// <summary>
    ///     Get <see cref="DateTime" /> values of the timeline that have this <paramref name="value" />.
    /// </summary>
    public DateTime[] GetTimesByValue(TValue value)
        => timeline.Where(pair => pair.Value!.Equals(value))
            .Select(pair => pair.Time)
            .ToArray();

    /// <summary>
    ///     Get all <see cref="DateTime" /> before <paramref name="time" />.
    /// </summary>
    public DateTime[] GetTimesBefore(DateTime time)
        => GetAllTimes()
            .Where(t => t < time)
            .OrderBy(t => t)
            .ToArray();

    /// <summary>
    ///     Get all <see cref="DateTime" /> after <paramref name="time" />.
    /// </summary>
    public DateTime[] GetTimesAfter(DateTime time)
        => GetAllTimes()
            .Where(t => t > time)
            .OrderBy(t => t)
            .ToArray();

    /// <summary>
    ///     Get all <see cref="TValue" /> of the timeline.
    /// </summary>
    public TValue[] GetAllValues()
        => timeline.Select(pair => pair.Value)
            .ToArray();

    /// <summary>
    ///     Get all <see cref="TValue" /> associated with <paramref name="time" />.
    /// </summary>
    public TValue[] GetValuesByTime(DateTime time)
        => timeline.Where(pair => pair.Time == time)
            .Select(pair => pair.Value)
            .ToArray();

    /// <summary>
    ///     Get all <see cref="TValue" /> before <paramref name="time" />.
    /// </summary>
    public Timeline<TValue> GetValuesBefore(DateTime time)
        => new(this.Where(pair => pair.Time < time).ToArray());

    /// <summary>
    ///     Get all <see cref="TValue" /> before <paramref name="time" />.
    /// </summary>
    public Timeline<TValue> GetValuesAfter(DateTime time)
        => new(this.Where(pair => pair.Time > time).ToArray());

    /// <summary>
    ///     Gets all values that happened at specified millisecond.
    /// </summary>
    /// <param name="millisecond">Value to look for.</param>
    /// <returns>Array of values.</returns>
    public Timeline<TValue> GetValuesByMillisecond(int millisecond)
        => new(timeline.Where(pair => pair.Time.Millisecond == millisecond).ToArray());

    /// <summary>
    ///     Gets all values that happened at specified second.
    /// </summary>
    /// <param name="second">Value to look for.</param>
    /// <returns>Array of values.</returns>
    public Timeline<TValue> GetValuesBySecond(int second)
        => new(timeline.Where(pair => pair.Time.Second == second).ToArray());

    /// <summary>
    ///     Gets all values that happened at specified minute.
    /// </summary>
    /// <param name="minute">Value to look for.</param>
    /// <returns>Array of values.</returns>
    public Timeline<TValue> GetValuesByMinute(int minute)
        => new(timeline.Where(pair => pair.Time.Minute == minute).ToArray());

    /// <summary>
    ///     Gets all values that happened at specified hour.
    /// </summary>
    /// <param name="hour">Value to look for.</param>
    /// <returns>Array of values.</returns>
    public Timeline<TValue> GetValuesByHour(int hour)
        => new(timeline.Where(pair => pair.Time.Hour == hour).ToArray());

    /// <summary>
    ///     Gets all values that happened at specified day.
    /// </summary>
    /// <param name="day">Value to look for.</param>
    /// <returns>Array of values.</returns>
    public Timeline<TValue> GetValuesByDay(int day)
        => new(timeline.Where(pair => pair.Time.Day == day).ToArray());

    /// <summary>
    ///     Gets all values that happened at specified time of the day.
    /// </summary>
    /// <param name="timeOfDay">Value to look for.</param>
    /// <returns>Array of values.</returns>
    public Timeline<TValue> GetValuesByTimeOfDay(TimeSpan timeOfDay)
        => new(timeline.Where(pair => pair.Time.TimeOfDay == timeOfDay).ToArray());

    /// <summary>
    ///     Gets all values that happened at specified day of the week.
    /// </summary>
    /// <param name="dayOfWeek">Value to look for.</param>
    /// <returns>Array of values.</returns>
    public Timeline<TValue> GetValuesByDayOfWeek(DayOfWeek dayOfWeek)
        => new(timeline.Where(pair => pair.Time.DayOfWeek == dayOfWeek).ToArray());

    /// <summary>
    ///     Gets all values that happened at specified day of the year.
    /// </summary>
    /// <param name="dayOfYear">Value to look for.</param>
    /// <returns>Array of values.</returns>
    public Timeline<TValue> GetValuesByDayOfYear(int dayOfYear)
        => new(timeline.Where(pair => pair.Time.DayOfYear == dayOfYear).ToArray());

    /// <summary>
    ///     Gets all values that happened at specified month.
    /// </summary>
    /// <param name="month">Value to look for.</param>
    /// <returns>Array of values.</returns>
    public Timeline<TValue> GetValuesByMonth(int month)
        => new(timeline.Where(pair => pair.Time.Month == month).ToArray());

    /// <summary>
    ///     Gets all values that happened at specified year.
    /// </summary>
    /// <param name="year">Value to look for.</param>
    /// <returns>Array of values.</returns>
    public Timeline<TValue> GetValuesByYear(int year)
        => new(timeline.Where(pair => pair.Time.Year == year).ToArray());

    /// <summary>
    ///     Add an event at a given <paramref name="time"/>.
    /// </summary>
    /// <param name="time">The date at which the event occurred.</param>
    /// <param name="value">The event value.</param>
    public void Add(DateTime time, TValue value)
    {
        timeline.Add((time, value));
    }

    /// <summary>
    ///     Add a set of <see cref="DateTime" /> and <see cref="TValue" /> to the timeline.
    /// </summary>
    public void Add(params (DateTime, TValue)[] timeline)
    {
        this.timeline.AddRange(timeline);
    }

    /// <summary>
    ///     Append an existing timeline to this one.
    /// </summary>
    public void Add(Timeline<TValue> timeline)
        => Add(timeline.ToArray());

    /// <summary>
    ///     Add a <paramref name="value" /> associated with <see cref="DateTime.Now" /> to the timeline.
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
    ///     Check whether or not a event exists at a specific date in the timeline.
    /// </summary>
    /// <param name="time">The date at which the event occurred.</param>
    /// <param name="value">The event value.</param>
    /// <returns>True if this event exists at the given date, false otherwise.</returns>
    public bool Contains(DateTime time, TValue value)
        => timeline.Contains((time, value));

    /// <summary>
    ///     Check if timeline contains this set of value pairs.
    /// </summary>
    /// <param name="timeline">The events to checks.</param>
    /// <returns>True if any of the events has occurred in the timeline.</returns>
    public bool Contains(params (DateTime, TValue)[] timeline)
        => timeline.Any(@event => Contains(@event.Item1, @event.Item2));

    /// <summary>
    ///     Check if timeline contains any of the event of the provided <paramref name="timeline"/>.
    /// </summary>
    /// <param name="timeline">The events to checks.</param>
    /// <returns>True if any of the events has occurred in the timeline.</returns>
    public bool Contains(Timeline<TValue> timeline)
        => Contains(timeline.ToArray());

    /// <summary>
    ///     Check if timeline contains any of the time of the provided <paramref name="times"/>.
    /// </summary>
    /// <param name="times">The times to checks.</param>
    /// <returns>True if any of the times is stored in the timeline.</returns>
    public bool ContainsTime(params DateTime[] times)
    {
        var storedTimes = GetAllTimes();
        return times.Any(value => storedTimes.Contains(value));
    }

    /// <summary>
    ///     Check if timeline contains any of the event of the provided <paramref name="values"/>.
    /// </summary>
    /// <param name="values">The events to checks.</param>
    /// <returns>True if any of the events has occurred in the timeline.</returns>
    public bool ContainsValue(params TValue[] values)
    {
        var storedValues = GetAllValues();
        return values.Any(value => storedValues.Contains(value));
    }

    /// <summary>
    ///     Remove an event at a specific date.
    /// </summary>
    /// <param name="time">The date at which the event occurred.</param>
    /// <param name="value">The event value.</param>
    /// <returns>True if the event was removed, false otherwise.</returns>
    public bool Remove(DateTime time, TValue value)
        => timeline.Remove((time, value));

    /// <summary>
    ///     Remove a set of value pairs from the timeline.
    /// </summary>
    /// <param name="timeline">An collection of all events to remove.</param>
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
    ///     Remove an existing timeline from this timeline.
    /// </summary>
    /// <param name="timeline">An collection of all events to remove.</param>
    /// <returns>Returns true if the operation completed successfully.</returns>
    public bool Remove(Timeline<TValue> timeline)
        => Remove(timeline.ToArray());

    /// <summary>
    ///     Remove a value pair from the timeline if the time is equal to <paramref name="times" />.
    /// </summary>
    /// <returns>Returns true if the operation completed successfully.</returns>
    public bool RemoveTimes(params DateTime[] times)
    {
        var isTimeContainedInTheTimeline = times.Any(time => GetAllTimes().Contains(time));

        if (!isTimeContainedInTheTimeline)
        {
            return false;
        }

        var eventsToRemove = times.SelectMany(time =>
            timeline.Where(@event => @event.Time == time))
            .ToList();

        foreach (var @event in eventsToRemove)
        {
            timeline.Remove(@event);
        }

        return true;
    }

    /// <summary>
    ///     Remove a value pair from the timeline if the value is equal to <paramref name="values" />.
    /// </summary>
    /// <returns>Returns true if the operation completed successfully.</returns>
    public bool RemoveValues(params TValue[] values)
    {
        var isValueContainedInTheTimeline = values.Any(v => GetAllValues().Contains(v));

        if (!isValueContainedInTheTimeline)
        {
            return false;
        }

        var eventsToRemove = values.SelectMany(value =>
            timeline.Where(@event => EqualityComparer<TValue>.Default.Equals(@event.Value, value)))
            .ToList();

        foreach (var @event in eventsToRemove)
        {
            timeline.Remove(@event);
        }

        return true;
    }

    /// <summary>
    ///     Convert the timeline to an array.
    /// </summary>
    /// <returns>
    /// The timeline as an array of tuples of (<see cref="DateTime"/>, <typeparamref name="TValue"/>).
    /// </returns>
    public (DateTime Time, TValue Value)[] ToArray()
        => timeline.ToArray();

    /// <summary>
    ///     Convert the timeline to a list.
    /// </summary>
    /// <returns>
    /// The timeline as a list of tuples of (<see cref="DateTime"/>, <typeparamref name="TValue"/>).
    /// </returns>
    public IList<(DateTime Time, TValue Value)> ToList()
        => timeline;

    /// <summary>
    ///     Convert the timeline to a dictionary.
    /// </summary>
    /// <returns>
    /// The timeline as an dictionary of <typeparamref name="TValue"/> by <see cref="DateTime"/>.
    /// </returns>
    public IDictionary<DateTime, TValue> ToDictionary()
        => timeline.ToDictionary(@event => @event.Time, @event => @event.Value);

    /// <inheritdoc />
    public override bool Equals(object? obj)
        => obj is Timeline<TValue> otherTimeline
           && this == otherTimeline;

    /// <inheritdoc />
    public override int GetHashCode()
        => timeline.GetHashCode();
}
