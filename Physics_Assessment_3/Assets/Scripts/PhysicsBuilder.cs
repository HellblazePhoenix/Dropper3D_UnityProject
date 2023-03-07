using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsBuilder : MonoBehaviour
{
    //so a tool tip is just a summary then like the normal triple slash but I assume that as a function of unity this allows the tooltip to also
    //be seen in the editor, handy.
    [Tooltip("a prefab that we clone several times as children of this object")]
    public PhysicsBuilderPart prefab;

    [Tooltip("How many prefabs to clone")]
    public int count;

    [Tooltip("Offset in local space of this object for positioning each child")]
    public Vector3 offset;


    public float breakingForce;
    public bool fixStart;
    public bool fixEnd;

    [ContextMenu("Build")]
    public void Build()
    {

        // delete any old ones
        ClearBridge();

        if (prefab == null)
            return;

        PhysicsBuilderPart previous = null;

        for (int i = 0; i < count; i++)
        {
            // clone the prefab and make it a child of us
            PhysicsBuilderPart instance = Instantiate(prefab, transform);

            instance.transform.localPosition = i * offset;
            instance.transform.localRotation = prefab.transform.localRotation;
            instance.name = name + "_" + i;

            Rigidbody rb = instance.GetComponent<Rigidbody>();

            rb.isKinematic = ((i == 0 && fixStart) || (i == count - 1 && fixEnd));

            // attach the previous body to this one
            if (previous)
            {
                foreach (Joint joint in previous.forwardJoints)
                    joint.connectedBody = rb;
            }
            previous = instance;
        }
        SetBreakingForce();
    }

    void DestroyObj(Object obj)
    {
        if (Application.isPlaying)
            Destroy(obj);
        else
            DestroyImmediate(obj);
    }

    [ContextMenu("Clear Bridge")]
    public void ClearBridge()
    {
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
            DestroyObj(rb.gameObject);
    }

    [ContextMenu("Set Break Force")]
    public void SetBreakingForce()
    {
        if (breakingForce != 0)
            foreach (Joint joint in GetComponentsInChildren<Joint>())
                joint.breakForce = breakingForce;
    }
}
