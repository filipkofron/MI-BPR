#include <windows.h>
#include <stdio.h>

int main(int argc, char** argv)
{
  MessageBox(
    NULL,
    (LPCWSTR)L"Resource not available\nDo you want to try again?",
    (LPCWSTR)L"Account Details",
    MB_ICONWARNING | MB_CANCELTRYCONTINUE | MB_DEFBUTTON2
    );
  char buffer[16];
  printf("Enter your name: ");
  gets(buffer);
  printf("Hello %s!\n", buffer);
  return 0;
}
