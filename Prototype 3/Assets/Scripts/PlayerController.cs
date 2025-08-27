using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;

    [Header("ParticleSystem Settings")]
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtyParticle;

    [Header("SFX Settings")]
    public AudioClip jumpSFX;
    public AudioClip crashSFX;

    [Header("Other Settings")]
    public float jumpForce = 500;
    public float gravityModifier;
    private bool isOnGround = true;
    public bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.volume = 0.2f;
            playerAudio.PlayOneShot(jumpSFX, 1.0f);
            isOnGround = false;
            dirtyParticle.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!gameOver)
            {
                isOnGround = true;
                dirtyParticle.Play();
            }
                
        }else if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!gameOver)
            {
                Debug.Log("GAME OVER");
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                explosionParticle.Play();
                playerAudio.volume = 0.8f;
                playerAudio.PlayOneShot(crashSFX, 1.0f);
                dirtyParticle.Stop();
                gameOver = true;
            }      
        }
        
    }
}
