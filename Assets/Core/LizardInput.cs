using UnityEngine;
using System.Collections;

public class LizardInput : MonoBehaviour
{
    private static LizardInput m_instance;

    public event System.Action<LimbId> PawGriped;
    public event System.Action<LimbId> PawReleased;
    public event System.Action<LimbId, Vector3> PawDirectionChanged;


    private InputType[] m_pawsBinding;
    private PawState[] m_pawsPrevStates;

    public static LizardInput Instance
    {
        get { return m_instance; }
    }

    void Awake()
    {
        if (m_instance == null)
        {
            m_instance = this;

            DontDestroyOnLoad(this);

            m_pawsBinding = new InputType[4];

            m_pawsPrevStates = new PawState[4];
            for (int i = 0; i < 4; i++)
            {
                m_pawsPrevStates[i] = new PawState();
                m_pawsPrevStates[i].Direction = Vector3.up;
            }

            SetPawBinding(LimbId.ArmLeft, InputType.Pad1Left);
            SetPawBinding(LimbId.ArmRight, InputType.Pad1Right);
			SetPawBinding(LimbId.LegLeft, InputType.Pad2Left);
			SetPawBinding(LimbId.LegRight, InputType.Pad2Right);
        }
    }

    void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            LimbId pawType = (LimbId)i;

            UpdateDirection(pawType);

            bool gripState = GetGripState(m_pawsBinding[i]);

            if (m_pawsPrevStates[i].IsGripped != gripState)
            {
                m_pawsPrevStates[i].IsGripped = gripState;

                if (gripState && PawGriped != null)
                    PawReleased(pawType);
                else if (!gripState && PawReleased != null)
                    PawGriped(pawType);
            }
        }
    }

    public void SetPawBinding(LimbId pawType, InputType inputType)
    {
        m_pawsBinding[(int)pawType] = inputType;
    }

    private bool GetGripState(InputType inputType)
    {
        switch (inputType)
        {
            case InputType.Pad1Left: return Input.GetAxis("Pad1LeftGrab") < 0.5f;
            case InputType.Pad1Right: return Input.GetAxis("Pad1RightGrab") < 0.5f;
            case InputType.Pad2Left: return Input.GetAxis("Pad2LeftGrab") < 0.5f;
            case InputType.Pad2Right: return Input.GetAxis("Pad2RightGrab") < 0.5f;
            case InputType.Wasd: return !Input.GetKey(KeyCode.LeftControl);
            case InputType.Ijkl: return !Input.GetKey(KeyCode.RightAlt);
            case InputType.Arrows: return !Input.GetKey(KeyCode.RightControl);
            case InputType.Numpad5123: return !Input.GetKey(KeyCode.Keypad0);
        }

        return false;
    }

    private void UpdateDirection(LimbId pawType)
    {
        float speed = 40.0f;

        InputType inputType = m_pawsBinding[(int)pawType];

        if (inputType == InputType.Pad1Left ||
            inputType == InputType.Pad1Right ||
            inputType == InputType.Pad2Left ||
            inputType == InputType.Pad2Right)
        {
            float hori = 0.0f;
            float vert = 0.0f;

            switch (inputType)
            {
                case InputType.Pad1Left:
                    hori = Input.GetAxis("Pad1LeftStickHori");
                    vert = Input.GetAxis("Pad1LeftStickVert");
                    break;

                case InputType.Pad1Right:
                    hori = Input.GetAxis("Pad1RightStickHori");
                    vert = Input.GetAxis("Pad1RightStickVert");
                    break;

                case InputType.Pad2Left:
                    hori = Input.GetAxis("Pad2LeftStickHori");
                    vert = Input.GetAxis("Pad2LeftStickVert");
                    break;

                case InputType.Pad2Right:
                    hori = Input.GetAxis("Pad2RightStickHori");
                    vert = Input.GetAxis("Pad2RightStickVert");
                    break;
            }

            if (hori != m_pawsPrevStates[(int)pawType].Direction.x ||
                vert != m_pawsPrevStates[(int)pawType].Direction.y)
            {
                Vector3 direction = m_pawsPrevStates[(int)pawType].Direction;
                direction.x = hori;
                direction.y = vert;
                m_pawsPrevStates[(int)pawType].Direction = direction;

                if (PawDirectionChanged != null)
                    PawDirectionChanged(pawType, m_pawsPrevStates[(int)pawType].Direction);
            }

            return;
        }

        if (m_pawsPrevStates[(int)pawType].Direction == Vector3.zero)
            m_pawsPrevStates[(int)pawType].Direction = Vector3.up;

        bool isLeft = false;
        bool isRight = false;

        switch (inputType)
        {
            case InputType.Wasd:
                isLeft = Input.GetKey(KeyCode.A);
                isRight = Input.GetKey(KeyCode.D);
                break;

            case InputType.Ijkl:
                isLeft = Input.GetKey(KeyCode.J);
                isRight = Input.GetKey(KeyCode.L);
                break;

            case InputType.Arrows:
                isLeft = Input.GetKey(KeyCode.LeftArrow);
                isRight = Input.GetKey(KeyCode.RightArrow);
                break;

            case InputType.Numpad5123:
                isLeft = Input.GetKey(KeyCode.Keypad1);
                isRight = Input.GetKey(KeyCode.Keypad3);
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
