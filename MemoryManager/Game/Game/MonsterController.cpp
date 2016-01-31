
#include "MonsterController.h"
#include "ConsolePrint.h"
#include"FixedSizeAllocator32.h"



using namespace std;

bool MonsterController::isSeeded = true;



void MonsterController::initialize(int num)
	{
	
		float x = rand() % 9;
		float y = rand() % 9;
		act->SetPosition(Vector3(x, y, 0));
		act->isAlive = true;
		DEBUG_PRINT("Creating a monster named %c at position %d, %d at line 19 in MonsterController.\n", act->name, x,y);
	}

void MonsterController::update(char b, int x, int y)
	{
		if (act->isAlive == true)
		{
			float newX = rand() % 9;
			float newY = rand() % 9;
			act->SetPosition(Vector3(newX, newY, 0));
			DEBUG_PRINT("Monster %c is now at position %d, %d  at line 28 in MonsterController.\n", act->name, newX, newY);
		}
	}

void MonsterController::display(int num)
	{
		if (act->isAlive == true)
		{
			cout << "Monster "<< act->name << "is at: ";
			act->GetPosition().display();
		}
		
		
	}


void MonsterController::mdisplay(int num)
	{
		if (act->isAlive==true)
		{
			cout << "Monster "<< act->name << " is at: ";
			act->GetPosition().display();
		}
	
	}

void MonsterController::setActor(Actor& acto)
{
	act = &acto;
}

Actor* MonsterController::getActor()
{
	return act;
}

MonsterController::MonsterController()
{
	if (isSeeded)
	{
		srand(time(NULL));
		isSeeded = false;
		DEBUG_PRINT("Time randomised.\n");
	}
}

MonsterController::MonsterController(char &n)
{
	act = new Actor(n);
}


MonsterController::~MonsterController()
{

}
void* MonsterController::operator new (size_t size)
{
	DEBUG_PRINT("MonsterController's new has been called.\n");
	return Cohort6::FixedSizeAllocator32::myAlloc();
}
void MonsterController::operator delete (void *p)
{
	DEBUG_PRINT("MonsterController's delete has been called.\n");
	if (Cohort6::FixedSizeAllocator32::myFree(p))
	{
		cout << "Monster deleted\n";
	}
	else
	{
		cout << "Invalid try to delete monster\n";
	}
}


	
	
		



