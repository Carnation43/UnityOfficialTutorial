using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyPad : MonoBehaviour
{
    [Header("Boosting")]
    public bool normalBoosting = true;
    public Vector3 boostDirection;
    public float boostForce;

    private PlayerMovement pm = null;

    private void OnTriggerEnter(Collider other)
    {
        AddForce(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        AddForce(collision.collider);
    }

    private void AddForce(Collider other)
    {
        if (other.GetComponentInParent<PlayerMovement>() != null)
        {
            pm = other.GetComponentInParent<PlayerMovement>();

            Rigidbody rb = pm.GetComponent<Rigidbody>();

            if (normalBoosting)
                rb.AddForce(boostDirection.normalized * boostForce, ForceMode.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!normalBoosting) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + boostDirection);
    }
}
