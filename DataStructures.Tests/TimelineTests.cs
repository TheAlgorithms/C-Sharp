using NUnit.Framework;
using System;
using System.Linq;

namespace DataStructures.Tests
{
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            Assert.IsTrue(timeline.Count == 5);
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            Assert.IsTrue(timeline.TimesCount == timeline.GetAllTimes().Length);
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            Assert.IsTrue(timeline.ValuesCount == timeline.GetAllValues().Length);
        }

        [Test]
        public static void IndexerGetTest()
        {
            var timeline = new Timeline<string>
            {
                { new DateTime(1995, 1, 1), "TestTime1" },
                { new DateTime(2000, 1, 1), "TestTime2" },
                { new DateTime(2005, 1, 1), "TestTime3" },
                { new DateTime(2010, 1, 1), "TestTime4" },
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            Assert.IsTrue(timeline[new DateTime(2000, 1, 1)][0] == "TestTime2");
        }

        [Test]
        public static void IndexerSetTest()
        {
            var timeline = new Timeline<string>
            {
                { new DateTime(1995, 1, 1), "TestTime1" },
                { new DateTime(2000, 1, 1), "TestTime2" },
                { new DateTime(2005, 1, 1), "TestTime3" },
                { new DateTime(2010, 1, 1), "TestTime4" },
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            timeline[new DateTime(2000, 1, 1)] = new[] { "TestDate2Modified" };
            Assert.IsTrue(timeline[new DateTime(2000, 1, 1)][0] == "TestDate2Modified");
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            var timeline2 = new Timeline<string>
            {
                { new DateTime(1995, 1, 1), "TestTime1" },
                { new DateTime(2000, 1, 1), "TestTime2" },
                { new DateTime(2005, 1, 1), "TestTime3" },
                { new DateTime(2010, 1, 1), "TestTime4" },
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            // timeline1 is equal to timeline2
            Assert.IsTrue(timeline1.Equals(timeline2));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            timeline.Clear();
            Assert.IsTrue(timeline.Count == 0);
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            var array = new (DateTime Time, string Value)[timeline.Count];
            timeline.CopyTo(array, 0);
            Assert.IsTrue(timeline.Count == array.Length);
            var i = 0;
            foreach (var (time, value) in timeline)
            {
                Assert.IsTrue(time == array[i].Time && value == array[i].Value);
                i++;
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            var times = timeline.GetAllTimes();
            var i = 0;
            foreach (var (time, value) in timeline)
            {
                Assert.IsTrue(time == times[i]);
                i++;
            }
        }

        [Test]
        public static void GetTimesByValueTest()
        {
            var timeline = new Timeline<string>
            {
                { new DateTime(1995, 1, 1), "TestTime1" },
                { new DateTime(2000, 1, 1), "TestTime2" },
                { new DateTime(2005, 1, 1), "TestTime3" },
                { new DateTime(2010, 1, 1), "TestTime4" },
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            Assert.IsTrue(timeline.GetTimesByValue("TestTime2")[0] == new DateTime(2000, 1, 1));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            var times = timeline.GetTimesBefore(new DateTime(2003, 1, 1));
            Assert.IsTrue(times.Length == 2);
            Assert.IsTrue(times[0] == new DateTime(1995, 1, 1));
            Assert.IsTrue(times[1] == new DateTime(2000, 1, 1));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            var times = timeline.GetTimesAfter(new DateTime(2003, 1, 1));
            Assert.IsTrue(times.Length == 3);
            Assert.IsTrue(times[0] == new DateTime(2005, 1, 1));
            Assert.IsTrue(times[1] == new DateTime(2010, 1, 1));
            Assert.IsTrue(times[2] == new DateTime(2015, 1, 1));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            var values = timeline.GetAllValues();
            var i = 0;
            foreach (var (time, value) in timeline)
            {
                Assert.IsTrue(value == values[i]);
                i++;
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            Assert.IsTrue(timeline.GetValuesByTime(new DateTime(2000, 1, 1))[0] == "TestTime2");
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            var array = timeline.GetValuesBefore(new DateTime(2003, 1, 1)).ToArray();
            Assert.IsTrue(array.Length == 2);
            Assert.IsTrue(array[0].Time == new DateTime(1995, 1, 1));
            Assert.IsTrue(array[1].Time == new DateTime(2000, 1, 1));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            var array = timeline.GetValuesAfter(new DateTime(2003, 1, 1)).ToArray();
            Assert.IsTrue(array.Length == 3);
            Assert.IsTrue(array[0].Time == new DateTime(2005, 1, 1));
            Assert.IsTrue(array[1].Time == new DateTime(2010, 1, 1));
            Assert.IsTrue(array[2].Time == new DateTime(2015, 1, 1));
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
                { new DateTime(2015, 1, 1, 10, 0, 0, 750), "TestTime5" }
            };
            var query = timeline.GetValuesByMillisecond(750);
            Assert.IsTrue(query.Count == 2 && timeline.Contains(query));
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
                { new DateTime(2015, 1, 1, 10, 0, 20), "TestTime5" }
            };
            var query = timeline.GetValuesBySecond(20);
            Assert.IsTrue(query.Count == 2 && timeline.Contains(query));
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
                { new DateTime(2015, 1, 1, 10, 40, 0), "TestTime5" }
            };
            var query = timeline.GetValuesByMinute(40);
            Assert.IsTrue(query.Count == 2 && timeline.Contains(query));
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
                { new DateTime(2015, 1, 1, 16, 0, 0), "TestTime5" }
            };
            var query = timeline.GetValuesByHour(16);
            Assert.IsTrue(query.Count == 2 && timeline.Contains(query));
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
                { new DateTime(2015, 1, 20), "TestTime5" }
            };
            var query = timeline.GetValuesByDay(20);
            Assert.IsTrue(query.Count == 2 && timeline.Contains(query));
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
                { new DateTime(2015, 1, 1, 21, 15, 40, 600), "TestTime5" }
            };
            var query = timeline.GetValuesByTimeOfDay(new TimeSpan(0, 21, 15, 40, 600));
            Assert.IsTrue(query.Count == 2 && timeline.Contains(query));
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
                { new DateTime(2015, 1, 8), "TestTime5" }  //Thursday
            };
            var query = timeline.GetValuesByDayOfWeek(DayOfWeek.Monday);
            Assert.IsTrue(query.Count == 2 && timeline.Contains(query));
        }

        [Test]
        public static void GetValuesByDayOfYearTest()
        {
            var timeline = new Timeline<string>
            {
                { new DateTime(1985, 1, 3), "TestTime1" },  //3rd day of year
                { new DateTime(1990, 1, 7), "TestTime2" },  //7th day of year
                { new DateTime(1995, 1, 22), "TestTime3" }, //22th day of year
                { new DateTime(2000, 2, 1), "TestTime4" },  //32th day of year
                { new DateTime(2005, 2, 1), "TestTime5" }   //32th day of year
            };
            var query = timeline.GetValuesByDayOfYear(32);
            Assert.IsTrue(query.Count == 2 && timeline.Contains(query));
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
                { new DateTime(2015, 4, 1), "TestTime5" }
            };
            var query = timeline.GetValuesByMonth(4);
            Assert.IsTrue(query.Count == 2 && timeline.Contains(query));
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
                { new DateTime(2005, 1, 2), "TestTime5" }
            };
            var query = timeline.GetValuesByYear(2005);
            Assert.IsTrue(query.Count == 2 && timeline.Contains(query));
        }

        [Test]
        public static void AddDateTimeAndTValueTest() //void Add(DateTime time, TValue value)
        {
            var timeline = new Timeline<string>();
            timeline.Add(new DateTime(2015, 1, 1), "TestTime");
            Assert.IsTrue(timeline.Count == 1 && timeline[new DateTime(2015, 1, 1)][0] == "TestTime");
        }

        [Test]
        public static void AddDateTimeAndTValueArrayTest() //void Add(params (DateTime, TValue)[] timeline)
        {
            var timeline = new Timeline<string>();
            timeline.Add((new DateTime(2015, 1, 1), "TestTime1"), (new DateTime(1750, 1, 1), "TestTime2"));
            Assert.IsTrue(
                timeline.Count == 2 &&
                timeline[new DateTime(2015, 1, 1)][0] == "TestTime1" &&
                timeline[new DateTime(1750, 1, 1)][0] == "TestTime2"
            );
        }

        [Test]
        public static void AddTimelineTest() //void Add(Timeline<TValue> timeline)
        {
            var timeline = new Timeline<string>();
            timeline.Add(new Timeline<string>(new DateTime(2015, 1, 1), "TestTime"));
            Assert.IsTrue(timeline.Count == 1 && timeline[new DateTime(2015, 1, 1)][0] == "TestTime");
        }

        [Test]
        public static void AddNowTest()
        {
            var timeline = new Timeline<string>();
            timeline.AddNow("Now");
            Assert.IsTrue(timeline.Count == 1 && timeline.ContainsValue("Now"));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            Assert.IsTrue(timeline.Contains(new DateTime(2000, 1, 1), "TestTime2"));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            Assert.IsTrue(timeline.Contains((new DateTime(1995, 1, 1), "TestTime1"), (new DateTime(2000, 1, 1), "TestTime2")));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            Assert.IsTrue(timeline.Contains(new Timeline<string>(new DateTime(2000, 1, 1), "TestTime2")));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            Assert.IsTrue(timeline.ContainsTime(new DateTime(2000, 1, 1)));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            Assert.IsTrue(timeline.ContainsValue("TestTime1"));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            timeline.Remove(new DateTime(2000, 1, 1), "TestTime2");
            Assert.IsTrue(timeline.Count == 4 && !timeline.Contains(new DateTime(2000, 1, 1), "TestTime2"));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            timeline.Remove((new DateTime(1995, 1, 1), "TestTime1"), (new DateTime(2000, 1, 1), "TestTime2"));
            Assert.IsTrue(
                timeline.Count == 3 &&
                !timeline.Contains((new DateTime(1995, 1, 1), "TestTime1"), (new DateTime(2000, 1, 1), "TestTime2"))
            );
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            timeline.Remove(new Timeline<string>(new DateTime(2000, 1, 1), "TestTime2"));
            Assert.IsTrue(timeline.Count == 4 && !timeline.Contains(new DateTime(2000, 1, 1), "TestTime2"));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            timeline.RemoveTime(new DateTime(2000, 1, 1));
            Assert.IsTrue(timeline.Count == 4 && !timeline.ContainsTime(new DateTime(2000, 1, 1)));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            timeline.RemoveValue("TestTime1");
            Assert.IsTrue(timeline.Count == 4 && !timeline.ContainsValue("TestTime1"));
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            var array = timeline.ToArray();
            Assert.IsTrue(timeline.Count == array.Length);
            var i = 0;
            foreach (var (time, value) in timeline)
            {
                Assert.IsTrue(time == array[i].Time && value == array[i].Value);
                i++;
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            var list = timeline.ToList();
            Assert.IsTrue(timeline.Count == list.Count);
            var i = 0;
            foreach (var (time, value) in timeline)
            {
                Assert.IsTrue(time == list[i].Time && value == list[i].Value);
                i++;
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            var dictionary = timeline.ToDictionary();
            var timelineList = new System.Collections.Generic.List<(DateTime Time, string Value)>();
            foreach (var pair in timeline)
            {
                timelineList.Add(pair);
            }

            var dictionaryList = new System.Collections.Generic.List<(DateTime Time, string Value)>();
            foreach (var (key, value) in dictionary)
            {
                dictionaryList.Add((key, value));
            }

            timelineList.OrderBy(pair => pair.Time);
            dictionaryList.OrderBy(pair => pair.Time);
            Assert.IsTrue(timelineList.Count == dictionaryList.Count);
            for (var i = 0; i < timelineList.Count; i++)
            {
                Assert.IsTrue(timelineList[i].Time == dictionaryList[i].Time && timelineList[i].Value == dictionaryList[i].Value);
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            var timeline2 = new Timeline<string>
            {
                { new DateTime(1995, 1, 1), "TestTime1" },
                { new DateTime(2000, 1, 1), "TestTime2" },
                { new DateTime(2005, 1, 1), "TestTime3" },
                { new DateTime(2010, 1, 1), "TestTime4" },
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            // timeline1 is equal to timeline2
            Assert.IsTrue(timeline1 == timeline2);
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
                { new DateTime(2015, 1, 1), "TestTime5" }
            };
            var timeline2 = new Timeline<string>
            {
                { new DateTime(1895, 1, 1), "TestTime6" },
                { new DateTime(1900, 1, 1), "TestTime7" },
                { new DateTime(1905, 1, 1), "TestTime8" },
                { new DateTime(1910, 1, 1), "TestTime9" },
                { new DateTime(1915, 1, 1), "TestTime10" }
            };
            // timeline1 is not equal to timeline2
            Assert.IsTrue(timeline1 != timeline2);
        }
    }
}
