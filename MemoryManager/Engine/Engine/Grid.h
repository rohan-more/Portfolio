#ifndef Grid_h
#define Grid_h

#include <iostream>
#include <new>
#include <cstdlib>

class Grid
{
public: int grid[10][10];

		static void setX(int);
		
		static void setY(int);
private:
		static int row;
		static int col;
};



#endif 