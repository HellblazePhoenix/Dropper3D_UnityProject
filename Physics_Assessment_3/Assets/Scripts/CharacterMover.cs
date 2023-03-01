using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    public float speed = 10;
    public float jumpHeight = 12;
    public float gravity;
    public Vector3 velocity;

   // public GameObject projectilePrefab;

    CharacterController cc;
    Animator animator;
    Transform cam;
    Vector2 moveInput = new Vector2();
    public bool jumpInput;
    public bool isGrounded = true;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        cam = Camera.main.transform;
        gravity = -Physics.gravity.y;
    }

    void Update()
    {
        // you can't jump while moving left + up because key jamming ignores the spacebar.
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        jumpInput = Input.GetButton("Jump");

        animator.SetFloat("Forwards", moveInput.y);
        animator.SetBool("Jump", !isGrounded);
    }

    void FixedUpdate()
    {
        Vector3 delta;

        // player movement using WASD or arrow keys

        // find the horizontal unit vector facing forward from the camera
        Vector3 camForward = cam.forward;
        camForward.y = 0;
        camForward.Normalize();

        // use our camera's right vector, which is always horizontal
        Vector3 camRight = cam.right;

        delta = (moveInput.x * camRight + moveInput.y * camForward) * speed;

        if (isGrounded || moveInput.x != 0 || moveInput.y != 0)
        {
            velocity.x = delta.x;
            velocity.z = delta.z;
        }

        // rotate the render component according to the camera in turn affecting beta's rotation.
        transform.forward = camForward;

        // check for jumping
        if (jumpInput && isGrounded)
            velocity.y = Mathf.Sqrt(2 * jumpHeight * gravity);

        // check if we've hit ground from falling. If so, remove our velocity
        if (isGrounded && velocity.y < 0)
            velocity.y = 0;

        // apply gravity I was wondering why this wasn't after zeroing 
        velocity += Physics.gravity * Time.fixedDeltaTime;


        // and apply this to our positional update this frame
        delta += velocity * Time.fixedDeltaTime;
        // I don't see how this is better the code already had air control due to not restricting horizontal movement all that has been 
        //done is to shift the values in delta into velocity every fixed frame.
        cc.Move(velocity * Time.deltaTime);
        isGrounded = cc.isGrounded;
    }
}
