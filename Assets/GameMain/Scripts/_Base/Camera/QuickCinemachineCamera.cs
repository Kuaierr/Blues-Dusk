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
    private float DefaultOrthographicSize = 3f;

    protected override void Awake()
    {
        base.Awake();
        DefaultOrthographicSize = m_VirtualCamera.m_Lens.OrthographicSize;
    }
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
        StopAllCoroutines();
        StartCoroutine(ShrinkProcess(0.75f * m_VirtualCamera.m_Lens.OrthographicSize, 2f));
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
        StopAllCoroutines();
        StartCoroutine(EnlargeProcess(DefaultOrthographicSize, 2f));
    }

    IEnumerator ShrinkProcess(float size, float speed)
    {
        while (m_VirtualCamera.m_Lens.OrthographicSize >= size)
        {
            m_VirtualCamera.m_Lens.OrthographicSize -= speed * Time.deltaTime;
            yield return null;
        }
        Debug.Log("Shrink End.");
    }

    IEnumerator EnlargeProcess(float size, float speed)
    {
        while (m_VirtualCamera.m_Lens.OrthographicSize <= size)
        {
            m_VirtualCamera.m_Lens.OrthographicSize += speed * Time.deltaTime;
            yield return null;
        }
        Debug.Log("Enlarge End.");
    }
}