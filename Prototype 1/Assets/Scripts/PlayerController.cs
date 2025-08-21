using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera Settings")]
    public Camera mainCamera;
    public Camera hoodCamera;
    public KeyCode switchKey;

    [Header("PlayerID")]
    public string inputID;

    private float speed = 15f;
    private float turnSpeed = 40f;
    private float horizontalInput;
    private float forwardInput;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the vehicle forward
        horizontalInput = Input.GetAxis("Horizontal" + inputID);
        forwardInput = Input.GetAxis("Vertical" + inputID);

        // Move Vertical
        transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        // Move Horizontal
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        if (Input.GetKeyDown(switchKey))
        {
            mainCamera.enabled = !mainCamera.enabled;
            hoodCamera.enabled = !hoodCamera.enabled;
        }
    }
}
