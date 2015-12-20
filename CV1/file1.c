#include "file1.h"

int File1_Funkce1( int a, int b )
{
	return a+b;
}

int __stdcall File1_Funkce2( int a, int b )
{
	return a+b;
}

int __stdcall File1_Funkce3( int a, int b )
{
	return a * a + b * b;
}
