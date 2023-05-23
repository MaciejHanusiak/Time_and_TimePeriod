using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Time_and_TimePeriod_Lib;

namespace Time_and_TimePeriod
{
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {


        public byte Hours { get; init; }
        public byte Minutes { get; init; }
        public byte Seconds { get; init; }
        public short Milliseconds { get; init; }


        public Time(byte Hours, byte Minutes, byte Seconds, short Milliseconds)
        {
            this.Hours = (byte)(Hours % 24);
            this.Minutes = (byte)(Minutes % 60);
            this.Seconds = (byte)(Seconds % 60);
            this.Milliseconds = (short)(Milliseconds % 1000);


            if (Milliseconds > 999 || Seconds > 59 || Minutes > 59 || Hours > 23)
                throw new ArgumentException("Invalid data applied");


        }
        #region constructors
        public Time() : this(0, 0, 0, 0) { }
        public Time(byte Hours, byte Minutes, byte Seconds) : this(Hours, Minutes, Seconds, 0) { }
        public Time(byte Hours, byte Minutes) : this(Hours, Minutes, 0, 0) { }
        public Time(byte Hours) : this(Hours, 0, 0, 0) { }

        public Time(string hh, string mm, string ss, string ms)
        {
            this.Hours = byte.Parse(hh);
            this.Minutes = byte.Parse(mm);
            this.Seconds = byte.Parse(ss);
            this.Milliseconds = short.Parse(ms);
        }
        public Time(string str)
        {
            var split = str.Split(':', StringSplitOptions.TrimEntries);
            byte h;
            if (byte.TryParse(split[0], out h))
                Hours = h;
            else
                throw new ArgumentException();
            byte m;
            if (byte.TryParse(split[1], out m))
                Minutes = m;
            else
                throw new ArgumentException();
            byte s;
            if (byte.TryParse(split[2], out s))
                Seconds = s;
            else
                throw new ArgumentException();
            short ms;
            if (short.TryParse(split[3], out ms)) 
                Milliseconds = ms;
            else 
                throw new ArgumentException();

            if (Hours > 23 || Minutes > 59 || Seconds > 59 || Milliseconds > 999)
                throw new ArgumentOutOfRangeException();

        }
        #endregion
        public override string ToString()
        {
            return string.Format("{0:00}:{1:00}:{2:00}:{3:000}", Hours, Minutes, Seconds,Milliseconds);
        }

    
        public int CompareTo(Time other)
        {
            if (Hours != other.Hours)
                return Hours - other.Hours;
            if (Minutes != other.Minutes)
                return Minutes - other.Minutes;
            if (Seconds != other.Seconds)
                return Seconds - other.Seconds;
            if (Milliseconds != other.Milliseconds)
                return Milliseconds - other.Milliseconds;
            return 0;
        }

        public bool Equals(Time other)
        {
            if (this.Hours == other.Hours && this.Minutes == other.Minutes && this.Seconds == other.Seconds
             && this.Milliseconds == other.Milliseconds) return true;
            else
                return false;
        }

#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        public override bool Equals(object obj)
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
            if (obj is Time)
                return Equals((Time)obj);
            return false;
        }

        public override int GetHashCode()
        {
            return (Hours, Minutes, Seconds, Milliseconds).GetHashCode();
        }
        #region operators
        public static Time operator +(Time a, Time b)
        {
            int sumH = (a.Hours + b.Hours);
            if (sumH > 23) { sumH %= 24; }
            int sumM = (a.Minutes + b.Minutes);
            if (sumM > 59) { sumM %= 60; sumH += 1; }
            int sumS = (a.Seconds + b.Seconds);
            if (sumS > 59) { sumS %= 60;  sumM += 1; }
            int sumMS = (a.Milliseconds + b.Milliseconds);
            if (sumMS > 999) { sumMS %= 1000; sumS += 1; }

            return new Time((byte)sumH, (byte)sumM, (byte)sumS, (short)sumMS);
        }
     

        public static bool operator ==(Time a, Time b) => a.Equals(b);
        public static bool operator !=(Time a, Time b) => !a.Equals(b);
        public static bool operator >(Time a, Time b) => a.CompareTo(b) > 0;
        public static bool operator <(Time a, Time b) => a.CompareTo(b) < 0;
        public static bool operator <=(Time a, Time b) => a.CompareTo(b) <= 0;
        public static bool operator >=(Time a, Time b) => a.CompareTo(b) >= 0;
        #endregion

        public Time Plus(TimePeriod timePeriod)
        {

            long totalMilliSeconds = Hours * 3600000 + Minutes * 60000 + Seconds * 1000 + Milliseconds;
            long periodMilliSeconds = timePeriod.hours * 3600000 + timePeriod.minutes * 60000 + timePeriod.seconds * 1000 + timePeriod.milliseconds;
            long resultMilliSeconds = (totalMilliSeconds + periodMilliSeconds) % (24 * 3600000);

            byte resultHours = (byte)(resultMilliSeconds / 3600000);
            byte resultMinutes = (byte)(resultMilliSeconds % 3600000 / 60000);
            byte resultSecondsFinal = (byte)(resultMilliSeconds % 60000 / 1000);
            short resultMilliseconds = (short)(resultMilliSeconds % 1000);

            return new Time(resultHours, resultMinutes, resultSecondsFinal,resultMilliseconds);
        }
        public static Time Plus(Time time, TimePeriod timePeriod){ return time.Plus(timePeriod); }
        public static Time operator +(Time time, TimePeriod timePeriod){return time.Plus(timePeriod);}

        public Time Minus(TimePeriod timePeriod)
        {

            long totalMilliSeconds = Hours * 3600000 + Minutes * 60000 + Seconds * 1000 + Milliseconds;
            long periodMilliSeconds = timePeriod.hours * 3600000 + timePeriod.minutes * 60000 + timePeriod.seconds * 1000 + timePeriod.milliseconds;
            long resultMilliSeconds = (totalMilliSeconds - periodMilliSeconds) % (24 * 3600000);

            byte resultHours = (byte)(resultMilliSeconds / 3600000);
            byte resultMinutes = (byte)(resultMilliSeconds % 3600000 / 60000);
            byte resultSecondsFinal = (byte)(resultMilliSeconds % 60000 / 1000);
            short resultMilliseconds = (short)(resultMilliSeconds % 1000);

            return new  Time(resultHours, resultMinutes, resultSecondsFinal, resultMilliseconds);
        }

        public static Time Minus(Time time, TimePeriod timePeriod){return time.Minus(timePeriod);}

        public static Time operator -(Time time, TimePeriod timePeriod){ return time.Minus(timePeriod);}
    }
}
