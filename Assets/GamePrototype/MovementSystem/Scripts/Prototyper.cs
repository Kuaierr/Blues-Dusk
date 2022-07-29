using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using GameKit;
using GameKit.Timer;
using Geometry = GameKit.Utility.Geometry;

public class Prototyper : MonoBehaviour
{
    public LayerMask navigationLayer;
    public float pressUpdateInterval = 0.2f;
    public float suddenStopDistance = 0.1f;
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Ticker_Auto ticker;
    private Vector3 targetPos;
    private bool isManuallyRoatating = false;
    [SerializeField] private Transform movePositionTransform;

    private void Start()
    {
        targetPos = Vector3.zero;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        // navMeshAgent.updateRotation = false;
        ticker = new Ticker_Auto(pressUpdateInterval);
        ticker.Register(MoveToDestination);
        ticker.Start();
        ticker.Pause();
    }
    void Update()
    {
        if (Geometry.EuclidDistance(this.transform.position.ToVector2(), targetPos.ToVector2()) <= suddenStopDistance)
            navMeshAgent.velocity = Vector3.zero;
        animator.SetFloat("VelocityX", Mathf.Abs(navMeshAgent.velocity.x));
        animator.SetFloat("VelocityY", Mathf.Abs(navMeshAgent.velocity.y));

        if (InputManager.instance.GetWorldMouseButtonDown(0))
        {
            MoveToDestination();
        }
        else
        {
            if (InputManager.instance.GetWorldMouseButton(0))
            {
                ticker.Resume();
            }

            if (InputManager.instance.GetWorldMouseButtonUp(0))
            {
                ticker.Pause();
            }
        }
    }

    /// <summary>
    /// LateUpdate is called every frame, if the Behaviour is enabled.
    /// It is called after all Update functions have been called.
    /// </summary>
    private void LateUpdate()
    {
        // if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon && navMeshAgent.updateRotation == false)
        // {
        //     Vector3 lookAt = new Vector3(navMeshAgent.velocity.normalized.x, navMeshAgent.velocity.normalized.y, 0);
        //     transform.rotation = Quaternion.LookRotation(lookAt);
        // }
    }

    private void SetDestination(Vector3 pos)
    {
        if (pos != Vector3.zero)
        {
            navMeshAgent.destination = pos;
            targetPos = pos;
        }
    }

    private void MoveToDestination()
    {
        
        
        Vector3 pos = CursorSystem.current.TryGetHitPosition();
        SetDestination(pos);
    }
}
