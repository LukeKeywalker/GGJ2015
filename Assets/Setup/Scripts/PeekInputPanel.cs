using UnityEngine;
using System.Collections;

public class PeekInputPanel : MonoBehaviour
{
    public InputTypeControl m_inputTypeControl;
    public UIGrid m_grid;

    public event System.Action<InputType> InputPeeked;

    void Awake()
    {
        CreateInputTypeControls();
    }

    private void CreateInputTypeControls()
    {
        CreateInputTypeControl(InputType.Pad1Left);
        CreateInputTypeControl(InputType.Pad1Right);
        CreateInputTypeControl(InputType.Pad2Left);
        CreateInputTypeControl(InputType.Pad2Right);
        CreateInputTypeControl(InputType.Wasd);
        CreateInputTypeControl(InputType.Ijkl);
        CreateInputTypeControl(InputType.Arrows);
        CreateInputTypeControl(InputType.Numpad5123);

        m_grid.Reposition();
    }

    private void CreateInputTypeControl(InputType inputType)
    {
        InputTypeControl control = (InputTypeControl)Instantiate(m_inputTypeControl);
        control.transform.parent = m_grid.transform;
        control.transform.localScale = Vector3.one;
        control.transform.localPosition = Vector3.zero;
        control.transform.localRotation = Quaternion.identity;

        control.Initialize(inputType);
        control.Clicked += InputTypeControlClicked;
    }

    private void InputTypeControlClicked(InputType inputType)
    {
        if (InputPeeked != null)
            InputPeeked(inputType);
    }
}
