#ifndef ActorController_h
#define ActorController_h
#include "Actor.h"
#include "Vector3.h"

class ActorController 
{
public:
	virtual void initialize(int)=0;
	virtual void update(char,int,int)=0;
	virtual void display(int)=0;
	virtual void setActor(Actor&) = 0;
	virtual Actor* getActor() = 0;
	
    //bool isAlive;
	Actor* act;
	Vector3 vect;
private:
	

	
};
#endif