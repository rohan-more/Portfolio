#ifndef ZombieController_h
#define ZombieController_h
#include "ActorController.h"


#include <iostream>
#include <time.h>
using namespace Cohort6;

class ZombieController : public ActorController
{

public:
	void zdisplay(int);
	virtual void initialize(int);
	virtual void update(char, int, int);
	virtual void display(int);
	virtual void setActor(Actor&);
	virtual Actor* getActor();
	ZombieController();
	ZombieController(char&);
	~ZombieController();
	static void* operator new (size_t size);
	static void operator delete (void *p);
	


private:
	static bool isSeeded;
	bool isAlive;

};

#endif // !ZombieController_h
