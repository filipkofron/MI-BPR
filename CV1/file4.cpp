#include <windows.h>
#include <stdio.h>

int main(int argc, char** argv)
{
	HMODULE hModule = NULL;
	int (*pfnFile1_Funkce1)(int, int) = NULL;
	int (__stdcall *pfnFile1_Funkce2)(int,int) = NULL;

	hModule = LoadLibrary( TEXT("knihovna.dll") );
	if( hModule )
	{
		pfnFile1_Funkce1 = (int (*)(int,int))GetProcAddress( hModule, "File1_Funkce1" );

		if( pfnFile1_Funkce1 )
		{
			printf("Soucet: %d.\n", pfnFile1_Funkce1(1,2) );
		}
		else
		{
			printf("File1_Funkce1: Nenalezena. Chyba %d.\n", GetLastError());
		}		

		FreeLibrary( hModule );
		hModule = NULL;
	}

	return 0;
}
