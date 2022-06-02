using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    public bool isAnsweringQuestion;

    public bool loadNextQuestion;
    public float fillFraction;

    //set to one of the above floats and will slowly tick down
    float timerValue;
    
    void Update()
    {
        UpdateTimer();
    }

    //cancelling timer when the play answer a question
    public void CancelTimer(){
        timerValue = 0;
    }
    void UpdateTimer(){
        //reducing the time we've got in our timer value
        timerValue -= Time.deltaTime;

        //check if we're asking a ques
        if(isAnsweringQuestion){

            //if we still have time
            if(timerValue > 0){
                fillFraction = timerValue / timeToCompleteQuestion; 
            } else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }

        } else
        {
            //check if we're showing answer
            if(timerValue > 0){
                fillFraction = timerValue / timeToShowCorrectAnswer;
            } else
            {
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
