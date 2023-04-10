using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    [Header("Booleans")]
    [SerializeField] public bool gameOver = false;
    [SerializeField] public bool gameStarted = false;

    [Header("Ui Objects")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Animation midGameScreen;
    [SerializeField] private Animation startGameScreen;
    [SerializeField] private GameObject deathFlashScreen;
    [SerializeField] private TextMeshProUGUI fpsText;
    [SerializeField] private TextMeshProUGUI velocityText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI recentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI clockText;
    [SerializeField] private TextMeshProUGUI invincibleText;
    [SerializeField] private TextMeshProUGUI clearScoreText;

    [Header("Components")]
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private BoxCollider2D playerBc;

    [Header("Scripts")]
    [SerializeField] private FlappyBird flappyBirdScript;
    [SerializeField] private DayNightCycle dayNightCycleScript;

    public object Playerprefs { get; private set; }

    void Start()
    {
        Application.targetFrameRate = 60;

        startGameScreen.Play("Appear");
    }

    // Update is called once per frame
    void Update()
    {
        UiAnimations();
        DisplayFPS();
        DisplayVelocity();
        DisplayScore();
        SavingScore();
        DisplayClock();
    }

    private void DisplayScore() {
        scoreText.text = flappyBirdScript.score.ToString();
    }

    private void SavingScore() {
        recentScoreText.text = flappyBirdScript.score.ToString();
        highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();

        if (flappyBirdScript.score > PlayerPrefs.GetInt("highScore")) {
            PlayerPrefs.SetInt("highScore", flappyBirdScript.score);
            PlayerPrefs.Save();
        }
    }

    private void UiAnimations() {
        if (gameOver)
        {
            gameOverScreen.SetActive(true);
            deathFlashScreen.SetActive(true);
        }

        // mid and start game animations
        if (gameStarted)
        {
            midGameScreen.Play("Appear");
            startGameScreen.Play("Disappear");
        }

        if (gameOver)
            midGameScreen.Play("Disappear");
    }

    private void DisplayFPS() {
        float fps;

        fps = 1 / Time.deltaTime;

        fpsText.text = "FPS - " + fps.ToString("F0");
    }

    private void DisplayVelocity()
    {
        velocityText.text = "Flappy Bird's Velocity: " + playerRb.velocity.ToString();
    }

    private void DisplayClock() {
        clockText.text = "Time: " + dayNightCycleScript.clock.ToString("F0") + " - AM[-0.1, 30], PM[31, 60]";
    }

    // UI Functions

    public void RestartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ClearScore() {
        PlayerPrefs.SetInt("highScore", 0);
    }

    public void IsInvincible() { 
        if (flappyBirdScript.invincible == false) {
            flappyBirdScript.invincible = true;
            invincibleText.text = "Invincible? " + flappyBirdScript.invincible.ToString();
            playerBc.isTrigger = true;
        }
        else if (flappyBirdScript.invincible == true) {
            flappyBirdScript.invincible = false;
            invincibleText.text = "Invincible? " + flappyBirdScript.invincible.ToString();
            playerBc.isTrigger = false;
        }
    }
}
