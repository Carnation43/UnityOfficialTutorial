using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    public float maxPositionY = 14;
    private float gravityModifier = 1.0f;
    private Rigidbody playerRb;
    private bool isLowEnough = true;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > maxPositionY)
        {
            playerRb.velocity = new Vector3(playerRb.velocity.x, Mathf.Lerp(playerRb.velocity.y, -1.0f, 0.1f), playerRb.velocity.z);
            if(transform.position.y > maxPositionY + 1)
            {
                transform.position = new Vector3(transform.position.x, maxPositionY + 1, transform.position.z);
            }
            
        }
        else { isLowEnough = true; }
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && isLowEnough)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }

       
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }

        if (other.gameObject.CompareTag("Ground"))
        {
            playerRb.AddForce(Vector3.up * 8, ForceMode.Impulse);
        }
    }

}
