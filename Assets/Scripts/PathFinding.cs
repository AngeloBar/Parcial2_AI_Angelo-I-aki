using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    public static Pathfinding instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public List<Node> CalculateAStar(Node startingNode, Node goalNode)
    {
        var frontier = new PriorityQueue<Node>();
        frontier.Enqueue(startingNode, 0);

        Dictionary<Node, Node> cameFrom = new Dictionary<Node, Node>();
        Dictionary<Node, int> costSoFar = new Dictionary<Node, int>();
        costSoFar.Add(startingNode, 0);

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            if (current == goalNode)
            {
                List<Node> path = new List<Node>();

                while (current != startingNode)
                {
                    path.Add(current);
                    current = cameFrom[current];
                }
                path.Reverse();
                return path;
            }
            foreach (var next in current.Vecinos)
            {
                int newCost = costSoFar[current] + next.heuristic;
                float priority = newCost + Vector3.Distance(next.transform.position, goalNode.transform.position);

                if (!costSoFar.ContainsKey(next))
                {
                    costSoFar.Add(next, newCost);
                    frontier.Enqueue(next, priority);
                    cameFrom.Add(next, current);
                }
                else if (costSoFar[next] > newCost)
                {
                    frontier.Enqueue(next, priority);
                    cameFrom[next] = current;
                    costSoFar[next] = newCost;
                }
            }
        }

        return new List<Node>();
    }
}
