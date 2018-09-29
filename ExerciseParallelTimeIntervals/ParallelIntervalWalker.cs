﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExerciseParallelTimeIntervals {
  public static class ParallelIntervalWalker {
    private class SortedDateTimes {
      List<DateTime> _items = new List<DateTime>();

      public void Add(Interval interval) {
        Add(interval.Start);
        Add(interval.End);
      }
      private void Add(DateTime dateTime) {
        for (var i = 0; i < _items.Count; i++) {
          DateTime current = _items[i];
          if (dateTime < _items[i]) {
            _items.Insert(i, dateTime);
            return;
          } else if (dateTime == current) {
            return;
          }
        }
        _items.Add(dateTime);
      }
      public void RemoveAt(int index) {
        _items.RemoveAt(index);
      }
      public bool TryGetNext(DateTime dateTime, out DateTime result) {
        for (int i = 0; i < _items.Count - 1; i++) {
          if(_items[i] == dateTime) {
            result = _items[i + 1];
            return true;
          }
        }
        result = default(DateTime);
        return false;
      }
      public DateTime First() {
        return _items[0];
      }
    }
    public static List<Interval> GetAllIntervals(List<Interval> intervals1, List<Interval> intervals2) {
      List<Interval> allIntervals = new List<Interval>();
      int index1 = 0;
      int index2 = 0;
      int count1 = intervals1.Count;
      int count2 = intervals2.Count;
      Interval current1 = intervals1[index1++];
      Interval current2 = intervals2[index2++];
      SortedDateTimes cumomentsts = new SortedDateTimes();
      cumomentsts.Add(current1);
      cumomentsts.Add(current2);
      DateTime start = cumomentsts.First();
      DateTime currentCut;
      cumomentsts.TryGetNext(start, out currentCut);
      bool hasStart = true;
      do {
        if (hasStart) {
          allIntervals.Add(new Interval(start, currentCut));
        }
        if (currentCut == current1.End && index1 < count1) {
          current1 = intervals1[index1++];
          cumomentsts.Add(current1);
        }
        if (currentCut == current2.End && index2 < count2) {
          current2 = intervals2[index2++];
          cumomentsts.Add(current2);
        }
        if (current1.Intersects(currentCut) || current2.Intersects(currentCut)) {
          start = currentCut;
          hasStart = true;
        } else {
          hasStart = false;
        }
        cumomentsts.RemoveAt(0);
      } while (cumomentsts.TryGetNext(currentCut, out currentCut));
      return allIntervals;
    }
  }
}
