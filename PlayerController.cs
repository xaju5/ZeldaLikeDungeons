using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 movement;
    private float angle;

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
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if(Input.anyKey)
            angle = getAngle(movement);

        animator.SetFloat("horizontalMov", movement.x);
        animator.SetFloat("verticalMov", movement.y);
        animator.SetFloat("speed", movement.sqrMagnitude);
        animator.SetFloat("angleMov", angle);
    }

    private void FixedUpdate()
    {
        //Movement
        move();
    }



    private void move()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private static float getAngle(Vector2 vector2)
    {
        if (vector2.x < 0)
            return 360 - (Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg * -1);
        else
            return Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg;
    }
    
}
