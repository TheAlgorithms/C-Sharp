using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using NUnit.Framework;

namespace DataStructures.Tests;

public static class TimelineTests
{
    [Test]
    public static void CountTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Count
            .Should()
            .Be(5);
    }

    [Test]
    public static void TimesCountTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.TimesCount
            .Should()
            .Be(timeline.GetAllTimes().Length);
    }

    [Test]
    public static void ValuesCountTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.ValuesCount
            .Should()
            .Be(timeline.GetAllValues().Length);
    }

    [Test]
    public static void IndexerGetTest()
    {
        const string eventName = "TestTime2";
        var eventDate = new DateTime(2000, 1, 1);

        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { eventDate, eventName },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline[eventDate][0]
            .Should()
            .Be(eventName);
    }

    [Test]
    public static void IndexerSetTest()
    {
        var eventDate = new DateTime(2000, 1, 1);

        const string formerEventName = "TestTime2";
        const string eventName = "TestTime2Modified";

        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { eventDate, formerEventName },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline[new DateTime(2000, 1, 1)] = new[] { eventName };

        timeline[new DateTime(2000, 1, 1)][0]
            .Should()
            .Be(eventName);
    }

    [Test]
    public static void EqualsTest()
    {
        var timeline1 = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var timeline2 = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        (timeline1 == timeline2)
            .Should()
            .BeTrue();
    }

    [Test]
    public static void ClearTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Clear();

        timeline.Count
            .Should()
            .Be(0);
    }

    [Test]
    public static void CopyToTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var array = new (DateTime Time, string Value)[timeline.Count];
        timeline.CopyTo(array, 0);

        timeline.Count
            .Should()
            .Be(array.Length);

        var i = 0;
        using (new AssertionScope())
        {
            foreach (var (time, value) in timeline)
            {
                array[i].Time
                    .Should()
                    .Be(time);

                array[i].Value
                    .Should()
                    .Be(value);

                ++i;
            }
        }
    }

    [Test]
    public static void GetAllTimesTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var times = timeline.GetAllTimes();

        var i = 0;
        using (new AssertionScope())
        {
            foreach (var (time, _) in timeline)
            {
                times[i++]
                    .Should()
                    .Be(time);
            }
        }
    }

    [Test]
    public static void GetTimesByValueTest()
    {
        var eventDate = new DateTime(2000, 1, 1);
        const string eventName = "TestTime2";

        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { eventDate, eventName },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.GetTimesByValue(eventName)[0]
            .Should()
            .Be(eventDate);
    }

    [Test]
    public static void GetTimesBeforeTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var times = timeline.GetTimesBefore(new DateTime(2003, 1, 1));

        using (new AssertionScope())
        {
            times.Length
                .Should()
                .Be(2);

            times[0]
                .Should()
                .Be(new DateTime(1995, 1, 1));

            times[1]
                .Should()
                .Be(new DateTime(2000, 1, 1));
        }
    }

    [Test]
    public static void GetTimesAfterTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var times = timeline.GetTimesAfter(new DateTime(2003, 1, 1));

        using (new AssertionScope())
        {
            times.Length
                .Should()
                .Be(3);

            times[0]
                .Should()
                .Be(new DateTime(2005, 1, 1));

            times[1]
                .Should()
                .Be(new DateTime(2010, 1, 1));

            times[2]
                .Should()
                .Be(new DateTime(2015, 1, 1));
        }
    }

    [Test]
    public static void GetAllValuesTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var values = timeline.GetAllValues();

        var i = 0;
        using (new AssertionScope())
        {
            foreach (var (_, value) in timeline)
            {
                values[i++]
                    .Should()
                    .Be(value);
            }
        }
    }

    [Test]
    public static void GetValuesByTimeTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.GetValuesByTime(new DateTime(2000, 1, 1))[0]
            .Should()
            .Be("TestTime2");
    }

    [Test]
    public static void GetValuesBeforeTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var array = timeline.GetValuesBefore(new DateTime(2003, 1, 1)).ToArray();

        using (new AssertionScope())
        {
            array.Length
                .Should()
                .Be(2);

            array[0].Time
                .Should()
                .Be(new DateTime(1995, 1, 1));

            array[1].Time
                .Should()
                .Be(new DateTime(2000, 1, 1));
        }
    }

    [Test]
    public static void GetValuesAfterTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var array = timeline.GetValuesAfter(new DateTime(2003, 1, 1)).ToArray();

        using (new AssertionScope())
        {
            array.Length
                .Should()
                .Be(3);

            array[0].Time
                .Should()
                .Be(new DateTime(2005, 1, 1));

            array[1].Time
                .Should()
                .Be(new DateTime(2010, 1, 1));

            array[2].Time
                .Should()
                .Be(new DateTime(2015, 1, 1));
        }
    }

    [Test]
    public static void GetValuesByMillisecondTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 10, 0, 0, 250), "TestTime1" },
            { new DateTime(1990, 1, 1, 10, 0, 0, 250), "TestTime2" },
            { new DateTime(1995, 1, 1, 10, 0, 0, 250), "TestTime3" },
            { new DateTime(2005, 1, 1, 10, 0, 0, 750), "TestTime4" },
            { new DateTime(2015, 1, 1, 10, 0, 0, 750), "TestTime5" },
        };

        var query = timeline.GetValuesByMillisecond(750);

        query.Count
            .Should()
            .Be(2);
    }

    [Test]
    public static void GetValuesBySecondTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 10, 0, 5), "TestTime1" },
            { new DateTime(1990, 1, 1, 10, 0, 5), "TestTime2" },
            { new DateTime(1995, 1, 1, 10, 0, 5), "TestTime3" },
            { new DateTime(2005, 1, 1, 10, 0, 20), "TestTime4" },
            { new DateTime(2015, 1, 1, 10, 0, 20), "TestTime5" },
        };

        var query = timeline.GetValuesBySecond(20);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
    }

    [Test]
    public static void GetValuesByMinuteTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 10, 15, 0), "TestTime1" },
            { new DateTime(1990, 1, 1, 10, 15, 0), "TestTime2" },
            { new DateTime(1995, 1, 1, 10, 15, 0), "TestTime3" },
            { new DateTime(2005, 1, 1, 10, 40, 0), "TestTime4" },
            { new DateTime(2015, 1, 1, 10, 40, 0), "TestTime5" },
        };

        var query = timeline.GetValuesByMinute(40);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
    }

    [Test]
    public static void GetValuesByHourTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 7, 0, 0), "TestTime1" },
            { new DateTime(1990, 1, 1, 7, 0, 0), "TestTime2" },
            { new DateTime(1995, 1, 1, 7, 0, 0), "TestTime3" },
            { new DateTime(2005, 1, 1, 16, 0, 0), "TestTime4" },
            { new DateTime(2015, 1, 1, 16, 0, 0), "TestTime5" },
        };

        var query = timeline.GetValuesByHour(16);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
    }

    [Test]
    public static void GetValuesByDayTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 10), "TestTime1" },
            { new DateTime(1990, 1, 10), "TestTime2" },
            { new DateTime(1995, 1, 10), "TestTime3" },
            { new DateTime(2005, 1, 20), "TestTime4" },
            { new DateTime(2015, 1, 20), "TestTime5" },
        };

        var query = timeline.GetValuesByDay(20);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
    }

    [Test]
    public static void GetValuesByTimeOfDayTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1, 10, 30, 15, 500), "TestTime1" },
            { new DateTime(1990, 1, 1, 10, 30, 15, 500), "TestTime2" },
            { new DateTime(1995, 1, 1, 10, 30, 15, 500), "TestTime3" },
            { new DateTime(2005, 1, 1, 21, 15, 40, 600), "TestTime4" },
            { new DateTime(2015, 1, 1, 21, 15, 40, 600), "TestTime5" },
        };

        var query = timeline.GetValuesByTimeOfDay(new TimeSpan(0, 21, 15, 40, 600));

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
    }

    [Test]
    public static void GetValuesByDayOfWeekTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(2015, 1, 5), "TestTime1" }, //Monday
            { new DateTime(2015, 2, 2), "TestTime2" }, //Monday
            { new DateTime(2015, 1, 6), "TestTime3" }, //Tuesday
            { new DateTime(2015, 1, 7), "TestTime4" }, //Wednesday
            { new DateTime(2015, 1, 8), "TestTime5" }, //Thursday
        };

        var query = timeline.GetValuesByDayOfWeek(DayOfWeek.Monday);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
    }

    [Test]
    public static void GetValuesByDayOfYearTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 3), "TestTime1" }, //3rd day of year
            { new DateTime(1990, 1, 7), "TestTime2" }, //7th day of year
            { new DateTime(1995, 1, 22), "TestTime3" }, //22th day of year
            { new DateTime(2000, 2, 1), "TestTime4" }, //32th day of year
            { new DateTime(2005, 2, 1), "TestTime5" }, //32th day of year
        };

        var query = timeline.GetValuesByDayOfYear(32);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
    }

    [Test]
    public static void GetValuesByMonthTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 1), "TestTime1" },
            { new DateTime(1990, 2, 1), "TestTime2" },
            { new DateTime(1995, 3, 1), "TestTime3" },
            { new DateTime(2005, 4, 1), "TestTime4" },
            { new DateTime(2015, 4, 1), "TestTime5" },
        };

        var query = timeline.GetValuesByMonth(4);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
    }

    [Test]
    public static void GetValuesByYearTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1985, 1, 2), "TestTime1" },
            { new DateTime(1990, 2, 1), "TestTime2" },
            { new DateTime(1995, 1, 2), "TestTime3" },
            { new DateTime(2005, 2, 1), "TestTime4" },
            { new DateTime(2005, 1, 2), "TestTime5" },
        };

        var query = timeline.GetValuesByYear(2005);

        using (new AssertionScope())
        {
            query.Count
                .Should()
                .Be(2);

            timeline
                .Should()
                .Contain(query);
        }
    }

    [Test]
    public static void AddDateTimeAndTValueTest() //void Add(DateTime time, TValue value)
    {
        var eventDate = new DateTime(2015, 1, 1);
        const string eventName = "TestTime";

        var timeline = new Timeline<string>();

        timeline.Add(eventDate, eventName);

        timeline.Count
            .Should()
            .Be(1);

        timeline[eventDate][0]
            .Should()
            .Be(eventName);
    }

    [Test]
    public static void AddDateTimeAndTValueArrayTest() //void Add(params (DateTime, TValue)[] timeline)
    {
        var eventDate1 = new DateTime(2015, 1, 1);
        const string eventName1 = "TestTime1";

        var eventDate2 = new DateTime(1750, 1, 1);
        const string eventName2 = "TestTime2";

        var timeline = new Timeline<string>();

        timeline.Add(
            (eventDate1, eventName1),
            (eventDate2, eventName2));

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(2);

            timeline[eventDate1][0]
                .Should()
                .Be(eventName1);

            timeline[eventDate2][0]
                .Should()
                .Be(eventName2);
        }
    }

    [Test]
    public static void AddTimelineTest() //void Add(Timeline<TValue> timeline)
    {
        var eventDate = new DateTime(2015, 1, 1);
        const string eventName = "TestTime";

        var timeline = new Timeline<string>();

        timeline.Add(new Timeline<string>(eventDate, eventName));

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(1);

            timeline[eventDate][0]
                .Should()
                .Be(eventName);
        }
    }

    [Test]
    public static void AddNowTest()
    {
        var timeline = new Timeline<string>();

        timeline.AddNow("Now");

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(1);

            timeline.ContainsValue("Now")
                .Should()
                .BeTrue();
        }
    }

    [Test]
    public static void ContainsDateTimeAndTValueTest() //bool Contains(DateTime time, TValue value)
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Contains(new DateTime(2000, 1, 1), "TestTime2")
            .Should()
            .BeTrue();
    }

    [Test]
    public static void ContainsDateTimeAndTValueArrayTest() //bool Contains(params (DateTime, TValue)[] timeline)
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Contains(
                (new DateTime(1995, 1, 1), "TestTime1"),
                (new DateTime(2000, 1, 1), "TestTime2"))
            .Should()
            .BeTrue();
    }

    [Test]
    public static void ContainsTimelineTest() //bool Contains(Timeline<TValue> timeline)
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Contains(new Timeline<string>(new DateTime(2000, 1, 1), "TestTime2"))
            .Should()
            .BeTrue();
    }

    [Test]
    public static void ContainsTimeTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.ContainsTime(new DateTime(2000, 1, 1))
            .Should()
            .BeTrue();
    }

    [Test]
    public static void ContainsValueTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.ContainsValue("TestTime1")
            .Should()
            .BeTrue();
    }

    [Test]
    public static void RemoveDateTimeAndTValueTest() //bool Remove(DateTime time, TValue value)
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Remove(new DateTime(2000, 1, 1), "TestTime2");

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(4);

            timeline.Contains(new DateTime(2000, 1, 1), "TestTime2")
                .Should()
                .BeFalse();
        }
    }

    [Test]
    public static void RemoveDateTimeAndTValueArrayTest() //bool Remove(params (DateTime, TValue)[] timeline)
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Remove(
            (new DateTime(1995, 1, 1), "TestTime1"),
            (new DateTime(2000, 1, 1), "TestTime2"));

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(3);

            timeline.Contains(
                    (new DateTime(1995, 1, 1), "TestTime1"),
                    (new DateTime(2000, 1, 1), "TestTime2"))
                .Should()
                .BeFalse();
        }
    }

    [Test]
    public static void RemoveTimelineTest() //bool Remove(Timeline<TValue> timeline)
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.Remove(new Timeline<string>(new DateTime(2000, 1, 1), "TestTime2"));

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(4);

            timeline.Contains(new DateTime(2000, 1, 1), "TestTime2")
                .Should()
                .BeFalse();
        }
    }

    [Test]
    public static void RemoveTimeTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.RemoveTimes(new DateTime(2000, 1, 1));

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(4);

            timeline.ContainsTime(new DateTime(2000, 1, 1))
                .Should()
                .BeFalse();
        }
    }

    [Test]
    public static void RemoveValueTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        timeline.RemoveValues("TestTime1");

        using (new AssertionScope())
        {
            timeline.Count
                .Should()
                .Be(4);

            timeline.ContainsValue("TestTime1")
                .Should()
                .BeFalse();
        }
    }

    [Test]
    public static void ToArrayTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var array = timeline.ToArray();

        timeline.Count
            .Should()
            .Be(array.Length);

        using (new AssertionScope())
        {
            var i = 0;
            foreach (var (time, value) in timeline)
            {
                time
                    .Should()
                    .Be(array[i].Time);

                value
                    .Should()
                    .Be(array[i].Value);

                ++i;
            }
        }
    }

    [Test]
    public static void ToListTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var list = timeline.ToList();

        timeline.Count
            .Should()
            .Be(list.Count);

        using (new AssertionScope())
        {
            var i = 0;
            foreach (var (time, value) in timeline)
            {
                time
                    .Should()
                    .Be(list[i].Time);

                value
                    .Should()
                    .Be(list[i].Value);

                ++i;
            }
        }
    }

    [Test]
    public static void ToDictionaryTest()
    {
        var timeline = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var dictionary = timeline.ToDictionary();

        var timelineList = new List<(DateTime Time, string Value)>();
        foreach (var pair in timeline)
        {
            timelineList.Add(pair);
        }

        var dictionaryList = new List<(DateTime Time, string Value)>();
        foreach (var (key, value) in dictionary)
        {
            dictionaryList.Add((key, value));
        }

        timelineList.OrderBy(pair => pair.Time);
        dictionaryList.OrderBy(pair => pair.Time);

        timelineList.Count
            .Should()
            .Be(dictionaryList.Count);

        using (new AssertionScope())
        {
            for (var i = 0; i < timelineList.Count; ++i)
            {
                timelineList[i].Time
                    .Should()
                    .Be(dictionaryList[i].Time);

                timelineList[i].Value
                    .Should()
                    .Be(dictionaryList[i].Value);
            }
        }
    }

    [Test]
    public static void EqualityOperatorTest()
    {
        var timeline1 = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var timeline2 = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        (timeline1 == timeline2)
            .Should()
            .BeTrue();
    }

    [Test]
    public static void InequalityOperatorTest()
    {
        var timeline1 = new Timeline<string>
        {
            { new DateTime(1995, 1, 1), "TestTime1" },
            { new DateTime(2000, 1, 1), "TestTime2" },
            { new DateTime(2005, 1, 1), "TestTime3" },
            { new DateTime(2010, 1, 1), "TestTime4" },
            { new DateTime(2015, 1, 1), "TestTime5" },
        };

        var timeline2 = new Timeline<string>
        {
            { new DateTime(1895, 1, 1), "TestTime6" },
            { new DateTime(1900, 1, 1), "TestTime7" },
            { new DateTime(1905, 1, 1), "TestTime8" },
            { new DateTime(1910, 1, 1), "TestTime9" },
            { new DateTime(1915, 1, 1), "TestTime10" },
        };

        (timeline1 == timeline2)
            .Should()
            .BeFalse();
    }
}
