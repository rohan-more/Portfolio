#ifndef PlayerController_h
#define PlayerController_h
#include "ActorController.h"

using namespace Cohort6;

class PlayerController : public ActorController
{
public:
	void pdisplay();
	virtual void initialize(int);
	virtual void update(char, int, int);
	virtual void display(int);
	virtual void setActor(Actor&);
	virtual Actor* getActor();
	PlayerController(char &);
	PlayerController();
	~PlayerController();
	static void* operator new (size_t size);
	static void operator delete (void *p);
	
private:
	bool isAlive;
};

#endif // !PlayerController_h