using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyBird : MonoBehaviour
{
    [Header("Customisable")]
    [SerializeField] private float smoothingTime = 10f;
    [SerializeField] private float flapHeight = 10f;
    [SerializeField] public int score = 0;
    [SerializeField] public bool invincible = false;
    private Touch touch;

    [Header("Rotations")]
    private Quaternion birdFalling = Quaternion.Euler(0f, 0f, -35f);
    private Quaternion birdDie = Quaternion.Euler(0f, 0f, -90f);
    private Quaternion birdFlying = Quaternion.Euler(0f, 0f, 35f);

    [Header("SFX")]
    [SerializeField] private AudioSource playerSFXPlayer;
    [SerializeField] private AudioClip getScore;
    [SerializeField] private AudioClip flap;
    [SerializeField] private AudioClip crash;
    [SerializeField] private AudioClip over;
    private bool hasPlayedSound = false;

    [Header("Components")]
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private BoxCollider2D playerBoxCollider;
    [SerializeField] private Animator playerAnimator;

    [Header("Scripts")]
    [SerializeField] private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);

        Physics.gravity = new Vector2(0f, -20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.gameOver == false && gameManagerScript.gameStarted == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1") || Input.touchCount > 0)
            {
                playerRb.constraints = RigidbodyConstraints2D.None;
                playerRb.bodyType = RigidbodyType2D.Dynamic;
                gameManagerScript.gameStarted = true;
                playerAnimator.SetBool("gameStarted", true);
            }
        }

        if (gameManagerScript.gameOver == false && gameManagerScript.gameStarted == true)
        {
            JumpInput();
        }
        
        BirdRotation();

        if (gameManagerScript.gameStarted == false)
            FreezeBird();
    }

    private void JumpInput() {
        if (Input.touchCount > 0)
            touch = Input.GetTouch(0);

        if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1") || Input.touchCount > 0) {
            // Mobile
            if (Input.touchCount > 0 && touch.phase == TouchPhase.Began) {
                playerRb.velocity = Vector2.zero;
                playerRb.velocity = new Vector2(0f, flapHeight);
                playerSFXPlayer.PlayOneShot(flap);
            }

            if (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")) {
                playerRb.velocity = Vector2.zero;
                playerRb.velocity = new Vector2(0f, flapHeight);
                playerSFXPlayer.PlayOneShot(flap);
            }
        }

        playerRb.velocity = Vector2.ClampMagnitude(playerRb.velocity, 7.5f);
    }

    private void BirdRotation() {
        if (gameManagerScript.gameOver == false && gameManagerScript.gameStarted == true) {
            if (playerRb.velocity.y > 0.5)
                transform.rotation = Quaternion.Slerp(transform.rotation, birdFlying, smoothingTime * Time.deltaTime);
            else if (playerRb.velocity.y < -0.5f && playerRb.velocity.y > -7.5f)
                transform.rotation = Quaternion.Slerp(transform.rotation, birdFalling, smoothingTime * Time.deltaTime);
            else if (playerRb.velocity.y < -5f)
                transform.rotation = Quaternion.Slerp(transform.rotation, birdDie, smoothingTime * Time.deltaTime);
        }

        if (gameManagerScript.gameOver == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, birdDie, smoothingTime * Time.deltaTime);
        }

        // quick solution to locking the birds x position
        transform.position = new Vector2(-1.49f, transform.position.y);
    }

    private void FreezeBird() {
        if (invincible) return;

        playerRb.constraints = RigidbodyConstraints2D.FreezePosition;
        playerRb.constraints = RigidbodyConstraints2D.FreezeRotation;
        playerRb.bodyType = RigidbodyType2D.Static;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Roof")) {

            if (!hasPlayedSound && !invincible)
            {
                gameManagerScript.gameOver = true;

                playerSFXPlayer.PlayOneShot(crash);
                playerSFXPlayer.PlayOneShot(over);

                hasPlayedSound = true;

                playerAnimator.SetBool("gameStarted", false);
                playerAnimator.SetBool("gameEnded", true);
            }
        }

        if (collision.gameObject.CompareTag("Ground") && !invincible)
            FreezeBird();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Score") && gameManagerScript.gameOver == false)
        {
            score += 1;
            playerSFXPlayer.PlayOneShot(getScore);
        }
    }
}
