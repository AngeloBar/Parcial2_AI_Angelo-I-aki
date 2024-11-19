using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public float radius;
    public float angle;
    public LayerMask mask;
    public float speed;

    public List<Node> path = new();

    public List<Node> patroll = new();

    public int currentPatrolPoint = 0;

    public Enemy[] allies;

    public bool playerFound;

    public int currentPathPoint;

    private FSM _fsm = new FSM();

    private void Start()
    {
        _fsm.AddState(FSM.AgentStates.Patrol, new PatrolState(_fsm, this));
        _fsm.AddState(FSM.AgentStates.Chase, new ChaseState(_fsm, this));
        _fsm.AddState(FSM.AgentStates.Seek, new SeekState(_fsm, this));
        _fsm.AddState(FSM.AgentStates.GoBack, new GoBackState(_fsm, this));
        _fsm.ChangeState(FSM.AgentStates.Patrol);
    }

    private void Update()
    {
        _fsm.ArtificialUpdate();
    }

    public void Alert()
    {
        for (int i = 0; i < allies.Length; i++)
        {
            allies[i].playerFound = true;
        }
    }

    public void Patrol()
    {
        Vector3 lookAtDirection = (patroll[currentPatrolPoint].transform.position - transform.position).normalized;
        Movement(lookAtDirection);

        if (Vector3.Distance(transform.position, patroll[currentPatrolPoint].transform.position) <= 0.2)
        {
            currentPatrolPoint++;
            if (currentPatrolPoint >= patroll.Count)
            {
                currentPatrolPoint = 0;
            }
        }
    }

    public bool Seek()
    {
        var Arrive = false;

        Vector3 lookAtDirection = (path[currentPathPoint].transform.position - transform.position).normalized;
        Movement(lookAtDirection);

        if (Vector3.Distance(transform.position, path[currentPathPoint].transform.position) <= 0.2)
        {
            currentPathPoint++;
            Debug.Log(Arrive);
            if (currentPathPoint >= path.Count)
            {
                Arrive = true;
                currentPathPoint = 0;
            }
        }
        return Arrive;
    }

    public bool GoBack()
    {
        var Arrive = false;

        Vector3 lookAtDirection = (path[currentPathPoint].transform.position - transform.position).normalized;
        Movement(lookAtDirection);

        if (Vector3.Distance(transform.position, path[currentPathPoint].transform.position) <= 0.2)
        {
            currentPathPoint++;
            if (currentPathPoint >= path.Count)
            {
                currentPathPoint = 0;
                Arrive = true;
            }
        }
        return Arrive;
    }

    private void OnDrawGizmos()
    {
        if (path.Count > 0)
        {
            foreach (Node node in path)
            {
                Gizmos.DrawSphere(node.transform.position, 1);
            }
        }
    }

    public bool InView(Transform obj)
    {
        var dir = obj.position - transform.position;

        if (dir.magnitude <= radius)
        {
            if (Vector2.Angle(-transform.right, dir) <= angle * 0.5f)
            {
                return LoSight(transform.position, obj.position);
            }
        }
        return false;
    }

    public bool LoSight(Vector3 start, Vector3 end)
    {
        Vector3 dir = (end - start).normalized;
        float distance = Vector3.Distance(start, end);

        if(Physics.Raycast(start, dir, out RaycastHit hit, distance, mask))
        {
            return false;
        }
            return true;
        
    }

    public void Movement(Vector3 direction)
    {
        direction.z = 0;

        transform.position += direction * speed * Time.deltaTime;

        if(direction != Vector3.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 180f;
            transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }
}
