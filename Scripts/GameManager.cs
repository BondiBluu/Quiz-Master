using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    //transition between quiz and win canvases
    Quiz quiz;
    EndScreen endScreen;

    void Awake(){
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();
    }
    void Start()
    {
        //keeping the quiz on and end screen off in the beginning
        quiz.gameObject.SetActive(true);
        endScreen.gameObject.SetActive(false); 
    }

    void Update()
    {
        //toggling the states of the 2 canvases when the game end
        if(quiz.isComplete == true){
        quiz.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(true); 
        endScreen.ShowFinalScore();
        }
    }

    //reloading the scene if player wants to play again
    public void OnReplayLevel(){
        //essentially loading the quiz screen again
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
