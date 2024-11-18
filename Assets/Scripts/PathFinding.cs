using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public List<Node> GetPath(Vector3 PosInicial, Vector3 PosFinal)
    {

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
        }

        var nodoCercano = colliders[0];
        var distanciaMin = Vector3.Distance(punto, nodoCercano.transform.position);
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
