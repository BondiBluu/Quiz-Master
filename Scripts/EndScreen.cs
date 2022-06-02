using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    //updates final score when test is done
    public void ShowFinalScore(){
        finalScoreText.text = "Congratulations!\n You got a score of " + 
                                    scoreKeeper.CalculateScore() + "!";
        
    }
}