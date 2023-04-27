using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movementVector;
    private float movementAngle;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        movementVector.x = Input.GetAxisRaw("Horizontal");
        movementVector.y = Input.GetAxisRaw("Vertical");
        if(Input.anyKey)
            movementAngle = GetAngle(movementVector);

        animator.SetFloat("horizontalMov", movementVector.x);
        animator.SetFloat("verticalMov", movementVector.y);
        animator.SetFloat("speed", movementVector.sqrMagnitude);
        animator.SetFloat("angleMov", movementAngle);
    }

    private void FixedUpdate()
    {
        //Movement
        Move();
    }



    private void Move()
    {
        rb.MovePosition(rb.position + movementVector * speed * Time.fixedDeltaTime);
    }

    private static float GetAngle(Vector2 vector2)
    {
        if (vector2.x < 0)
            return 360 - (Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg * -1);
        else
            return Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg;
    }
    
}
