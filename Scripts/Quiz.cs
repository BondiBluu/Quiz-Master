using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    //referencing the QuestionSO script
    QuestionSO currentQuestion;

    //reference to the question
    [SerializeField]  TextMeshProUGUI questionText;

    //list of questions
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();

    [Header("Answers")]
    //stores buttons
    [SerializeField] GameObject[] answerButtons;

    //reference to the correct answer index of our question
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;
    
    [Header("Button Colors")]
    //button sprites
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer; //reference to Timer class

    [Header("Scoring")]
    
    [SerializeField] TextMeshProUGUI scoreText;

    ScoreKeeper scoreKeeper;

    [Header("Slider")]

    [SerializeField] Slider progressBar;

    //seeing if the quiz is still in progress or complete
    public bool isComplete;
    

    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update(){
        //change the fill amount of the timer image every frame
        timerImage.fillAmount = timer.fillFraction;

        //check state of the time to see if we need to change the question
        if(timer.loadNextQuestion){

            if(progressBar.value == progressBar.maxValue){
            isComplete = true;
            return;
        }
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } else if (!hasAnsweredEarly && !timer.isAnsweringQuestion) //essentially if time runs out
        {
            DisplyAnswer(-1); //-1 auto chooses the wrong anser to display the right one
            SetButtonState(false);
        }
    }
    //method to connect button presses, using index to see which button index was clicked
    public void OnAnswerSelected(int index){
        hasAnsweredEarly = true;
        DisplyAnswer(index);
        SetButtonState(false); //disabling the button at this stage
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }

    void DisplyAnswer(int index){
        Image buttonImage;
        //if you chose  the right answer
         if(index == currentQuestion.GetCorrectAnswerIndex()){
             questionText.text = "Correct.";
             //making an image button and linking it to our button gameobject
            buttonImage  = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
         } else{
             correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex(); //getting the right answer
             string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
             questionText.text = "Incorrect- the right answer was; \n" + correctAnswer;

             buttonImage  = answerButtons[correctAnswerIndex].GetComponent<Image>();
             buttonImage.sprite = correctAnswerSprite;
         }
    }

    void GetNextQuestion(){
        if(questions.Count >0){
            //setting the interactibility with the button to true  when you get a new question
            SetButtonState(true);
            SetDefaultSprite();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion(){
        //basically making a rando # genner, making the current question that numver, and removing the question when picked
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion)){
            questions.Remove(currentQuestion);
        }
    }

    void DisplayQuestion(){
        //returning the question from QuestionSO script
        questionText.text = currentQuestion.GetQuestion();

        for(int i = 0; i < answerButtons.Length; i++){
        //taking the TextMeshPro and equating it to the answer button object, getting the children of the answer button- the text
        TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        //button's text equals Serializable object QuestionSO's answer
        buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    //setting the buttons as interactable or not  so you won't click them twice
        void SetButtonState(bool state){
            for(int i = 0; i < answerButtons.Length; i++){
                //referencing the Button component on the answer button gameobject
                Button button = answerButtons[i].GetComponent<Button>();
                button.interactable = state; //setting it equal to the state we're passing into the method (sets it to either true or false)
            }
        }

        //resetting button sprites back to default for the next question
        void SetDefaultSprite(){
            for(int i = 0; i < answerButtons.Length; i++){
                Image buttonImage  = answerButtons[i].GetComponent<Image>();
                buttonImage.sprite = defaultAnswerSprite;
            }
        }
}
