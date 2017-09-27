#ifndef EAE6320_FPSCAMERACONTROLLER_H
#define EAE6320_FPSCAMERACONTROLLER_H

#include "../../Game/Gameplay/cbController.h"
#include <cstdint>
#include "../Math/cVector.h"

namespace eae6320
{
	namespace Camera
	{
		class FPSCameraController final : Gameplay::cbController
		{
		public:
			static const uint32_t classUUID;
			static FPSCameraController* Initialize(const float i_acceleration = 0.0f)
			{
				return new FPSCameraController(i_acceleration);
			}
			void SetPlayerTransform(Gameplay::Transform* i_playerTransform)
			{
				m_playerTransform = i_playerTransform;
			}
		private:
			explicit FPSCameraController(const float i_acceleration)
				:
				m_playerTransform(nullptr),
				m_acceleration(i_acceleration)
			{}
			void UpdatePosition(Gameplay::Transform& io_transform) override;
			void UpdateOrientation(Gameplay::Transform& io_transform) override;
			Gameplay::Transform* m_playerTransform;
			Math::cVector m_velocity;
			const float m_acceleration;
		};
	}
}
#endif // !EAE6320_FPSCAMERACONTROLLER_H
