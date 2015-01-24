using UnityEngine;
using System.Collections;

public class InputTypeControl : MonoBehaviour
{
    public ButtonBehavior m_button;
    public UISprite m_icon;

    public event System.Action<InputType> Clicked;

    private InputType m_inputType;

    public InputType InputType
    {
        get { return m_inputType; }
    }

    void Awake()
    {
        m_button.Clicked += HandleButtonClicked;
    }

    public void Initialize(InputType inputType)
    {
        m_inputType = inputType;

        SetupIcon();
    }

    private void HandleButtonClicked(object sender)
    {
        if (Clicked != null)
            Clicked(m_inputType);
    }

    private void SetupIcon()
    {
        m_icon.spriteName = InputTypeHelpers.GetIconByType(m_inputType);
        m_button.gameObject.GetComponent<UIButton>().pressedSprite = m_icon.spriteName;
        m_button.gameObject.GetComponent<UIButton>().hoverSprite = m_icon.spriteName;
    }
}
