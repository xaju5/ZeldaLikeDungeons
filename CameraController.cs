using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed;

    private Vector3 targetPos, newPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
    }

    // Update is called once per frame after the rest of Update funtions are called
    void LateUpdate()
    {
        if (transform.position != targetPos) MoveCamera();
    }

    private void MoveCamera()
    {
        newPos = Vector3.Lerp(transform.position, targetPos, smoothSpeed);
        transform.position = newPos;
    }

    public void SetTargetPosition(Vector3 target)
    {
        targetPos += target;
    }
}
