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

    private void HandlePawGriped(LimbId pawType)
    {
        m_paws[(int)pawType].SetGrip(true);
    }

    private void HandlePawReleased(LimbId pawType)
    {
        m_paws[(int)pawType].SetGrip(false);
    }

    private void HandlePawDirectionChanged(LimbId pawType, Vector3 direction)
    {
        m_paws[(int)pawType].SetDirection(direction);
    }
}
