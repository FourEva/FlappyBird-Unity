using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipesLogic : MonoBehaviour
{
    [Header("Customisable")]
    [SerializeField] private float speed = 2f;

    [Header("Scripts")]
    [SerializeField] private GameManager gameManagerScript;

    private void Awake()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManager>();

        transform.position = new Vector2(transform.position.x, Random.Range(-1.31f, 2.31f));
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManagerScript.gameOver == false && gameManagerScript.gameStarted == true) {
            transform.Translate(Vector2.left * Time.deltaTime * speed);

            if (transform.position.x < -6)
                NewPosition();
        }
    }

    private void NewPosition()
    {

        if (gameManagerScript.gameOver == false && gameManagerScript.gameStarted == true)
        {
            float randomPipeYPos = Random.Range(-1.31f, 2.31f);

            Vector2 newPipePosition = new Vector2(6, randomPipeYPos);

            transform.position = newPipePosition;
        }

    }
}
