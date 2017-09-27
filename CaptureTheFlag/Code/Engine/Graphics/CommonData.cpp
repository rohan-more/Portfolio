#include "CommonData.h"

#ifndef  NULL
#define NULL 0
#endif // ! NULL

namespace eae6320
{
	namespace Graphics
	{
		CommonData* CommonData::commonData = nullptr;

		inline CommonData::CommonData() 
#if defined( EAE6320_PLATFORM_D3D )
			:
			s_direct3dDevice(NULL),
			s_direct3dImmediateContext(NULL)
#endif	
		{}

		inline CommonData::~CommonData() {}

		CommonData * CommonData::GetCommonData()
		{
			if (!commonData)
			{
				commonData = new CommonData();
			}
			return commonData;
		}
		bool CommonData::Initialize()
		{
			if (!commonData)
			{
				commonData = new CommonData();
			}
			if (commonData)
			{
				return true;
			}
			return false;
		}
		bool CommonData::CleanUp()
		{
			if (commonData)
			{
				delete commonData;
				return true;
			}
			return false;
		}
	}
}


