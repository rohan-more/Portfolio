#ifndef FixedSizeAllocator32_h
#define FixedSizeAllocator32_h

#include<cstddef>

namespace Cohort6
{
	class FixedSizeAllocator32
	{
	public:
		static void Create(void * pHeapMemory, size_t sizeHeap);
		static void * myAlloc();
		static bool myFree(void * pPtr);
		static void clearBitField();

	private:
		static char * bitField;
		const static size_t blockSize;
		static size_t numberOfBlocks;
		static char * head;
		static std::ptrdiff_t maxDistance;
	};
}

#endif // !FixedSizeAllocator32_h
