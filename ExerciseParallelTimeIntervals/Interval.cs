using System;

namespace ExerciseParallelTimeIntervals {
  public class Interval : IEquatable<Interval> {

    public Interval(DateTime start, DateTime end) {
      Start = start;
      End = end;
    }
    public DateTime Start { get; }
    public DateTime End { get; }

    public bool Intersects(DateTime dateTime) {
      return Start <= dateTime && dateTime < End;
    }
    public override bool Equals(object obj) {
      return Equals(obj as Interval);
    }

    public bool Equals(Interval other) {
      return other != null &&
             Start == other.Start &&
             End == other.End;
    }

    public override int GetHashCode() {
      var hashCode = -1676728671;
      hashCode = hashCode * -1521134295 + Start.GetHashCode();
      hashCode = hashCode * -1521134295 + End.GetHashCode();
      return hashCode;
    }

  }
}
