#ifndef MonsterController_h
#define MonsterController_h
#include "ActorController.h"


#include <iostream>
#include <time.h>
using namespace Cohort6;

class MonsterController : public ActorController
{

public:
	void mdisplay(int);
	virtual void initialize(int);
	virtual void update(char , int, int);
	virtual void display(int);
	virtual void setActor(Actor&);
	virtual Actor* getActor();
     MonsterController();
	 MonsterController(char&);
	 ~MonsterController();
	 static void* operator new (size_t size);
	 static void operator delete (void *p);
	 

private:
	static bool isSeeded;
	bool isAlive;
	int AsciiVal=0;

};

#endif // !MonsterController_h
