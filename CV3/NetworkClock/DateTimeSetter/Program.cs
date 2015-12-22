using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security;
using System.Text;
using static Privileges.Privileges;

namespace DateTimeSetter
{
  class Program
  {
    [StructLayout(LayoutKind.Sequential)]
    public struct SYSTEMTIME
    {
      public short Year;
      public short Month;
      public short DayOfWeek;
      public short Day;
      public short Hour;
      public short Minute;
      public short Second;
      public short Milliseconds;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool SetSystemTime(ref SYSTEMTIME st);

    static void CheckYear(short year)
    {
      if (year < 1700 || year > 3000)
        throw new ArgumentException("Invalid year.");
    }

    static void CheckMonth(short month)
    {
      if (month < 1 || month > 12)
        throw new ArgumentException("Invalid month.");
    }

    static void CheckDay(short day)
    {
      if (day < 1 || day > 31)
        throw new ArgumentException("Invalid day.");
    }

    static void CheckHour(short hour)
    {
      if (hour < 0 || hour > 23)
        throw new ArgumentException("Invalid hour.");
    }

    static void CheckMinute(short minute)
    {
      if (minute < 0 || minute > 59)
        throw new ArgumentException("Invalid minute.");
    }

    static void CheckSecond(short second)
    {
      if (second < 0 || second > 59)
        throw new ArgumentException("Invalid second.");
    }

    static int Main(string[] args)
    {
      GiveUpAllExceptDateTime();

      bool ret = false;
      if (args.Length != 6)
        return 1;
      try
      {
        var st = new SYSTEMTIME
        {
          Year = short.Parse(args[0]),
          Month = short.Parse(args[1]),
          Day = short.Parse(args[2]),
          Hour = short.Parse(args[3]),
          Minute = short.Parse(args[4]),
          Second = short.Parse(args[5])
        };
        CheckYear(st.Year);
        CheckMonth(st.Month);
        CheckDay(st.Day);
        CheckHour(st.Hour);
        CheckMinute(st.Minute);
        CheckSecond(st.Second);

        Console.WriteLine($"Setting time to {st.Year}/{st.Month}/{st.Day} {st.Hour}:{st.Minute}:{st.Second}");
        ret = SetSystemTime(ref st);
      }
      catch (Exception e)
      {
        Console.WriteLine("Invalid date/time: " + e.Message);
        return 1;
      }

      if (!ret)
      {
        Console.WriteLine("Error setting date/time: " + Marshal.GetLastWin32Error());
      }

      return ret ? 0 : 1;
    }
  }
}
