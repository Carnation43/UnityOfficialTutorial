using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed = 80;
    public float mouseX;
    public float currentRotationSpeed;

    private float rotationAcc = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        currentRotationSpeed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            mouseX = Input.GetAxis("Mouse X");
            currentRotationSpeed = mouseX * rotationSpeed;
            
        }
    
        if(currentRotationSpeed != 0)
        {
            currentRotationSpeed = Mathf.Lerp(currentRotationSpeed, 0, rotationAcc * Time.deltaTime);
            if(Mathf.Abs(currentRotationSpeed) < 0.5f)
            {
                currentRotationSpeed = 0;
            }
        }

        if(currentRotationSpeed != 0)
        {
            transform.Rotate(Vector3.up, currentRotationSpeed * Time.deltaTime);
        }
    }
}
