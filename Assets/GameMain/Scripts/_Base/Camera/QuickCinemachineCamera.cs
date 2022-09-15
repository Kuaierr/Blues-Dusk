using System.Collections;
using UnityEngine;
using UnityGameKit.Runtime;
using Cinemachine;

public class QuickCinemachineCamera : MonoSingletonBase<QuickCinemachineCamera>
{
    public CinemachineVirtualCamera m_VirtualCamera;
    public Vector3 DefaultFollowPositionOffset;
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
        this.transform.position = transform.position - DefaultFollowPositionOffset;
        StartCoroutine(FollowTarget(transform));
        Log.Info(this.transform.position);
    }

    IEnumerator FollowTarget(Transform transform)
    {
        yield return null;
        SetFollowTarget(transform);
    }
}