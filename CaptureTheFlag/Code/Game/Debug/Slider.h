#ifndef EAE6320_SLIDER_H
#define EAE6320_SLIDER_H

#include "Color.h"
#include "PixelCoordinates.h"
#include "IUIController.h"
#include <string>

namespace eae6320
{
	namespace Debug
	{
		namespace UI
		{
			class Text;
		}
	}
}

namespace eae6320
{
	namespace Gameplay
	{
		class GameObject2D;
	}
}

namespace eae6320
{
	namespace Debug
	{
		namespace UI
		{
			class Slider final :public IUIController
			{
			public:
				explicit Slider(const PixelCoordinates i_pixelCoordinates = { 0,0 }, const std::string i_sliderName = "", const Color i_color = { 1.0f,1.0f,1.0f }, const float i_minValue = 0.0f, const float i_maxValue = 1.0f, const float i_sliderSize = 200.0f, void const* i_callbackThisPointer = nullptr, float(*i_callback)(void const* i_callbackthisPointer,  const float i_currentValue, const float i_minvalue, const float i_maxvalue) = nullptr, const bool i_isSelected = false);
				~Slider();
				float GetValue()const;
				void Reset(float i_resetValue);
			private:
				void RecalculateSliderbarWidth() const;
				void Update() override;
				void Draw(const Graphics::Material* const i_material = nullptr, const float alpha = 1.0f, const bool invertColor = false)const override;
				void Initialize() override;
				void CleanUp()override;
				float(*m_callback)(void const* i_callbackthisPointer, const float i_currentValue, const float i_minValue, const float i_maxValue);
				void const* m_callbackThisPointer;
				Gameplay::GameObject2D* m_slider;
				Text* m_sliderName;
				float m_value, m_minValue, m_maxValue, m_sliderSize;
				PixelCoordinates m_pixelCoordinates;
			};
		}
	}
}

#endif // !EAE6320_SLIDER_H
