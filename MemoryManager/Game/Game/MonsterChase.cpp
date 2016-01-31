// RohanChase.cpp : Defines the entry point for the console application.
//

#include <iostream>
#include <string>
#include <ctime>
#include <new>
#include <cstdlib>
#include "RandomNameGenerator.h"
#include "Monster.h"
#include "Grid.h"
using namespace std;

int main()
{

	
	
	int numberOfMonsters;

	Monster monster;
	MyGrid myGrid;
	int row = 10, col = 10;
	
	int i, j;

	for (i = 0; i <= 9; i++)
	{
		for (j = 0; j <= 9; j++)
		{
			myGrid.grid[i][j] = 1;
		}

	}
	cout << "Play MonsterChase:" << endl;
	string heroName;
	cout << "Enter player's name:" << endl;
	cin >> heroName;
	cout << "Enter desired number of monsters:" << endl;
	cin >> numberOfMonsters;
	

	
	RandomNameGenerator randName;
	randName.nameGenerator(numberOfMonsters);
	
	
	char choice;
	int r = 5, c = 5;
	


	do
	{
		cout << "Press W for up, S for down, A for left, D for right and Q to quit game:" << endl;
		cout << "Enter your choice:";
		cin >> choice;
		cout << endl;
		switch (choice)
		{
		case 'd':
			cout << "Player moved right." << endl;
			c = c + 1;
			break;
		case 'a':
			cout << "Player moved left." << endl;
			c = c - 1;
			break;
		case 'w':
			cout << "Player moved upwards." << endl;
			r = r - 1;
			break;
		case 's':
			cout << "Player moved downwards." << endl;
			r = r + 1;
			break;
		case 'q':
			cout << "Player quits." << endl;
			break;
		default:
			cout << "Wrong input. Press W for up, S for down, A for left, D for right and Q to quit." << endl;
			break;

		}
		cout << heroName << "'s position:" << r << "," << c << endl;

		int monsterNewRow, monsterNewCol;

		for (int i = 0; i < numberOfMonsters; i++)
		{
			char monsterName = monster.nameOfMonsters[i];

			monsterNewRow = rand() % 8;
			monsterNewCol = rand() % 8;

			cout << "Monster " << monsterName << " is at :" << monsterNewRow << "," << monsterNewCol << endl;
			int monsterLoc[8][8];
			for (int j = 0; j <= numberOfMonsters; j++)
			{
				monsterLoc[i][j] = monsterLoc[monsterNewRow][monsterNewCol];
			}

			//monster.nameOfMonsters[monsterNewRow][monsterNewCol] == monsterName;
			if ((r == monsterNewRow && c == monsterNewCol) || (monsterLoc[i][j] == monsterLoc[i - 1][j - 1]))

			{
				cout << "Monster " << monsterName << " has been killed." << endl;

				monster.nameOfMonsters[i] = 'null';
				for (int i = 0; i <= numberOfMonsters; i++)
				{
					monster.nameOfMonsters[i] = monster.nameOfMonsters[i + 1];
				}
				numberOfMonsters--;

			}


		}

	} while (choice != 'q');


	cout << "Game ends." << endl;

}