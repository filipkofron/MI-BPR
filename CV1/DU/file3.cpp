#include <stdio.h>
#include "file1.h"

int main(int argc, char** argv)
{
	int iSum = File1_Funkce1(1, 2);
	File1_Funkce2(1, 2);
	File1_Funkce3(1, 2);

	printf("The sum is: %d\n", iSum);
	return 0;
}