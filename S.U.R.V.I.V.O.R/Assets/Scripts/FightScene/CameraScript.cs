using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    private float moveSpeed = 10f;
    private float liftSpeed = 20f;
    private float rotateSpeed = 1f;
    private float rotateZone = 20f;
    private Camera mainCamera;

    private float rotationX = 0;
    private float rotationY = 0;
    void Start()
    {
        mainCamera = GetComponentInChildren<Camera>();
        var body = GetComponent<Rigidbody>();
        body.freezeRotation = true;
    }

    void Update()
    {
        var deltaX = Input.GetAxis("Horizontal") * moveSpeed;
        var deltaZ = Input.GetAxis("Vertical") * moveSpeed;
        var movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, moveSpeed) * Time.deltaTime;
        transform.Translate(new Vector3(movement.x, 0, movement.z));

        
        if (Input.mousePosition.y < rotateZone || Input.mousePosition.y > mainCamera.pixelHeight - rotateZone)
        {
            rotationX -= rotateSpeed * ((Input.mousePosition.y < rotateZone) ? -1 : 1);
            rotationX = Mathf.Clamp(rotationX, -90, 90);
            mainCamera.transform.localEulerAngles = new Vector3(rotationX, 0, 0);
        }
        if (Input.mousePosition.x < rotateZone || Input.mousePosition.x > mainCamera.pixelWidth - rotateZone)
        {
            float delta = rotateSpeed * ((Input.mousePosition.x < rotateZone) ? 1 : -1);
            rotationY -= rotateSpeed * ((Input.mousePosition.x < rotateZone) ? 1 : -1);
        }
        transform.localEulerAngles = new Vector3(0, rotationY, 0);
        transform.Translate(0, liftSpeed * Input.mouseScrollDelta.y * Time.deltaTime,0);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, 0.1f, 20),
        transform.position.z);
    }
}
