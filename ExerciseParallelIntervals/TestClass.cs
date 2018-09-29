using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExerciseParallelTimeIntervals {
  [TestFixture]
  internal class TestClass {

    [Test]
    public void TestGetAllIntervalsWithConsecutiveHourAnd15MinutesSeries() {
      // arrange
      List<Interval> intervals1 = new List<Interval>();
      List<Interval> intervals2 = new List<Interval>();
      DateTime start;
      DateTime end;
      start = new DateTime(2018, 1, 1);
      for (int i = 0; i < 1000000; i++) {
        end = start + TimeSpan.FromHours(1);
        intervals1.Add(new Interval(start, end));
        for (int j = 0; j < 4; j++) {
          end = start + TimeSpan.FromMinutes(15);
          intervals2.Add(new Interval(start, end));
          start = start + TimeSpan.FromMinutes(15);
        }
      }
      // act
      List<Interval> result = ParallelIntervalWalker.GetAllIntervals(intervals1, intervals2);
      // assert
      Assert.That(result, Is.EqualTo(intervals2));
    }

    [TestCaseSource(typeof(TestData), nameof(TestData.DefinedIntervalCollections))]
    public void TestGetAllIntervalsWithDefinedIntervalCollections(List<Interval> intervals1, List<Interval> intervals2, List<Interval> expectedResult) {
      // arrange & act
      List<Interval> result = ParallelIntervalWalker.GetAllIntervals(intervals1, intervals2);
      // assert
      Assert.That(result, Is.EqualTo(expectedResult));
    }
  }
}
