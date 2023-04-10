using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForeground : MonoBehaviour
{
    [Header("Customisable")]
    [SerializeField] private float speed = 2f;

    [Header("Scripts")]
    [SerializeField] private GameManager gameManagerScript;

    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.gameOver == false && gameManagerScript.gameStarted == true) {
            transform.Translate(Vector2.left * Time.deltaTime * speed);

            if (transform.position.x < -7.4f)
                transform.position = startPos;
        }
    }
}
