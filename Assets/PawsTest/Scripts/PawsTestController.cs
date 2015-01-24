using UnityEngine;
using System.Collections;

public class PawsTestController : MonoBehaviour
{
    public PawTest[] m_paws;

    void Awake()
    {
        LizardInput.Instance.PawGriped += HandlePawGriped;
        LizardInput.Instance.PawReleased += HandlePawReleased;
        LizardInput.Instance.PawDirectionChanged += HandlePawDirectionChanged;
    }

    private void HandlePawGriped(PawType pawType)
    {
        m_paws[(int)pawType].SetGrip(true);
    }

    private void HandlePawReleased(PawType pawType)
    {
        m_paws[(int)pawType].SetGrip(false);
    }

    private void HandlePawDirectionChanged(PawType pawType, Vector3 direction)
    {
        m_paws[(int)pawType].SetDirection(direction);
    }
}
