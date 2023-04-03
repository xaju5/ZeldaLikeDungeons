using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAEyeStatue : MonoBehaviour
{
    public GameObject player;
    public float maxVisionDistance;

    private float lookAngle;
    private Vector2 heading;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        heading = player.GetComponent<Transform>().position - this.GetComponent<Transform>().position;
        if (heading.sqrMagnitude > maxVisionDistance) return;
        lookAngle = GetAngle(heading);
        animator.SetFloat("angle", lookAngle);
    }

    private static float GetAngle(Vector2 vector2)
    {
        if (vector2.x < 0)
            return 360 - (Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg * -1);
        else
            return Mathf.Atan2(vector2.x, vector2.y) * Mathf.Rad2Deg;
    }
}
