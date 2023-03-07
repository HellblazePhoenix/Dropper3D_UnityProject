using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge_Button : MonoBehaviour
{
    public Transform bridgeOrigin;


    private void OnTriggerExit(Collider other)
    {
        if (bridgeOrigin.childCount == 0) bridgeOrigin.GetComponent<PhysicsBuilder>().Build();
        else bridgeOrigin.GetComponent<PhysicsBuilder>().ClearBridge();
    }
}
