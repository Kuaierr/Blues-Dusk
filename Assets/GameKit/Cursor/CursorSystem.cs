using UnityEngine;
using GameKit;
using System.Collections.Generic;

public sealed class CursorSystem : MonoSingletonBase<CursorSystem>
{
    public static Vector3 MAGIC_POS = Vector3.zero;
    private Vector3 originPos, diretcion;
    private Dictionary<int, RaycastHit> m_CachedRaycastInfo;
    private void Start()
    {
        m_CachedRaycastInfo = new Dictionary<int, RaycastHit>();
    }

    private void Update()
    {
        if (IsActive)
        {
            if (m_CachedRaycastInfo.Count > 0)
                m_CachedRaycastInfo.Clear();
            originPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            diretcion = Camera.main.transform.forward;
        }
    }

    public RaycastHit GetHitInfo(int layer)
    {
        // originPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        // diretcion = Camera.main.transform.forward;
        if (Physics.Raycast(originPos, diretcion, out RaycastHit tmpHitInfo, 20, layer))
        {
            m_CachedRaycastInfo.Add(layer, tmpHitInfo);
            return tmpHitInfo;
        }
        return default(RaycastHit);
    }

    public Vector3 GetHitPosition(int layer)
    {
        if (m_CachedRaycastInfo.ContainsKey(layer))
            return GetPositionFromRaycast(m_CachedRaycastInfo[layer]);
        else
        {
            originPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            diretcion = Camera.main.transform.forward;
            if (Physics.Raycast(originPos, diretcion, out RaycastHit tmpHitInfo, 20, layer))
            {
                m_CachedRaycastInfo.Add(layer, tmpHitInfo);
                return GetPositionFromRaycast(tmpHitInfo);
            }
            return MAGIC_POS;
        }
    }

    public GameObject GetHitGameObject(int layer)
    {
        if (m_CachedRaycastInfo.ContainsKey(layer))
            return GetGameObjectFromRaycast(m_CachedRaycastInfo[layer]);
        else
        {
            originPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            diretcion = Camera.main.transform.forward;
            if (Physics.Raycast(originPos, diretcion, out RaycastHit tmpHitInfo, 20, layer))
            {
                m_CachedRaycastInfo.Add(layer, tmpHitInfo);
                return GetGameObjectFromRaycast(tmpHitInfo);
            }
            return null;
        }
    }

    public T GetHitComponent<T>(int layer) where T : class
    {
        if (m_CachedRaycastInfo.ContainsKey(layer))
            return GetComponentFromRaycast<T>(m_CachedRaycastInfo[layer]);
        else
        {
            originPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            diretcion = Camera.main.transform.forward;
            if (Physics.Raycast(originPos, diretcion, out RaycastHit tmpHitInfo, 20, layer))
            {
                m_CachedRaycastInfo.Add(layer, tmpHitInfo);
                return GetComponentFromRaycast<T>(tmpHitInfo);
            }
            return null;
        }
    }

    public T GetComponentFromRaycast<T>(RaycastHit raycastHit) where T : class
    {
        if (raycastHit.transform == null || raycastHit.transform.gameObject == null)
        {
            Utility.Debugger.LogWarning("No Hit Target Exsit.");
            return null;
        }
        T component = raycastHit.transform.GetComponent<T>();
        if (component == null)
            Utility.Debugger.LogWarning("Hit Target Has No {0} Component.", typeof(T).Name);

        return component;
    }

    public Vector3 GetPositionFromRaycast(RaycastHit raycastHit)
    {
        if (raycastHit.transform == null || raycastHit.transform.gameObject == null)
        {
            Utility.Debugger.LogWarning("No Hit Target Exsit.");
            return MAGIC_POS;
        }
        else
            Utility.Debugger.LogWarning(raycastHit.transform.gameObject.name);
        return raycastHit.point;
    }

    public GameObject GetGameObjectFromRaycast(RaycastHit raycastHit)
    {
        if (raycastHit.transform == null || raycastHit.transform.gameObject == null)
        {
            Utility.Debugger.LogWarning("No Hit Target Exsit.");
            return null;
        }
        return raycastHit.transform.gameObject;
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(originPos, originPos + diretcion * 20);
        }
    }
}
