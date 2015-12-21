using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Priviliges
{
    public class Priviliges
    {
      [DllImport("PriviligesLib.dll", CallingConvention = CallingConvention.Cdecl)]
      public static extern void GiveUpAllExceptServer();

      [DllImport("PriviligesLib.dll", CallingConvention = CallingConvention.Cdecl)]
      public static extern void GiveUpAllExceptDateTime();
  }
}
