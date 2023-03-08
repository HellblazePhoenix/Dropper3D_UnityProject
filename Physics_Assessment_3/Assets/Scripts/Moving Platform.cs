using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 start; // start point
    public float distance; // distance platform travels away from start point before returning
    [Tooltip("This should be a unit vector")]
    public Vector3 direction; // direction the platform travels away from the start
    public float speed; // speed of travel

    public bool move; // whether the object should move or not


    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (move)
            if (Vector3.Distance(start, transform.position) < distance)
                transform.position += direction * speed * Time.deltaTime;
            else
                transform.position -= direction * speed * Time.deltaTime;
    }
}
