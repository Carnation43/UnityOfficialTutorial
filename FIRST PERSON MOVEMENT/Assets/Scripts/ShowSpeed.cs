using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowSpeed : MonoBehaviour
{
    public Transform player;

    private Rigidbody rb;
    private TextMeshProUGUI speedText;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
        speedText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        float realSpeed = flatVelocity.magnitude;
        speedText.text = "Speed: " + realSpeed.ToString("F1");
    }
}
