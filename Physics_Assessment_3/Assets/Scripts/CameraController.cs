using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO:
// 1. Polish the camera control fix the issue of being unable to look up
// 2. allow the camera to ignore the player hit boxes. Achieved by using Unity's layers system to only raycast on the default layer

public class CameraController : MonoBehaviour
{
    public Transform target;

    Camera cam;
    public float force = 1000;
    public float speed = 1000;
    public float distance = 10;
    public float heightOffset;
    public float currentDistance;
    public float cDistToDistSpeed = 2;
    int layerMaskRagdolls;
    int layerMaskDefault;

    private void Start()
    {
        cam = GetComponent<Camera>();
        string[] layers1 = { "Ragdoll" };
        layerMaskRagdolls = LayerMask.GetMask(layers1);
        string[] layers2 = { "Default" };
        layerMaskDefault = LayerMask.GetMask(layers2);
    }

    // Update is called once per frame
    void Update()
    {
                // right drag rotates the camera
        if (Input.GetMouseButton(1))
        {
            Vector3 angles = transform.eulerAngles;
            float dx = Input.GetAxis("Mouse Y");
            float dy = Input.GetAxis("Mouse X");
            // look up and down by rotating around X-axis
            angles.x = Mathf.Clamp(angles.x + dx * speed * Time.deltaTime, 0, 70);
            // spin the camera round
            angles.y += dy * speed * Time.deltaTime;
            transform.eulerAngles = angles;
        } 
        
        
        RaycastHit hit;
        if (Physics.Raycast(GetTargetPosition(), -transform.forward, out hit, distance, layerMaskDefault))
        {
            // snap the camera right in to where the collision happenedl
            currentDistance = hit.distance;
        }
        else
        {
            // relax the camera back to the desired distance
            currentDistance = Mathf.MoveTowards(currentDistance, distance, cDistToDistSpeed * Time.deltaTime);
        }


        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitRag;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitRag, Mathf.Infinity, layerMaskRagdolls))
            {
                CharacterMover ragdoll = hitRag.collider.GetComponentInParent<CharacterMover>();
                if (ragdoll)
                    ragdoll.ragdollActive = true;

                Rigidbody body = hitRag.collider.GetComponent<Rigidbody>();
                if (body)
                    body.AddForce(ray.direction * force);
            }
        }

        // look at the target point
        transform.position = GetTargetPosition() - currentDistance * transform.forward;
    }    
    
    Vector3 GetTargetPosition()
    {
        return target.position + heightOffset * Vector3.up;
    }
}
