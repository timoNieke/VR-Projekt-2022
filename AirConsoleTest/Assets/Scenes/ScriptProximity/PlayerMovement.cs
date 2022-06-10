using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;
    public float speed = 12f;

    //Working on Gravity
    Vector3 velocity;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //das erlaubt uns, dass der Spieler nicht immer in richtung des globalen links läuft wenn wir "d" drücken, 
        //sondern der Spieler wird abhängig von seiner Blickrichtung nach links und rechts bewegt.
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * Time.deltaTime * speed);

        //Gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);



    }
}
