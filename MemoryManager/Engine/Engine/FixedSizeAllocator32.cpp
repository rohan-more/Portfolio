#include"FixedSizeAllocator32.h"

#include<cstdlib>

namespace Cohort6
{
	char *  FixedSizeAllocator32::bitField = NULL;
	const size_t FixedSizeAllocator32::blockSize = 32;
	size_t FixedSizeAllocator32::numberOfBlocks = 0;
	char * FixedSizeAllocator32::head = NULL;
	std::ptrdiff_t FixedSizeAllocator32::maxDistance = 0;
	void FixedSizeAllocator32::Create(void * heapMemory, size_t sizeHeap)
	{
		
		numberOfBlocks = sizeHeap / blockSize;
		bitField = reinterpret_cast<char *>(calloc((size_t)(sizeHeap / (blockSize * 8)), sizeof(char)));
		head = reinterpret_cast<char *>(heapMemory);
	    maxDistance = reinterpret_cast<char *>(reinterpret_cast<char *>(heapMemory)+sizeHeap) - head;
		
	}
	void * FixedSizeAllocator32::myAlloc()
	{
		size_t counter = 0;
		if (numberOfBlocks)
		{
			while (*(bitField + counter) == 1)
			{
				counter++;
			}
			*(bitField + counter) = 1;
			numberOfBlocks--;
			return reinterpret_cast<void *>((head + 32 * counter));
		}
		else
		{
			return NULL;
		}
	}
	bool FixedSizeAllocator32::myFree(void * pPtr)
	{
		std::ptrdiff_t distance = reinterpret_cast<char *>(pPtr)-head;
		if (distance >= 0 && distance <= maxDistance)
		{
			size_t temp = distance / blockSize;
			if (*(bitField + temp))
			{
				*(bitField + temp) = 0;
				numberOfBlocks++;
				return true;
			}
		}
		return false;
	}
	void  FixedSizeAllocator32::clearBitField()
	{
		free(bitField);
	}
}