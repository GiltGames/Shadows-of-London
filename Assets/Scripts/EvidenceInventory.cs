using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class EvidenceInventory : MonoBehaviour
{
    public RaycastCube evidence1, evidence2, evidence3, evidence4, evidence5, evidence6;

    public bool EvidenceStatus;

    void Start()
    {
        RaycastCube raycastCubeComponent = evidence1;
    }

    private void Update()
    {

    }
}
