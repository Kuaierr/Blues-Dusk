using System.Collections;
using UnityEngine;
using UnityGameKit.Runtime;
using Cinemachine;

public class QuickCinemachineCamera : MonoSingletonBase<QuickCinemachineCamera>
{
    public CinemachineVirtualCamera m_VirtualCamera;
    public Vector3 DefaultFollowPositionOffset;
    public Vector3 DefaultRotation;
    public Transform FocusTransform;
    private Transform m_CachedPlayerTransform;
    private bool FollowPlayer(Transform transform)
    {
        if (m_VirtualCamera == null)
        {
            Log.Fail("CinemachineVirtualCamera reference is null.");
            return false;
        }
        m_VirtualCamera.Follow = transform;
        m_CachedPlayerTransform = transform;
        return true;
    }

    public void SetFollowPlayer(Transform transform)
    {
        m_VirtualCamera.ForceCameraPosition(transform.position - DefaultFollowPositionOffset, DefaultRotation.ToQuaternion());
        FollowPlayer(transform);
    }

    public void SetFocus(Vector3 position)
    {
        FocusTransform.position = position;
        m_VirtualCamera.Follow = FocusTransform;
    }

    public void ResetFocus()
    {
        if (m_CachedPlayerTransform == null)
        {
            Log.Fail("m_DefaultTargetTransform is null");
            return;
        }
        Log.Success("Reset Focus to {0}", m_CachedPlayerTransform.gameObject.name);
        FollowPlayer(m_CachedPlayerTransform);
    }
}