using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PipeSpawnerUnused : MonoBehaviour
{
    [Header("Customisable")]
    [SerializeField] private float timeBeforeSpawn = 2f;
    [SerializeField] private float timeSpawnLoop;

    [Header("GameObjects")]
    [SerializeField] private GameObject pipePrefab;

    [Header("Scripts")]
    [SerializeField] private GameManager gameManagerScript;

    private void Start()
    {
        InvokeRepeating("InstantiatePipe", timeBeforeSpawn, timeSpawnLoop);
    }

    private void InstantiatePipe() {

        if (gameManagerScript.gameOver == false && gameManagerScript.gameStarted == true) {
            float randomPipeYPos = Random.Range(-1.31f, 2.31f);

            Vector2 newPipePosition = new Vector2(6, randomPipeYPos);

            Instantiate(pipePrefab, newPipePosition, pipePrefab.transform.rotation);
        }

    }
}
