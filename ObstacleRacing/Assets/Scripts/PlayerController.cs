using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody playerRb;
    [SerializeField] float jumpForce;
    [SerializeField] float gravityModifier;
    private AudioSource playerAudio;
    public bool isGrounded;
    public bool gameOver;
    private Animator playerAnim;
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] ParticleSystem dirtParticle;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip crashSound;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !gameOver)
        {
            playerRb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            playerAnim.SetTrigger("Jump_trig");
            playerAudio.PlayOneShot(jumpSound, 0.6f);
            dirtParticle.Stop();
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
            
        if (collision.gameObject.CompareTag("Ground")){
            isGrounded = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameOver = true;
            Debug.Log("Game over!");
            explosionParticle.Play();
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            playerAudio.PlayOneShot(crashSound, 0.6f);
            dirtParticle.Stop();
        }
    }
}
