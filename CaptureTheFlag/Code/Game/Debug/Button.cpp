#include "Button.h"
#include "Text.h"
#include "../../Game/Gameplay/GameObject2D.h"
#include "../../Engine/Graphics/cSprite.h"
#include "../../Engine/UserInput/UserInput.h"
#include "../../Engine/Graphics/Font.h"
#include "../../Engine/Asserts/Asserts.h"
#include "../../Engine/Logging/Logging.h"

namespace
{
	const std::string background = "data/gameobjects2d/ui/sliderbar.bing2dobj";
}

eae6320::Debug::UI::Button::Button(const PixelCoordinates i_pixelCoordinates, const std::string i_buttonName, const Color i_color, const float i_backgroundTransperancy, void(*i_callback)(), const bool i_isSelected) :
	IUIController(i_color, i_isSelected),
	m_buttonName(new Text(i_pixelCoordinates, i_buttonName, i_color)),
	m_callback(i_callback),
	m_backgroundTransperancy(i_backgroundTransperancy),
	m_pixelCoordinates(i_pixelCoordinates)
	//,m_pressed(false)
{
	Button::Initialize();
}

eae6320::Debug::UI::Button::~Button()
{
	Button::CleanUp();
}

void eae6320::Debug::UI::Button::Initialize()
{
	const bool doNotUsePixelCoordinatesFromFile = true;
	if (Graphics::Font::widthArray && Graphics::Font::texcoordArray)
	{
		m_background = Gameplay::GameObject2D::LoadGameObject2D(background.c_str(), doNotUsePixelCoordinatesFromFile, m_pixelCoordinates.x, m_pixelCoordinates.y);
		RecalculateBackgroundWidth();
	}
	else
	{
		EAE6320_ASSERT(false);
		Logging::OutputError("Font not initialized so sliders cannot be initialized");
	}
}

void eae6320::Debug::UI::Button::RecalculateBackgroundWidth() const
{
	Gameplay::RectTransform rectTransform = m_background->GetRectTransform();
	const Gameplay::Anchor anchor = m_background->GetAnchor();
	rectTransform.width = m_buttonName->GetRightTextPixelCoordinate() - m_pixelCoordinates.x;
	const Graphics::Sprite::ScreenPosition screenPosition = ConvertPixelCoordinatesUsingAnchorToScreenCoordinates(rectTransform, anchor);
	m_background->GetSprite()->SetScreenPosition(screenPosition);
}

void eae6320::Debug::UI::Button::CleanUp()
{
	if (m_buttonName)
	{
		delete m_buttonName;
		m_buttonName = nullptr;
	}
	if (m_background)
	{
		delete m_background;
		m_background = nullptr;
	}
}

void eae6320::Debug::UI::Button::Draw(const Graphics::Material * const i_material, const float alpha, const bool invertColor) const
{
	const bool invertBackgroundColor = true;
	IUIController::Draw(m_background->GetMaterial(), m_backgroundTransperancy, invertBackgroundColor);
	m_background->GetSprite()->Draw();
	IUIController* text = reinterpret_cast<IUIController*>(m_buttonName);
	text->isSelected = isSelected;
	text->Draw();
}

void eae6320::Debug::UI::Button::Update()
{
	if (isSelected && UserInput::GetKeyUp(VK_RETURN))
	{
		if (m_callback)m_callback();
		//m_pressed = true;
	}
}

/*void eae6320::Debug::UI::Button::SetState(const bool i_state)
{
	m_pressed = i_state;
}
bool eae6320::Debug::UI::Button::GetState()const
{
	return m_pressed;
}*/
