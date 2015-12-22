// PriviligesLib.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "PrivLib.h"
#include <windows.h>
#include <set>
#include <vector>

/**
 * Shows an unicode message via an User interface form (MessageBox).
 */
void ShowMessage(const char* msg)
{
  size_t maxWideLen = strlen(msg);
  wchar_t* wchars = new wchar_t[maxWideLen + 1];
  wchars[maxWideLen] = 0;
#pragma warning(push)
#pragma warning(disable : 4996)
  mbstowcs(wchars, msg, maxWideLen);
#pragma warning(pop)
  MessageBox(nullptr, wchars, L"Kraken strikes!", MB_OK);
  delete[] wchars;
}

/**
 * Upon an error shows given formatted message.
 * Note: expanded (formatted) message can only grow by 1024 characters in length.
 */
void Error(const char* format, ...)
{
  size_t maxLen = strlen(format) + 1024;
  va_list args;
  va_start(args, format);
  char* buffer = new char[maxLen + 1];
  vsnprintf(buffer, maxLen, format, args);
  va_end(args);
  buffer[maxLen] = '\0';
  ShowMessage(buffer);
  delete[] buffer;
  exit(1);
}

void EnablePrivs(const std::vector<LUID> &enableLuids, HANDLE token, PTOKEN_PRIVILEGES tokenPrivs, DWORD bufferLen)
{
  DWORD	i;
  LUID	luidShutdown;

  LookupPrivilegeValue(NULL, SE_SHUTDOWN_NAME, &luidShutdown);

  for (i = 0; i < tokenPrivs->PrivilegeCount; ++i)
  {
    if (tokenPrivs->Privileges[i].Luid.HighPart == luidShutdown.HighPart && tokenPrivs->Privileges[i].Luid.LowPart == luidShutdown.LowPart)
    {
      tokenPrivs->Privileges[i].Attributes = SE_PRIVILEGE_ENABLED;
    }
  }

  if (AdjustTokenPrivileges(token, FALSE, tokenPrivs, bufferLen, NULL, NULL) == ERROR_SUCCESS)
  {
    Error("Could not adjust given privileges: %d", GetLastError());
  }
}

/**
* Will give up all privileges except the ones given in the set.
*/
void DoGiveUpAllExcept(const std::vector<LUID> &keepLuids, HANDLE token, PTOKEN_PRIVILEGES tokenPrivs, DWORD bufferLen)
{
  for (DWORD i = 0; i < tokenPrivs->PrivilegeCount; ++i)
  {
    bool found = false;
    for (auto luid : keepLuids)
    {
      found |= tokenPrivs->Privileges[i].Luid.HighPart == luid.HighPart && tokenPrivs->Privileges[i].Luid.LowPart == luid.LowPart;
    }
    if (!found)
    {
      tokenPrivs->Privileges[i].Attributes = SE_PRIVILEGE_REMOVED;
    }
  }
  BOOL success = AdjustTokenPrivileges(token, FALSE, tokenPrivs, bufferLen, nullptr, nullptr);
  if (!success)
  {
    Error("An error while giving up priviliges: %d\n", GetLastError());
  }
}

/**
 * Will give up all priviliges except the ones given in the set.
 */
void GiveUpAllExcept(const std::set<LPCWSTR> &keepSet)
{
  std::vector<LUID> luids;
  luids.resize(keepSet.size());
  int i = 0;
  for (auto pluid : keepSet)
  {
    LookupPrivilegeValue(nullptr, pluid, &luids[i]);
    i++;
  }
  HANDLE process = GetCurrentProcess();
  HANDLE token = nullptr;
  DWORD bufferLen = 0;
  PTOKEN_PRIVILEGES tokenPrivs;
  if (process)
  {
    if (OpenProcessToken(process, TOKEN_QUERY | TOKEN_ADJUST_PRIVILEGES, &token))
    {
      DWORD errorCode = ERROR_SUCCESS;
      if (!GetTokenInformation(token, TokenPrivileges, NULL, 0, &bufferLen))
      {
        errorCode = GetLastError();
        if (errorCode == ERROR_INSUFFICIENT_BUFFER)
        {
          errorCode = ERROR_SUCCESS;
        }
        else
        {
          Error("Could not get the process token info: %d", errorCode);
        }
      }
      if (errorCode == ERROR_SUCCESS)
      {
        tokenPrivs = static_cast<PTOKEN_PRIVILEGES>(malloc(bufferLen));
        if (GetTokenInformation(token, TokenPrivileges, (LPVOID) tokenPrivs, bufferLen, &bufferLen))
        {
          DoGiveUpAllExcept(luids, token, tokenPrivs, bufferLen);
          EnablePrivs(luids, token, tokenPrivs, bufferLen);
        }
        else
        {
          errorCode = GetLastError();
          Error("Could not get the process token info: %d", errorCode);
        }
          
        free(tokenPrivs);
      }
      if (token)
      {
        CloseHandle(token);
      }
    }
    else
    {
      Error("Could not get the process token handle.");
    }
  }
  else
  {
    Error("Could not get the process handle.");
  }
}

/**
 * Give up all priviliges except those to be able to run server.
 */
void GiveUpAllExceptServer()
{
  GiveUpAllExcept({ });
}

/**
* Give up all priviliges except those to change date/time.
*/
void GiveUpAllExceptDateTime()
{
  GiveUpAllExcept({ SE_SYSTEMTIME_NAME });
}
