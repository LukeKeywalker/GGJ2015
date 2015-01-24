using UnityEngine;
using System.Collections;

public class PawsTestController : MonoBehaviour
{
    public PawTest[] m_paws;

    void Awake()
    {
        LizardInput.Instance.PawGriped += HandlePawGriped;
        LizardInput.Instance.PawReleased += HandlePawReleased;
    }

    private void HandlePawGriped(PawType pawType)
    {
        m_paws[(int)pawType].SetGrip(true);
    }

    private void HandlePawReleased(PawType pawType)
    {
        m_paws[(int)pawType].SetGrip(false);
    }
}
