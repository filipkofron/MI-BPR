using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SharedClock
{
  class DateTimeSetter
  {
    public static void SetDateTime(DateTime dt)
    {
      ProcessStartInfo startInfo = new ProcessStartInfo("DateTimeSetter.exe");
      startInfo.WindowStyle = ProcessWindowStyle.Normal;

      startInfo.Arguments = $"{dt.Year} {dt.Month} {dt.Day} {dt.Hour} {dt.Minute} {dt.Second}";
      Process p = Process.Start(startInfo);
      bool fail = true;
      if (p != null)
      {
        fail = false;
        fail |= !p.WaitForExit(10000);
        fail |= p.ExitCode != 0;
      }

      if (fail)
      {
        throw new InvalidProgramException("Could not set date/time using the supplied DateTimeSetter.");
      }
    }
  }
}
