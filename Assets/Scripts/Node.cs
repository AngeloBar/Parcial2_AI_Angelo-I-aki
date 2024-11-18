using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] private List<Node> _vecinos = new();

    public List<Node> Vecinos { get { return _vecinos;  } }

    [SerializeField] private float searchRange; 

    [SerializeField] public float heuristic = 99999;

    public Node nodoPrevio;
    void Awake()
    {
        heuristic = 99999;

        var colliders = Physics.OverlapSphere(transform.position, searchRange, LayerMask.GetMask("Node"));

        foreach (var collider in colliders)
        {
            var node = collider.GetComponent<Node>();

            if (node == null || node == this || _vecinos.Contains(node) || !PathFinding.EnVision(transform.position, node.transform.position)) 
            {
                continue;
            }

            _vecinos.Add(node);
        }
    }

    private void DrawOnGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, searchRange);
    }
}
