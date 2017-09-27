#ifndef EAE6320_CBCONTROLLER_H
#define EAE6320_CBCONTROLLER_H
#include <cstdint>
namespace eae6320
{
	namespace Gameplay
	{
		struct Transform;
	}
}

namespace eae6320
{
	namespace Gameplay
	{
		class cbController
		{
		public:
			uint32_t controller_UUID;
			virtual void UpdatePosition(Transform& io_transform) = 0;
			virtual void UpdateOrientation(Transform& io_transform) = 0;
			virtual ~cbController() = default;
			cbController(uint32_t i_controller_UUID) : controller_UUID(i_controller_UUID) {}
			cbController() {}
		};
	}
}

#endif // !EAE6320_CBCONTROLLER_H