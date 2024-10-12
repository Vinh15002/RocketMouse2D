using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    [SerializeField]
    private float jetpackForce = 15f;
    [SerializeField]
    private float movementSpeed = 3f;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        OnJump();
        Vector2 move = new Vector2(movementSpeed, rigidbody.velocity.y);
        rigidbody.velocity = move;

    }

    private void Movement()
    {

    }

    private void OnJump()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            rigidbody.velocity = new Vector2(movementSpeed, jetpackForce);
        }
    }
}
