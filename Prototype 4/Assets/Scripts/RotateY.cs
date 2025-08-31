using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateY : MonoBehaviour
{
    public bool upDownEnable;

    private float startY;
    // Start is called before the first frame update
    void Start()
    {
        startY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (upDownEnable)
        {
            float y = Mathf.Sin(Time.time) * 0.25f;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        
        transform.Rotate(Vector3.up * 90.0f * Time.deltaTime);
    }
}
