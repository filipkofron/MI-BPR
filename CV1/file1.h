#ifndef __file1_h__
#define __file1_h__

#if COMPILING_DLL
#define DLL __declspec(dllexport)
#elif IMPORTING_FROM_DLL
#define DLL __declspec(dllimport)
#else
#define DLL
#endif

#if __cplusplus
extern "C" {
#endif

int DLL File1_Funkce1( int a, int b );
int DLL __stdcall File1_Funkce2( int a, int b );
int DLL __stdcall File1_Funkce3( int a, int b );

#if __cplusplus
}
#endif

#endif
