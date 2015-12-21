// PriviligesLib.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "PrivLib.h"
#include <windows.h>
#include <set>

void GiveUpAllExcept(std::set<PLUID> keepSet)
{
  HANDLE process = GetCurrentProcess();
  if (process)
  {

  }
  else
  {
    MessageBox(NULL, L"Kraken strikes!", L"Kraken strikes!", ALERT_SYSTEM_ERROR);
  }
}

void GiveUpAllExceptServer()
{
  MessageBox(NULL, L"Kraken strikes! Not implemented.", L"Kraken strikes!", ALERT_SYSTEM_ERROR);
}

void GiveUpAllExceptDateTime()
{
  MessageBox(NULL, L"Kraken strikes! Not implemented.", L"Kraken strikes!", ALERT_SYSTEM_ERROR);
}
