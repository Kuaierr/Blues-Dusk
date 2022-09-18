using System.Collections;
using UnityEngine;
using UnityGameKit.Runtime;
using Cinemachine;

public class QuickCinemachineCamera : MonoSingletonBase<QuickCinemachineCamera>
{
    public CinemachineVirtualCamera m_VirtualCamera;
    public Vector3 DefaultFollowPositionOffset;
    public Vector3 DefaultRotation;
    private bool SetFollowTarget(Transform transform)
    {
        if (m_VirtualCamera == null)
        {
            Log.Fail("CinemachineVirtualCamera reference is null.");
            return false;
        }
        m_VirtualCamera.Follow = transform;
        return true;
    }

    public void SetFollow(Transform transform)
    {
        m_VirtualCamera.ForceCameraPosition(transform.position - DefaultFollowPositionOffset, DefaultRotation.ToQuaternion());
        SetFollowTarget(transform);
    }
}