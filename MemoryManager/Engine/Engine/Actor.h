#ifndef Actor_h
#define Actor_h
#include "Vector3.h"

using namespace Cohort6;
class Actor
{
	Vector3 position;

public:
	const char * name;
	Vector3 GetPosition(void);
	void SetPosition(Vector3);
	bool isAlive;
	Actor(char &);
	Actor();

	static void* operator new (size_t size);
	static void operator delete (void *p);

};

#endif