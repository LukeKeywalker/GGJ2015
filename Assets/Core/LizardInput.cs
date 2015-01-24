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
        {
            m_pawsPrevStates[i] = new PawState();
            m_pawsPrevStates[i].Direction = Vector3.up;
        }

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

            UpdateDirection(pawType);

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
            case InputType.Wasd: return !Input.GetKey(KeyCode.LeftControl);
            case InputType.Arrows: return !Input.GetKey(KeyCode.RightControl);
        }

        return false;
    }

    /*
    private Vector3 GetGripDirection(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.Wasd: return !Input.GetKey(KeyCode.LeftControl);
            case InputType.Arrows: return !Input.GetKey(KeyCode.RightControl);
        }

        return Vector3.zero;
    }
     * */

    private void UpdateDirection(PawType pawType)
    {
        float speed = 40.0f;

        InputType inputType = m_pawsBinding[(int)pawType];

        bool isLeft = false;
        bool isRight = false;

        switch (inputType)
        {
            case InputType.Wasd:
                isLeft = Input.GetKey(KeyCode.A);
                isRight = Input.GetKey(KeyCode.D);
                break;

            case InputType.Arrows:
                isLeft = Input.GetKey(KeyCode.LeftArrow);
                isRight = Input.GetKey(KeyCode.RightArrow);
                break;
        }

        if (isLeft)
            m_pawsPrevStates[(int)pawType].Direction = Quaternion.AngleAxis(Mathf.PI * Time.deltaTime * speed, Vector3.forward) * m_pawsPrevStates[(int)pawType].Direction;

        if (isRight)
            m_pawsPrevStates[(int)pawType].Direction = Quaternion.AngleAxis(Mathf.PI * Time.deltaTime * speed, Vector3.back) * m_pawsPrevStates[(int)pawType].Direction;

        if ((isLeft || isRight) && PawDirectionChanged != null)
            PawDirectionChanged(pawType, m_pawsPrevStates[(int)pawType].Direction);
    }
}
