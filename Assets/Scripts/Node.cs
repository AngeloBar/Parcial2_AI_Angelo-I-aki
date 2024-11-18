using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private List<Node> _vecinos = new();

    [SerializeField] private float searchRange; 

    [SerializeField] private float heuristic;
    void Awake()
    {
        var colliders = Physics.OverlapSphere(transform.position, searchRange, LayerMask.GetMask("Node"));

        foreach (var collider in colliders)
        {
            var node = collider.GetComponent<Node>();

            if (node == null || node == this || _vecinos.Contains(node)) 
            {
                continue;
            }

            _vecinos.Add(node);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, searchRange);
    }
}
