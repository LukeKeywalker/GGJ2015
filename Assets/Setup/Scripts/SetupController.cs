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

    private PawType m_selectedPawType;

    void Awake()
    {
        m_peekInputPanel.gameObject.SetActive(false);

        m_peekInputPanel.InputPeeked += HandleInputPeeked;
        m_startButton.Clicked += HancleStartButtonClicked;

        m_frontLeftInputType.Clicked += FrontLeftInputTypeControlClicked;
        m_frontRightInputType.Clicked += FrontRightInputTypeControlClicked;
        m_backLeftInputType.Clicked += BackLeftInputTypeControlClicked;
        m_backRightInputType.Clicked += BackRightInputTypeControlClicked;

        SetInputType(PawType.FrontLeft, InputType.Wasd);
        SetInputType(PawType.FrontRight, InputType.Arrows);
        SetInputType(PawType.BackLeft, InputType.Pad2Left);
        SetInputType(PawType.BackRight, InputType.Pad2Right);
    }

    private void HancleStartButtonClicked(object obj)
    {
        Application.LoadLevel("PawsTest");
    }

    private void SetInputType(PawType pawType, InputType inputType)
    {
        LizardInput.Instance.SetPawBinding(pawType, inputType);

        switch (pawType)
        {
            case PawType.FrontLeft:
                m_frontLeftInputType.Initialize(inputType);
                break;

            case PawType.FrontRight:
                m_frontRightInputType.Initialize(inputType);
                break;

            case PawType.BackLeft:
                m_backLeftInputType.Initialize(inputType);
                break;

            case PawType.BackRight:
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
        ShowPeekInputPanel(PawType.FrontLeft);
    }

    private void FrontRightInputTypeControlClicked(InputType inputType)
    {
        ShowPeekInputPanel(PawType.FrontRight);
    }

    private void BackLeftInputTypeControlClicked(InputType inputType)
    {
        ShowPeekInputPanel(PawType.BackLeft);
    }

    private void BackRightInputTypeControlClicked(InputType inputType)
    {
        ShowPeekInputPanel(PawType.BackRight);
    }

    private void ShowPeekInputPanel(PawType pawType)
    {
        m_selectedPawType = pawType;

        m_peekInputPanel.gameObject.SetActive(true);
    }
}
