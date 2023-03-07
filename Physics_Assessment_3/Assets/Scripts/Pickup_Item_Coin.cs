using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:
// 1. Add an OnCollision event trigger for the player that brings up a win screen with the options to play again or quit.

public class Pickup_Item_Coin : MonoBehaviour
{
    Transform spin;


    public float spinSpeed = 60.0f;
    // Start is called before the first frame update
    void Start()
    {
        spin = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
        spin.rotation = Quaternion.AngleAxis(Time.realtimeSinceStartup * spinSpeed, Vector3.up);
    }
}
