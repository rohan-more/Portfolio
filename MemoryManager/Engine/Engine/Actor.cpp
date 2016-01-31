#include "Vector3.h"
#include "Actor.h"
#include"FixedSizeAllocator32.h"
#include <iostream>

using namespace Cohort6;

	void Actor::SetPosition(Vector3 Position)
	{
		position = Position;
	}
	Vector3 Actor::GetPosition(void)
	{
		return position;
	}
	Actor::Actor(char & n)
	{
		name = &n;
	}
	Actor::Actor()
	{

	}
	void*  Actor::operator new (size_t size)
	{
		return FixedSizeAllocator32::myAlloc();
	}
		void Actor::operator delete (void *p)
	{
		FixedSizeAllocator32::myFree(p);
	}


