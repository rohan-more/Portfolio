
#include <iostream>

#include <ctime>
#include <new>
#include <cstdlib>
#include "RandomNameGenerator.h"
#include "Monster.h"
using namespace std;

	void RandomNameGenerator::nameGenerator(int i)
	{
		Monster *monsterArr;
		monsterArr = new Monster[i];
		
		Monster monsterss;
		cout << "The Monster names are: " << endl;
		srand(time(NULL));

		for (int m = 0; m < i; m++)
		{

			int AsciiVal = rand() % 26 + 97;
			int monsterRow = rand() % 9;
			int monsterCol = rand() % 9;
			char AsciiChar = AsciiVal;
			monsterss.nameOfMonsters[m] = AsciiChar;
			cout << "The Monster " << monsterss.nameOfMonsters[m] << " is at " << monsterRow << "," << monsterCol << endl;
			monsterArr[m].setPosX(monsterRow);
			monsterArr[m].setPosY(monsterCol);
			

		}
	}
