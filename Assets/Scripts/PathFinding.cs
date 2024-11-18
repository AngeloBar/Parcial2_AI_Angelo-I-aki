using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public List<Node> GetPath(Vector3 PosInicial, Vector3 PosFinal)
    {
        var nodoInicial = NodoMasCercano(PosInicial);
        var nodoFinal = NodoMasCercano(PosFinal);

        var openNode = new PriorityQueue<Node>();
        var closeNode = new HashSet<Node>();

        openNode.Enqueue(nodoInicial, nodoInicial.heuristic);

        while (openNode.Count > 0)
        {
            var actualNode = openNode.Dequeue();

            foreach (var vecino in actualNode.Vecinos)
            {
                if (closeNode.Contains(vecino)) 
                {
                    continue;
                }

                var heuristic = actualNode.heuristic +
                    Mathf.RoundToInt(Vector3.Distance(actualNode.transform.position, vecino.transform.position)) +
                    Mathf.RoundToInt(Vector3.Distance(nodoFinal.transform.position, vecino.transform.position));

                var heuristicMismaDist = actualNode.heuristic + 1 + 
                    Vector3.Distance(nodoFinal.transform.position, vecino.transform.position);

                if (vecino.heuristic > heuristic)
                {
                    vecino.heuristic = heuristic;
                }
            }
        }
    }

    private Node NodoMasCercano(Vector3 punto)
    {
        var rangoBusqueda = 2;
        var colliders = Physics.OverlapSphere(punto, rangoBusqueda, LayerMask.GetMask("Node"));

        colliders = GetNodoEnVision(punto, colliders).ToArray();

        while(colliders.Length == 0)
        {
            rangoBusqueda *= 2;

            colliders = Physics.OverlapSphere(punto, rangoBusqueda, LayerMask.GetMask("Node"));

            colliders = GetNodoEnVision(punto, colliders).ToArray();
        }

        var nodoCercano = colliders[0];
        var distanciaMin = Vector3.Distance(punto, nodoCercano.transform.position);

        for (int i = 1; i < colliders.Length; i++)
        {
            if (Vector3.Distance(colliders[0].transform.position, punto) < distanciaMin)
            {
                nodoCercano = colliders[i];
                distanciaMin = Vector3.Distance(punto, nodoCercano.transform.position);
            }
        }

        return nodoCercano.GetComponent<Node>();
    }

    public List<Collider> GetNodoEnVision(Vector3 from, Collider[] colliders)
    {
        var nodoEnVision = new List<Collider>();

        foreach (var collider in colliders) 
        {
            if (EnVision(from, collider.transform.position))
                nodoEnVision.Add(collider);
        }
        return nodoEnVision;
    }

    public bool EnVision(Vector3 from, Vector3 to)
    {
        var dir = to - from;
        return !Physics.Raycast(from, dir, dir.magnitude, LayerMask.GetMask("wall"));
    }
}
