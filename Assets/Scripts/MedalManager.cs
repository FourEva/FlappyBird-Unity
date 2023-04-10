using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedalManager : MonoBehaviour
{
    [Header("Medals")]
    [SerializeField] private RawImage medalRenderer;
    [SerializeField] private Texture none;
    [SerializeField] private Texture bronze;
    [SerializeField] private Texture silver;
    [SerializeField] private Texture gold;
    [SerializeField] private Texture platinum;

    [Header("Scripts")]
    [SerializeField] private FlappyBird flappyBirdScript;

    // Update is called once per frame
    void Update()
    {
        SelectMedal();
    }

    private void SelectMedal() {
        switch (flappyBirdScript.score) {
            case 0:
                medalRenderer.texture = none; break;
            case 10:
                medalRenderer.texture = bronze; break;
            case 20:
                medalRenderer.texture = silver; break;
            case 30:
                medalRenderer.texture = gold; break;
            case 40:
                medalRenderer.texture = platinum; break;
        }
    }
}
