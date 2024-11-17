using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public LayerMask mask;

    private void Awake()
    {
        instance = this;
    }

    public bool LoSight(Vector3 start, Vector3 end)
    {
        print("Esta en el angulo");
        var dir = end - start;
        return !Physics.Raycast(start, dir, dir.magnitude, mask);
    }
}
