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

    public Transform nodeTarget;

    public List<Node> path = new();

    private void Update()
    {
        if (InView(target))
        {
            print("A LA VISTA");
            Vector3 lookAtDirection = (target.position - transform.position).normalized;
            Movement(lookAtDirection);

        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            print("funca");
            path = PathFinding.instance.GetPath(this.transform.position, nodeTarget.transform.position);
        }
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
            print("Esta en el radio");
            if (Vector2.Angle(-transform.right, dir) <= angle * 0.5f)
            {
                print("Esta en angulo");
                return LoSight(transform.position, obj.position);
            }
            
            else
                print("No esta en angulo");
        }
        return false;
    }

    public bool LoSight(Vector3 start, Vector3 end)
    {
        Vector3 dir = (end - start).normalized;
        float distance = Vector3.Distance(start, end);

        if(Physics.Raycast(start, dir, out RaycastHit hit, distance, mask))
        {
            print("Vista bloqueada");
            return false;
        }
            print("Vista no bloqueada");
            return true;
        
    }

    public void Movement(Vector3 direction)
    {
        direction.z = 0;

        transform.position += direction * speed * Time.deltaTime;
    }
}
