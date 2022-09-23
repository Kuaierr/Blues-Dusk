using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityGameKit.Runtime;
using GameKit.Element;
using GameKit.Timer;
using GameKit.QuickCode;
using DG.Tweening;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour
{
    public float RoateSpeed = 5;
    private Animator m_Animator;
    private NavMeshAgent m_NavMeshAgent;
    private Vector3 m_CachedTargetPos;
    private Vector3 m_CachedInteractivePos;
    private GameElementBase m_CachedInteractive;
    private LayerMask layerMask;
    private UnityAction m_OnArrived;
    private PlayerMovement m_Movement;
    public static Player Current;
    [SerializeField] private bool m_UpdateRotation = true;
    [SerializeField] private bool m_StartMove;
    private void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_Movement = GetComponent<PlayerMovement>();
        m_Movement.OnInit();
        layerMask = LayerMask.GetMask("Interactive") | LayerMask.GetMask("Navigation");
        m_NavMeshAgent.updatePosition = m_UpdateRotation;
        Current = this;
        m_StartMove = false;
    }
    void Update()
    {
        // m_Animator.SetFloat("VelocityX", Mathf.Abs(m_NavMeshAgent.velocity.x));
        // m_Animator.SetFloat("VelocityY", Mathf.Abs(m_NavMeshAgent.velocity.y));

        if (InputManager.instance.GetWorldMouseButtonDown(0))
        {
            MoveToDestination();
            m_StartMove = true;
        }

        if (m_StartMove)
            m_Movement.OnUpdate();
    }

    public void SetTransform(Transform trans)
    {
        if (trans == null)
            return;

        this.transform.position = new Vector3(trans.position.x, this.transform.position.y, trans.position.z);
        this.transform.rotation = trans.rotation;
    }

    private void MoveToDestination()
    {
        StopAllCoroutines();
        m_OnArrived -= RotateAction;
        if (!CursorSystem.current.IsActive)
            return;
        RaycastHit hitInfo = CursorSystem.current.GetHitInfo(layerMask);
        GameElementBase interactive = CursorSystem.current.GetComponentFromRaycast<GameElementBase>(hitInfo);
        if (interactive == null)
        {
            Vector3 pos = CursorSystem.current.GetPositionFromRaycast(hitInfo);
            m_CachedTargetPos = pos;
            if (m_CachedTargetPos != Vector3.zero)
                m_Movement.SetDestination(m_CachedTargetPos);
            // m_NavMeshAgent.destination = m_CachedTargetPos;
        }
        else
        {
            Log.Warning("MoveToDestination");
            m_CachedInteractivePos = interactive.transform.position;
            m_CachedInteractive = interactive;
            m_CachedTargetPos = interactive.InteractPosition;
            // m_NavMeshAgent.destination = m_CachedTargetPos;
            m_Movement.SetDestination(m_CachedTargetPos);
            m_NavMeshAgent.velocity = Vector3.one.IgnoreY() * 0.0001f; // 提供基础初速度，避免跳过速度检测
            m_OnArrived += RotateAction;
            StopAction();
        }
    }

    private void RotateAction() => StartCoroutine(RotateToElement(m_CachedInteractivePos));
    private void StopAction() => StartCoroutine(StopCallback());
    private void RotateTo(Vector3 position, float roateMultipier = 1)
    {
        Quaternion quaternion = Quaternion.LookRotation(position - transform.position);
        Quaternion clampQuaternion = Quaternion.Euler(quaternion.eulerAngles.IgnoreX().IgnoreZ());
        this.transform.rotation = Quaternion.Lerp(transform.rotation, clampQuaternion, RoateSpeed * roateMultipier * Time.deltaTime);
    }

    private IEnumerator RotateToElement(Vector3 targetPos)
    {
        Vector2 faceDirection = targetPos.ToVector2() - transform.position.ToVector2();
        float angle = Vector3.Angle(transform.forward.ToVector2(), faceDirection);
        do
        {
            RotateTo(targetPos.IgnoreY());
            faceDirection = targetPos.ToVector2() - transform.position.ToVector2();
            angle = Vector3.Angle(transform.forward.ToVector2(), faceDirection);
            yield return null;
        } while (angle > 5f);
        m_CachedInteractive?.OnInteract();
    }

    private IEnumerator StopCallback()
    {
        do
        {
            yield return null;
        } while (!m_Movement.IsStop);
        m_OnArrived?.Invoke();
        Log.Success("Arrived");
    }


    /// <summary>
    /// 原Plyaer Controller部分
    /// </summary>



    private void OnDrawGizmos()
    {
        Vector2 faceDirection = m_CachedInteractivePos.ToVector2() - transform.position.ToVector2();
        Gizmos.color = Color.green;
        Gizmos.DrawLine(this.transform.position, this.transform.position + transform.forward * 4);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, m_CachedInteractivePos.IgnoreY());
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(this.transform.position, m_CachedTargetPos.IgnoreY());
    }
}
