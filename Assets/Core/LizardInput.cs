using UnityEngine;
using System.Collections;

public class LizardInput : MonoBehaviour
{
    private static LizardInput m_instance;

    public event System.Action<PawType> PawGriped;
    public event System.Action<PawType> PawReleased;
    public event System.Action<PawType, Vector3> PawDirectionChanged;

    private InputType[] m_pawsBinding;
    private PawState[] m_pawsPrevStates;

	public static LizardInput Instance
    {
        get { return m_instance; }
    }

    void Awake()
    {
        m_pawsBinding = new InputType[4];

        m_pawsPrevStates = new PawState[4];
        for (int i = 0; i < 4; i++)
            m_pawsPrevStates[i] = new PawState();

        if (m_instance == null)
        {
            m_instance = this;

            DontDestroyOnLoad(this);
        }
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            PawType pawType = (PawType)i;

            bool gripState = GetGripState(m_pawsBinding[i]);

            if (m_pawsPrevStates[i].IsGripped != gripState)
            {
                m_pawsPrevStates[i].IsGripped = gripState;

                if (gripState && PawGriped != null)
                    PawGriped(pawType);
                else if (!gripState && PawReleased != null)
                    PawReleased(pawType);
            }
        }
    }

    public void SetPawBinding(PawType pawType, InputType inputType)
    {
        m_pawsBinding[(int)pawType] = inputType;
    }

    private bool GetGripState(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.Wasd: return Input.GetKey(KeyCode.LeftControl);
            case InputType.Arrows: return Input.GetKey(KeyCode.RightControl);
        }

        return false;
    }
}
