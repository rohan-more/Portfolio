


#include <new>
#include "Actor.h"
#include "ActorController.h"
#include "MonsterController.h"
#include "PlayerController.h"
#include "ZombieController.h"
#include "FixedSizeAllocator32.h"
#include "Vector3.h"
#include <assert.h>
#include <stdio.h>
#include "ConsolePrint.h"

#ifdef _DEBUG
#define _CRTDBG_MAP_ALLOC
#include <stdlib.h>
#include <iostream>
#include <istream>
#include <cstdlib>
#include <crtdbg.h>
#include<string.h>
#else
#include <stdlib.h>
#endif // _DEBUG


using namespace std; 
using namespace Cohort6;

int main()
{
	//_CrtSetDbgFlag(_CRTDBG_ALLOC_MEM_DF | _CRTDBG_LEAK_CHECK_DF);
	//_CrtSetBreakAlloc(149);

	const size_t sizeHeap = 32 * 1000;
	void * pHeapMemory = _aligned_malloc(sizeHeap, 4);
	 
	FixedSizeAllocator32::Create(pHeapMemory, sizeHeap);


	ZombieController **zc;
	MonsterController **mc;

	int numberOfMonsters;
	int numberOfZombies;
	int k = 0;
	int l = 0, m = 0;
	char * pName = reinterpret_cast<char*>(calloc(1, sizeof(char)));
	cout << "Play MonsterChase:" << endl;
	cout << "Player name (Put fullstop '.' at end of each word):";
	char tempName;
	int length;
	while (true)
	{
		//scanf_s("%c", &tempName);
		tempName = getchar();
		if (tempName == '.')
		{
			break;

		}
		length = strlen(pName);
		pName = reinterpret_cast<char*>(realloc(pName, length + 2));
		*(pName + length) = tempName;
		*(pName + length + 1) = '\0';
		length = 0;
	}
	
	
	char* b = new char;


	PlayerController *plc = new PlayerController(*pName);

	plc->initialize(1);
	plc->pdisplay();
	cout << "Enter number of monsters between 1-25:" << endl;
	cin >> numberOfMonsters;
	MessagedAssert(((numberOfMonsters <= 25) && (numberOfMonsters >= 1)), "Keep the number between 1 and 25.");
	mc = new MonsterController*[numberOfMonsters];
	
	/*Actor* array;
	array = new Actor[numberOfMonsters];*/

	char ** mName= new char*[numberOfMonsters]; 
	cout << "Enter a one word name of each monster(less than 10 letters and put fullstop '.' at end of each word):" << endl;
	for (int i = 0; i < numberOfMonsters; i++)
	{
		cout << "Enter a monster name:" << endl;
		mName[i] = reinterpret_cast<char*>(calloc(1, sizeof(char)));
		while (true)
		{
			//scanf_s("%c", &tempName);
			 tempName= getchar();
			if (tempName == '.')
			{
				break;

			}
			length = strlen(mName[i]);
			mName[i] = reinterpret_cast<char*>(realloc(mName[i], length + 2));
			*(mName[i] + length) = tempName;
			*(mName[i] + length + 1) = '\0';
			length = 0;
		}
		mc[i] = new MonsterController(*mName[i]);
		mc[i]->initialize(i);		
	}
	


	cout << "Enter number of Zombies less than "<< 25 - numberOfMonsters<<": "<< endl;
	cin >> numberOfZombies;
	MessagedAssert(((numberOfZombies <= 25) && (numberOfZombies >= 1)), "Zombies are too many in number.");
	char ** zName = new char*[numberOfZombies];
	cout << "Enter a one word name of each zombie(less than 10 letters and put fullstop '.' at end of each word):" << endl;
	zc = new ZombieController*[numberOfZombies];
	
	/*Actor* array1;
	array1 = new Actor[numberOfZombies];*/
	for (int i = 0; i < numberOfZombies; i++)
	{
		cout << "Enter a zombie name:" << endl;
		zName[i] = reinterpret_cast<char*>(calloc(1, sizeof(char)));
		while (true)
		{
			//scanf_s("%c", &tempName);
			tempName = getchar();
			if (tempName == '.')
			{
				break;

			}
			length = strlen(zName[i]);
			zName[i] = reinterpret_cast<char*>(realloc(zName[i], length + 2));
			*(zName[i] + length) = tempName;
			*(zName[i] + length + 1) = '\0';
			length = 0;
		}
		zc[i] = new ZombieController(*zName[i]);
		zc[i]->initialize(i);
	}

	for (int i = 0; i < numberOfMonsters; i++)
	{
		mc[i]->mdisplay(i);
	}



	char choice;
	do{

		cout << "Press W,A,S,D to move the player. Press Q to quit." << endl;
		cin >> choice;
		MessagedAssert((choice == 'w' || choice == 'a' || choice == 's' || choice == 'd' || choice == 'q'), "Please press W or A or S or D or Q only");

		plc->update(choice, 0, 0);

		for (int i = 0; i < numberOfMonsters; i++)
		{

			for (int j = i + 1; j < numberOfMonsters; j++)
			{
				if (mc[i]->act->GetPosition() == plc->act->GetPosition())
				{

					printf("Monster %s died.\n", mc[i]->act->name);
					mc[i]->act->isAlive = false;

				}
				if (mc[i]->act->GetPosition() == mc[j]->act->GetPosition())
				{

					mc[j]->act->isAlive = false;
					zc[k]->act->isAlive = true;
					printf("Monsters %s died. \n", mc[j]->act->name);
					printf("Monsters %s turned into zombie %s. \n", mc[j]->act->name, zc[k]->act->name);
					zc[i]->zdisplay(i);
					k++;

				}
				if (zc[i]->act->GetPosition() == plc->act->GetPosition())
				{
					printf("Zombie %s died.\n", zc[i]->act->name);
					zc[i]->act->isAlive = false;

				}
			}
		}

		for (int i = 0; i < numberOfMonsters; i++)
		{
			mc[i]->update(0, 0, 0);
			mc[i]->mdisplay(i);
		}

		for (int i = 0; i < numberOfZombies; i++)
		{
			l++;
			m++;
			zc[i]->update(0, l, m);
			zc[i]->zdisplay(i);
		}



	} while (choice != 'q');


	cout << "Game ends." << endl;

	for (unsigned int i = 0; i < numberOfMonsters; i++)
	{
		delete mc[i];
	}
	for (unsigned int i = 0; i < numberOfZombies; i++)
	{
		delete zc[i];
	}

	
	delete zc;
	delete mc;
	delete plc;
	delete b;
	free(pName);
	for (size_t i = 0; i < numberOfMonsters; i++)
	{
		free(mName[i]);
	}
	free(mName);
	for (size_t i = 0; i < numberOfZombies; i++)
	{
		free(zName[i]);
	}
	free(zName);
	
	_aligned_free(pHeapMemory);
	FixedSizeAllocator32::clearBitField();
	

#if defined(_DEBUG)
	_CrtDumpMemoryLeaks();
#endif // _DEBUG
}
	

