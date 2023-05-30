using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Moving_Platform : MonoBehaviour
{

    Vector3 startDif = Vector3.zero;
    [Tooltip("Do not modify this after the platform starts moving as it will throw the platform out of sync with it's anchor point/starting position.")]
    public Vector3 direction;
    public float speed;
    public Vector3 velocity;
    public float distance;
    public Rigidbody rb;

    public bool move;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        velocity = direction * speed;
        //rb.velocity = velocity;
    }
    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            transform.Translate(velocity * Time.deltaTime);
            startDif += (velocity * Time.deltaTime);
            if (Vector3.Distance(startDif, Vector3.zero) > distance)
            {
                velocity = -velocity;
                //rb.velocity = velocity;
            }


        }
    }

}
