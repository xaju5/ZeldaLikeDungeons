using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    public Vector3 newCameraPos, newPlayerPos;

    CameraController cameraController;
    
    // Start is called before the first frame update
    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player") return;

        cameraController.SetTargetPosition(newCameraPos);
        collision.transform.position += newPlayerPos;
    }
}
