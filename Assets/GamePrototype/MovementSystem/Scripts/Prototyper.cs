using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameKit;
using GameKit.Timer;
using Geometry = GameKit.Utility.Geometry;
using DG.Tweening;

public class Prototyper : MonoBehaviour
{
    public static Vector3 MAGIC_INTERACTIVE_POS = Vector3.zero;
    public float pressUpdateInterval = 0.2f;
    public float suddenStopDistance = 0.1f;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Ticker_Auto ticker;
    private Vector3 targetPos;
    private Vector3 m_CachedInteractivePos;
    private InteractiveElement m_CachedInteractive;
    private bool isManuallyRoatating = false;
    private LayerMask layerMask;

    private void Start()
    {
        targetPos = Vector3.zero;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        layerMask = LayerMask.GetMask("Interactive") | LayerMask.GetMask("Navigation");
        ticker = new Ticker_Auto(pressUpdateInterval);
        ticker.Register(MoveToDestination);
        ticker.Start();
        ticker.Pause();
    }
    void Update()
    {
        // if (Geometry.EuclidDistance(this.transform.position.ToVector2(), targetPos.ToVector2()) <= suddenStopDistance)
        // {
        //     navMeshAgent.velocity = Vector3.zero;

        // }

        if (navMeshAgent.velocity == Vector3.zero)
        {
            if (m_CachedInteractivePos != MAGIC_INTERACTIVE_POS)
            {
                RotateTo(m_CachedInteractivePos);
                Vector2 faceDirection = m_CachedInteractivePos.ToVector2() - transform.position.ToVector2();
                float angle = Vector3.Angle(transform.forward.ToVector2(), faceDirection);
                if (angle <= 5f)
                {
                    m_CachedInteractivePos = MAGIC_INTERACTIVE_POS;
                    m_CachedInteractive?.OnInteract();
                }
            }

        }
        animator.SetFloat("VelocityX", Mathf.Abs(navMeshAgent.velocity.x));
        animator.SetFloat("VelocityY", Mathf.Abs(navMeshAgent.velocity.y));

        if (InputManager.instance.GetWorldMouseButtonDown(0))
        {
            MoveToDestination();
        }
        // else
        // {

        //     if (InputManager.instance.GetWorldMouseButton(0))
        //     {
        //         Debug.Log($"Keep Press Mouse Button");
        //         ticker.Resume();
        //     }

        //     if (InputManager.instance.GetWorldMouseButtonUp(0))
        //     {
        //         ticker.Pause();
        //     }
        // }
    }

    private void MoveToDestination()
    {
        RaycastHit hitInfo = CursorSystem.current.GetHitInfo(layerMask);
        InteractiveElement interactive = CursorSystem.current.GetComponentFromRaycast<InteractiveElement>(hitInfo);
        if (interactive == null)
        {
            Vector3 pos = CursorSystem.current.GetPositionFromRaycast(hitInfo);
            if (pos != CursorSystem.MAGIC_POS)
            {
                navMeshAgent.destination = pos;
                targetPos = pos;
            }
        }
        else
        {
            navMeshAgent.destination = interactive.InteractPosition;
            m_CachedInteractivePos = interactive.transform.position;
            m_CachedInteractive = interactive;
            targetPos = interactive.InteractPosition;
        }
    }

    private void RotateTo(Vector3 position)
    {
        Quaternion quaternion = Quaternion.LookRotation(position - transform.position);
        this.transform.rotation = Quaternion.Lerp(transform.rotation, quaternion, 5 * Time.deltaTime);
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        Vector2 faceDirection = m_CachedInteractivePos.ToVector2() - transform.position.ToVector2();
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, this.transform.position + transform.forward * 4);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, m_CachedInteractivePos.IgnoreY());
    }
}
