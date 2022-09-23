using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityGameKit.Runtime;
using GameKit.Element;
using GameKit.Timer;
using GameKit.QuickCode;
using DG.Tweening;
using System.Linq;


public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private LayerMask layerMask;

    //����
    [Header("Bezier Config")]
    [SerializeField]
    private float SmoothingLength = 0.25f;  //handler Ӱ������
    [SerializeField]
    private int SmoothingSections = 10;  //points in each section ÿ����waypoint֮�����ɵĵ���
    [SerializeField]
    [Range(-1, 1)]
    private float SmoothingFactor = 0;  //�ƶ������н�

    private NavMeshPath CurrentPath;
    public Vector3[] PathLocations = new Vector3[0];
    private int PathIndex = 0;

    [Header("Movement Config")]
    [SerializeField]
    [Range(0, 0.99f)]
    private float Smoothing = 0.25f; //ƽ��ϵ��
    [SerializeField]
    private float RotateSpeed = 1; //��ת�ٶ�ϵ��
    [SerializeField]
    private Vector3 TargetDirection;
    [SerializeField]
    private Vector3 MovementVector;
    private float LerpTime = 0;


    private Vector3 InfinityVector = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

    //test
    [SerializeField]
    private LayerMask FloorLayer;

    void Start()
    {
        CurrentPath = new NavMeshPath();
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        layerMask = LayerMask.GetMask("Interactive") | LayerMask.GetMask("Navigation");
    }

    //void Update()
    //{
    //    if (InputManager.instance.GetWorldMouseButtonDown(0) && navMeshAgent != null)
    //    {
    //        HandleInput();
    //    }
    //    MoveCharacter();
    //    animator.SetFloat("VelocityX", Mathf.Abs(navMeshAgent.velocity.x));
    //    animator.SetFloat("VelocityY", Mathf.Abs(navMeshAgent.velocity.y));

    //    //if (navMeshAgent.velocity.sqrMagnitude > Mathf.Epsilon)
    //    //{
    //    //    transform.rotation = Quaternion.LookRotation(navMeshAgent.velocity.normalized);
    //    //}
    //}
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("clicked");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, FloorLayer))
            {
                SetDestination(hit.point);
            }
        }
        MoveCharacter();
    }

    private void HandleInput()
    {
        MoveToDestination();
    }

    private void MoveToDestination()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, FloorLayer))
            {
                SetDestination(hit.point);
            }
        }
    }

    private void MoveCharacter()
    {
        if (PathIndex >= PathLocations.Length)
        {
            return;
        }
        if (Vector3.Distance(transform.position, PathLocations[PathIndex] + (navMeshAgent.baseOffset * Vector3.up)) <= navMeshAgent.radius)
        {
            PathIndex++;
            LerpTime = 0;
            if (PathIndex >= PathLocations.Length)
            {
                return;
            }
        }
        MovementVector = (PathLocations[PathIndex]+(navMeshAgent.baseOffset * Vector3.up) - transform.position).normalized;
        
        TargetDirection = Vector3.Lerp(
            TargetDirection,
            MovementVector,
            Mathf.Clamp01(LerpTime * RotateSpeed * (1 - Smoothing))
        );

        Vector3 lookDirection = MovementVector;
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.LookRotation(lookDirection),
                Mathf.Clamp01(LerpTime * RotateSpeed * (1 - Smoothing))
            );
        }
        navMeshAgent.Move(TargetDirection * navMeshAgent.speed * Time.deltaTime);

        LerpTime += Time.deltaTime;
    }

    private void SetDestination(Vector3 position)
    {
        navMeshAgent.ResetPath();
        NavMesh.CalculatePath(transform.position, position, navMeshAgent.areaMask, CurrentPath);
        Vector3[] wayPoints = CurrentPath.corners;
        if(wayPoints.Length > 2)
        {
            Bezier[] beziers = new Bezier[wayPoints.Length-1];
            SmoothCurves(beziers,wayPoints);
            PathLocations = GetPathLocations(beziers);
            PathIndex = 0;
        }
        else
        {
            PathLocations = wayPoints;
            PathIndex = 0;
        }
    }

    #region Create Bezier Path
    private void SmoothCurves(Bezier[] Curves, Vector3[] Corners)
    {
        for (int i = 0; i < Curves.Length; i++)
        {
            if (Curves[i] == null)
            {
                Curves[i] = new Bezier();
            }

            Vector3 position = Corners[i];
            Vector3 lastPosition = i == 0 ? Corners[i] : Corners[i - 1];
            Vector3 nextPosition = Corners[i + 1];

            Vector3 lastDirection = (position - lastPosition).normalized;
            Vector3 nextDirection = (nextPosition - position).normalized;

            Vector3 startTangent = (lastDirection + nextDirection) * SmoothingLength;
            Vector3 endTangent = (nextDirection + lastDirection) * (-1) * SmoothingLength;

            Curves[i].points[0] = position; 
            Curves[i].points[1] = position + startTangent;
            Curves[i].points[2] = nextPosition + endTangent;
            Curves[i].points[3] = nextPosition;
        }

        //special first point
        {
            Vector3 nextDirection = (Curves[1].EndPoint - Curves[1].StartPoint).normalized;
            Vector3 lastDirection = (Curves[0].EndPoint - Curves[0].StartPoint).normalized;

            Curves[0].points[2] = Curves[0].points[3] +
                (nextDirection + lastDirection) * -1 * SmoothingLength;
        }
    }

    private Vector3[] GetPathLocations(Bezier[] Curves)
    {
        Vector3[] pathLocations = new Vector3[Curves.Length * SmoothingSections];

        int index = 0;
        for (int i = 0; i < Curves.Length; i++)
        {
            Vector3[] segments = Curves[i].GetSegment(SmoothingSections);
            for (int j = 0; j < segments.Length; j++)
            {
                pathLocations[index] = segments[j];
                index++;
            }
        }

        pathLocations = PostProcessPath(Curves, pathLocations);

        return pathLocations;
    }
    #endregion

    #region post processing Bezier
    private Vector3[] PostProcessPath(Bezier[] beziers, Vector3[] path)
    {
        Vector3[] finalPath = RemoveOverSmoothPoints(beziers, path);

        path = RemoveTooClosePoints(beziers, path);

        path = SamplePointInNavmesh(beziers, path);

        return finalPath;
    }

    private Vector3[] RemoveOverSmoothPoints(Bezier[] beziers, Vector3[] path)
    {
        if (path.Length <= 2)
            return path;

        int index = 1;
        int lastindex = 0;
        for(int i = 0; i < beziers.Length-1; i++)
        {
            Vector3 targetDir = (beziers[i].EndPoint - beziers[i].StartPoint).normalized;
            for(int j = 0; j < SmoothingSections - 1; j++)
            {
                Vector3 segmentDir = (path[index] - path[lastindex]).normalized;
                float dot = Vector3.Dot(targetDir, segmentDir);
                if (dot <= SmoothingFactor)
                {
                    path[index] = InfinityVector;
                }
                else
                {
                    lastindex = index;
                }
                index++;
            }
            index++;
        }
        path[path.Length - 1] = beziers[beziers.Length - 1].EndPoint;

        Vector3[] silmPath = path.Except(new Vector3[] {InfinityVector}).ToArray();
        return silmPath;
    }

    private Vector3[] RemoveTooClosePoints(Bezier[] beziers, Vector3[] path)
    {
        if (path.Length <= 2)
            return path;

        int lastIndex = 0;
        int index = 1;
        for(int i =0; i < path.Length-1; i++)
        {
            if (Vector3.Distance(path[index], path[lastIndex]) <= navMeshAgent.radius)
            {
                path[index] = InfinityVector;
            }
            else
            {
                lastIndex = index;
            }
            index++;
        }
        Vector3[] silmPath = path.Except(new Vector3[] { InfinityVector }).ToArray();
        return silmPath;
    }

    private Vector3[] SamplePointInNavmesh(Bezier[] beziers, Vector3[] path)
    {
        for (int i = 0; i < path.Length; i++)
        {
            if (NavMesh.SamplePosition(path[i], out NavMeshHit hit, navMeshAgent.radius * 1.5f, navMeshAgent.areaMask))
            {
                path[i] = hit.position;
            }
            else
            {
                path[i] = InfinityVector;
            }
        }
        Vector3[] silmPath = path.Except(new Vector3[] { InfinityVector }).ToArray();
        return silmPath;
    }
    #endregion

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        for(int i = 0; i < PathLocations.Length; i++)
        {
            Gizmos.DrawSphere(PathLocations[i],0.1f);
        }
        Gizmos.color = Color.blue;
        for(int j = 0; j < PathLocations.Length-1; j++)
        {
            Gizmos.DrawLine(PathLocations[j],PathLocations[j+1]);
        }
    }
}
