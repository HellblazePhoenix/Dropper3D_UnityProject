using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollTrigger : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        CharacterMover ragdoll = other.gameObject.GetComponentInParent<CharacterMover>();
        if (ragdoll != null)
            ragdoll.ragdollActive = true;
    }
}
