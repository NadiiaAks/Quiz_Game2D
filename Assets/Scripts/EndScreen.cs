using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    Score scoreKeeper;
    void Start()
    {
        scoreKeeper =FindObjectOfType<Score>();
    }

    public void ShowFinalScreen()
    {
        finalScoreText.text = "Congratulations!\nYou got a score of " + scoreKeeper.CalculateScore() + "%";
    }

}
