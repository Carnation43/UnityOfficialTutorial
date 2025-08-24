using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float horizontalInput;
    public float verticalInput;
    public float speed;
    public float xRange;
    public float zTop;
    public float zBottom;

    [Header("Projectile Settings")]
    public GameObject projectile;
    private float cooldown = 0.5f;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // keep the player in bounds
        if(transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if(transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.z > zTop)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zTop);
        }
        if (transform.position.z < zBottom)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBottom);
        }
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * speed * Time.deltaTime * horizontalInput, Space.Self);
        transform.Translate(Vector3.forward * speed * Time.deltaTime * verticalInput * 0.5f, Space.Self);

        
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && timer > cooldown)
        {
            Instantiate(projectile, transform.position, transform.rotation);
            Debug.Log("Bullets Generation");
            timer = 0;
        }
    }
}
