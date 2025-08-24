using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;

    public float cooldown = 2f;
    private float timer;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && timer >= cooldown)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);

            timer = 0f;
        }
    }
}
