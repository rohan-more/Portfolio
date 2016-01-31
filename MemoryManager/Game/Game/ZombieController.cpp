
#include "ZombieController.h"
#include"FixedSizeAllocator32.h"
#include "ConsolePrint.h"




using namespace std;

bool ZombieController::isSeeded = true;


void ZombieController::initialize(int num)
{

	
	float x = rand() % 9;
	float y = rand() % 9;
	act->SetPosition(Vector3(x, y, 0));
	DEBUG_PRINT("Creating a zombie named %c at position %d, %d at line 19 in ZombieController.\n", act->name, x, y);
}

void ZombieController::update(char b, int zx, int zy)
{
	if (act->isAlive == true)
	{
		act->SetPosition(act->GetPosition() + Vector3(zx,zy,0));
		//act->SetPosition(Vector3(newX, newY, 0));
		DEBUG_PRINT("Zombie %c is now at position %d, %d  at line 28 in ZombieController.\n", act->name, zx, zy);
	}
}

void ZombieController::display(int num)
{
	if (act->isAlive == true)
	{
		cout << "Zombie "<< act->name << "is at: ";
		act->GetPosition().display();
	}

}


void ZombieController::zdisplay(int num)
{
	if (act->isAlive == true)
	{
		cout << "Zombie "<< act->name << " is at: ";
		act->GetPosition().display();
	}
	
}

void ZombieController::setActor(Actor& acto)
{
	act = &acto;
}

Actor* ZombieController::getActor()
{
	return act;
}

ZombieController::ZombieController()
{
	if (isSeeded)
	{
		srand(time(NULL));
		isSeeded = false;
		DEBUG_PRINT("Time randomised.\n");
	}
}

ZombieController::~ZombieController()
{

}
ZombieController::ZombieController(char & n)
{
	act = new Actor(n);
}
void* ZombieController::operator new (size_t size)
{
	DEBUG_PRINT("ZombieController's new has been called.\n");
	return Cohort6::FixedSizeAllocator32::myAlloc();
}
void ZombieController::operator delete (void *p)
{
	DEBUG_PRINT("ZombieController's delete has been called.\n");
	if (Cohort6::FixedSizeAllocator32::myFree(p))
	{
		std::cout << "Zombie deleted\n";
	}
	else
	{
		std::cout << "Invalid try to delete zombie\n";
	}
}






