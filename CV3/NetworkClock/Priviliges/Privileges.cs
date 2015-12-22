using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Privileges
{
    public class Privileges
    {
      [DllImport("PrivilegesLib.dll", CallingConvention = CallingConvention.Cdecl)]
      public static extern void GiveUpAllExceptServer();

      [DllImport("PrivilegesLib.dll", CallingConvention = CallingConvention.Cdecl)]
      public static extern void GiveUpAllExceptDateTime();
  }
}
