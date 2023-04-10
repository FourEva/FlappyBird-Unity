using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Customisable")]
    [SerializeField] public float clock;

    [Header("Components")]
    [SerializeField] private Animator lightCycleAnimator;
    [SerializeField] private GameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        clock = -0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateClock();
        MoveBackground();
    }

    private void UpdateClock()
    {
        if (gameManagerScript.gameStarted == true && gameManagerScript.gameOver == false) clock += Time.deltaTime;

        if (clock < 0f) lightCycleAnimator.SetBool("isNight", false);
        else if (clock > 30f) lightCycleAnimator.SetBool("isNight", true);

        if (clock > 60f)
            clock = -0.1f;
    }

    private void MoveBackground() {
        if (gameManagerScript.gameStarted == false) return;
        if (gameManagerScript.gameOver == true) return;

        transform.Translate(Vector2.left * Time.deltaTime * 0.5f);

        if (transform.position.x < -2.83f)
            transform.position = new Vector2(2.929121f, -0.2680094f);
    }
}
