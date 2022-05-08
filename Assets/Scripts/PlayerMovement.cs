using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] GameObject stunnedVisuals;
    public CharacterController player;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 4f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    Vector3 velocity;
    bool isGrounded;
    bool isStunned;

    public bool getGrounded()
    {
        return isGrounded;
    }

    public void setStunned(bool set)
    {
        isStunned = set;
    }


    // Update is called once per frame
    void Update()
    {
        if(isStunned)
        {
            stunnedVisuals.SetActive(true);
        }
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -1f;
        }

        if(isStunned == false)
        {
            stunnedVisuals.SetActive(false);
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            player.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }

            velocity.y += gravity * Time.deltaTime;

            player.Move(velocity * Time.deltaTime);
        }
    }

}
