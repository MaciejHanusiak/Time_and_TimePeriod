using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Time_and_TimePeriod;

namespace Time_and_TimePeriod_Lib
{
    public readonly struct TimePeriod
    {
        public long TimLngth_milliseconds { get; init; }

        public TimePeriod(long? hours = 0, long? minutes = 0, long? seconds = 0, long? milliseconds = 0)
        {
            if (hours is null || minutes is null || seconds is null || milliseconds is null) throw new ArgumentNullException();
            if (hours < 0 || minutes < 0 || seconds < 0 || milliseconds < 0)
                throw new ArgumentException("Negative values are not allowed.");

            TimLngth_milliseconds = (long)(hours * 3600000 + minutes * 60000 + seconds * 1000 + milliseconds);
        }

        public TimePeriod(long? hours, long? minutes, long? seconds) : this(hours, minutes, seconds, 0) { }
        public TimePeriod(long? hours, long? minutes) : this(hours, minutes, 0, 0) { }
        public TimePeriod(long? hours) : this(hours, 0, 0, 0) { }

        public TimePeriod(Time start, Time end)
        {
            TimLngth_milliseconds = Math.Abs((end.Hours * 3600000 + end.Minutes * 60000 + end.Seconds * 1000 + end.Milliseconds) -
                            (start.Hours * 3600000 + start.Minutes * 60000 + start.Seconds * 1000 + start.Milliseconds));
        }


        public TimePeriod(string duration)
        {
            var split = duration.Split(':', StringSplitOptions.TrimEntries);
            if (split.Length != 4)
                throw new ArgumentException("Wrong format! Try again");

            long h, m, s,ms;
            if (long.TryParse(split[0], out h) &&
                long.TryParse(split[1], out m) &&
                long.TryParse(split[2], out s) &&
                long.TryParse(split[3], out ms ))
            {
                if (h < 0 || m < 0 || s < 0 || ms < 0)
                    throw new ArgumentException("There are negative values! Try again");

                TimLngth_milliseconds = h * 3600000 + m * 60000 + s * 1000 + ms;
            }
            else
            {
                throw new ArgumentException("Wrong format! Try again");
            }

            
        }
        public long _TimLngth_milliseconds => TimLngth_milliseconds;

        public long hours => TimLngth_milliseconds / 3600000;
        public long minutes => (TimLngth_milliseconds % 3600000) / 60000;
        public long seconds => (TimLngth_milliseconds % 60000) / 1000;
        public long milliseconds => TimLngth_milliseconds % 1000;

        public override string ToString()
        {
            return string.Format("{0:00}:{1:00}:{2:00}:{3:000}", hours, minutes, seconds, milliseconds);
        }

        public bool Equals(TimePeriod other)
        {
            return TimLngth_milliseconds == other.TimLngth_milliseconds;
        }
        public override bool Equals(object obj)
        {
            return obj is TimePeriod other && Equals(other);
        }
        public override int GetHashCode()
        {
            return TimLngth_milliseconds.GetHashCode();
        }

        public int CompareTo(TimePeriod other)
        {
            return TimLngth_milliseconds.CompareTo(other.TimLngth_milliseconds);
        }

        public static bool operator ==(TimePeriod t1, TimePeriod t2) { return t1.Equals(t2); }
        
        public static bool operator !=(TimePeriod t1, TimePeriod t2) { return !(t1 == t2);}

        public static bool operator <(TimePeriod t1, TimePeriod t2) {return t1.CompareTo(t2) < 0;}
        public static bool operator <=(TimePeriod t1, TimePeriod t2) {return t1.CompareTo(t2) <= 0;}
        public static bool operator >(TimePeriod t1, TimePeriod t2) {return !(t1 <= t2);}
        public static bool operator >=(TimePeriod t1, TimePeriod t2) {return !(t1 < t2);}

        public TimePeriod Plus(TimePeriod other)
        {
            long new_TimLngth_milliseconds = (_TimLngth_milliseconds + other._TimLngth_milliseconds);
            long newMilliseconds = new_TimLngth_milliseconds % 1000;
            long newSeconds = (new_TimLngth_milliseconds %  60000) / 1000;
            long newMinutes = (new_TimLngth_milliseconds % 3600000) / 60000;
            long newHours = new_TimLngth_milliseconds / 3600000;

            return new TimePeriod(newHours, newMinutes, newSeconds, newMilliseconds);
        }
        public static TimePeriod Plus(TimePeriod time1, TimePeriod time2)
        {

            return time1.Plus(time2);
        }

        public TimePeriod Minus(TimePeriod other)
        {
            long new_TimLngth_milliseconds = (_TimLngth_milliseconds - other._TimLngth_milliseconds);
            long newMilliseconds = new_TimLngth_milliseconds % 1000;
            long newSeconds = (new_TimLngth_milliseconds % 60000) / 1000;
            long newMinutes = (new_TimLngth_milliseconds % 3600000) / 60000;
            long newHours = new_TimLngth_milliseconds / 3600000;
            if (new_TimLngth_milliseconds < 0)
                throw new InvalidOperationException("Resulting time period cannot be negative.");
            return new TimePeriod(newHours, newMinutes, newSeconds, newMilliseconds);
        }
        public static TimePeriod Minus(TimePeriod time1, TimePeriod time2)
        {

            return time1.Minus(time2);
        }

        public TimePeriod Multiply(long other)
        {
            if (other < 0)
                throw new ArgumentException("Multiplier was not positive!");
            long new_TimLngth_milliseconds = (_TimLngth_milliseconds * other);
            long newMilliseconds = new_TimLngth_milliseconds % 1000;
            long newSeconds = (new_TimLngth_milliseconds % 60000) / 1000;
            long newMinutes = (new_TimLngth_milliseconds % 3600000) / 60000;
            long newHours = new_TimLngth_milliseconds / 3600000;

            return new TimePeriod(newHours, newMinutes, newSeconds, newMilliseconds);
        }
        public static TimePeriod Mulitply(TimePeriod time1, long long1)
        {

            return time1.Multiply(long1);
        }
        public static TimePeriod operator +(TimePeriod time1, TimePeriod time2) { return time1.Plus(time2);}

        public static TimePeriod operator -(TimePeriod time1, TimePeriod time2) { return time1.Minus(time2);}
        public static TimePeriod operator *(TimePeriod time1, long long1) {return time1.Multiply(long1);}
    }
}
