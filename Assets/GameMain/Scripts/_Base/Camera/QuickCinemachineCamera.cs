using UnityEngine;
using UnityGameKit.Runtime;
using Cinemachine;

public class QuickCinemachineCamera : MonoSingletonBase<QuickCinemachineCamera>
{
    public CinemachineVirtualCamera m_VirtualCamera;
    public bool SetFollowTarget(Transform transform)
    {
        if(m_VirtualCamera ==null)
        {
            Log.Fail("CinemachineVirtualCamera reference is null.");
            return false;
        }
        m_VirtualCamera.Follow = transform;
        return true;
    }
}