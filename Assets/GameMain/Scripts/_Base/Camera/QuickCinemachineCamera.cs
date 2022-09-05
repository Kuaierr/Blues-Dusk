using UnityEngine;
using UnityGameKit.Runtime;
using Cinemachine;

public class QuickCinemachineCamera : MonoSingletonBase<QuickCinemachineCamera>
{
    public CinemachineVirtualCamera m_VirtualCamera;
    public Vector3 DefaultFollowPositionOffset;
    public bool SetFollowTarget(Transform transform)
    {
        if (m_VirtualCamera == null)
        {
            Log.Fail("CinemachineVirtualCamera reference is null.");
            return false;
        }
        m_VirtualCamera.Follow = transform;

        return true;
    }

    // public void SetFollowPostion(Vector3 position)
    // {
    //     this.transform.position = position - DefaultFollowPositionOffset;
    //     Log.Info(this.transform.position);
    // }
}