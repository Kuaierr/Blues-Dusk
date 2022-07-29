using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameKit;
using UnityEngine.EventSystems;

public sealed class CursorSystem : MonoSingletonBase<CursorSystem>
{
    public static Vector3 MAGIC_VECTOR = Vector3.zero;
    public RaycastHit hitInfo;
    public LayerMask navigationLayer;

    private void Update()
    {
        if (IsActive)
        {
            Vector3 originPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            Vector3 diretcion = Camera.main.transform.forward;
            if (Physics.Raycast(originPos, diretcion, out RaycastHit hitInfo, 20, navigationLayer))
                this.hitInfo = hitInfo;
        }
    }

    public Vector3 TryGetHitPosition()
    {
        if (!IsActive || hitInfo.transform == null || hitInfo.transform.gameObject == null)
        {
            Utility.Debugger.LogWarning("No Hit Target Exsit.");
            return MAGIC_VECTOR;
        }
        return this.hitInfo.point;
    }

    public GameObject TryGetHitGameObject()
    {
        if (!IsActive || hitInfo.transform == null || hitInfo.transform.gameObject == null)
        {
            Utility.Debugger.LogWarning("No Hit Target Exsit.");
            return null;
        }
        return this.hitInfo.transform.gameObject;
    }

    public T TryGetHitComponent<T>() where T : class
    {
        if (!IsActive || hitInfo.transform == null || hitInfo.transform.gameObject == null)
        {
            Utility.Debugger.LogWarning("No Hit Target Exsit.");
            return null;
        }
        T component = hitInfo.transform.GetComponent<T>();
        if (component == null)
        {
            Utility.Debugger.LogWarning(Utility.Text.Format("Hit Target Has No {0} Component.", typeof(T).Name));
            return null;
        }
        return component;
    }
}
