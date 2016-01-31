
#include <iostream>
#include "ActorController.h"
#include "PlayerController.h"
#include"FixedSizeAllocator32.h"
#include "ConsolePrint.h"


using namespace std;




void PlayerController::initialize(int a)
	{
		DEBUG_PRINT("Player %c has been initiated.\n", act->name);
		act->SetPosition(Vector3(5.0f, 5.0f, 0.0f));
		
	}

void PlayerController::update(char option, int x, int y)
	{
		if (act->isAlive == true)
		{
			if (option == 'w')
			{
				act->SetPosition(act->GetPosition() + Vector3(0.0f, 1.0f, 0.0f));
			}

			if (option == 's')
			{
				act->SetPosition(act->GetPosition() + Vector3(0.0f, -1.0f, 0.0f));
			}

			if (option == 'a')
			{
				act->SetPosition(act->GetPosition() + Vector3(-1.0f, 0.0f, 0.0f));
			}

			if (option == 'd')
			{
				act->SetPosition(act->GetPosition() + Vector3(1.0f, 0.0f, 0.0f));
			}
			
		}

	}

	void PlayerController::display(int a)
	{

		cout<<act->name<<" is at: ";
		act->GetPosition().display();


	}

	void PlayerController:: pdisplay()
	{
		cout << act->name << " is at: ";
		act->GetPosition().display();
		
	}

	void PlayerController::setActor(Actor& acto)
	{
		act = &acto;
	}

	Actor* PlayerController::getActor()
	{
		return act;
	}

	PlayerController::PlayerController(char & n)
	{
		act = new Actor(n);
		
	}
	PlayerController::PlayerController()
	{
		
	}

	PlayerController::~PlayerController()
	{
		
	}
	void* PlayerController::operator new (size_t size)
	{
		DEBUG_PRINT("PlayerController's new has been called.\n");
		return Cohort6::FixedSizeAllocator32::myAlloc();
	}
		void PlayerController::operator delete (void *p)
	{
		DEBUG_PRINT("PlayerController's delete has been called.\n");
		if (Cohort6::FixedSizeAllocator32::myFree(p))
		{
			std::cout << "Player deleted\n";
		}
		else
		{
			std::cout << "Invalid try to delete player\n";
		}
	}


