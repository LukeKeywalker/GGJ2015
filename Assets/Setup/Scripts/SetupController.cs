using UnityEngine;
using System.Collections;

public class SetupController : MonoBehaviour
{
    public PeekInputPanel m_peekInputPanel;
    public InputTypeControl m_frontLeftInputType;
    public InputTypeControl m_frontRightInputType;
    public InputTypeControl m_backLeftInputType;
    public InputTypeControl m_backRightInputType;
    public ButtonBehavior m_startButton;

    private LimbId m_selectedPawType;

    void Awake()
    {
        m_peekInputPanel.gameObject.SetActive(false);

        m_peekInputPanel.InputPeeked += HandleInputPeeked;
        m_startButton.Clicked += HancleStartButtonClicked;

        m_frontLeftInputType.Clicked += FrontLeftInputTypeControlClicked;
        m_frontRightInputType.Clicked += FrontRightInputTypeControlClicked;
        m_backLeftInputType.Clicked += BackLeftInputTypeControlClicked;
        m_backRightInputType.Clicked += BackRightInputTypeControlClicked;

        //SetInputType(PawType.FrontLeft, InputType.Wasd);
        //SetInputType(PawType.FrontRight, InputType.Arrows);
        SetInputType(LimbId.ArmLeft, InputType.Pad1Left);
        SetInputType(LimbId.ArmRight, InputType.Pad1Right);
        SetInputType(LimbId.LegLeft, InputType.Pad2Left);
        SetInputType(LimbId.LegRight, InputType.Pad2Right);
    }

    private void HancleStartButtonClicked(object obj)
    {
        Application.LoadLevel("PawsTest");
    }

    private void SetInputType(LimbId pawType, InputType inputType)
    {
        LizardInput.Instance.SetPawBinding(pawType, inputType);

        switch (pawType)
        {
            case LimbId.ArmLeft:
                m_frontLeftInputType.Initialize(inputType);
                break;

            case LimbId.ArmRight:
                m_frontRightInputType.Initialize(inputType);
                break;

            case LimbId.LegLeft:
                m_backLeftInputType.Initialize(inputType);
                break;

            case LimbId.LegRight:
                m_backRightInputType.Initialize(inputType);
                break;
        }
    }

    private void HandleInputPeeked(InputType inputType)
    {
        SetInputType(m_selectedPawType, inputType);

        m_peekInputPanel.gameObject.SetActive(false);
    }

    private void FrontLeftInputTypeControlClicked(InputType inputType)
    {
        ShowPeekInputPanel(LimbId.ArmLeft);
    }

    private void FrontRightInputTypeControlClicked(InputType inputType)
    {
        ShowPeekInputPanel(LimbId.ArmRight);
    }

    private void BackLeftInputTypeControlClicked(InputType inputType)
    {
        ShowPeekInputPanel(LimbId.LegLeft);
    }

    private void BackRightInputTypeControlClicked(InputType inputType)
    {
        ShowPeekInputPanel(LimbId.LegRight);
    }

    private void ShowPeekInputPanel(LimbId pawType)
    {
        m_selectedPawType = pawType;

        m_peekInputPanel.gameObject.SetActive(true);
    }
}
