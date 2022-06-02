using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//making sure this script shows in the "create" dropdown of Unity
//menu name is that the script is called in the dropdown. File name is the name the object takes in the project window
[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    //adjust and control the size of your text box. Numbers are the min and max numbers you want shown
    [TextArea(2,6)]
    //not making it public since it affects ALL the other Scriptable Objects but we still want to make changes to the var
    [SerializeField] string question = "Enter  new question text here.";

    //array of answers
    [SerializeField] string[] answers = new string[4];

    //index number for our answer
    [SerializeField] int rightAnswerIndex;

    //getting read access to the question variable since it's private
    public string GetQuestion(){
        return question;
    }

    //returning the answer the player chose at that index
     public string GetAnswer(int index){
        return answers[index];
    }

    //registering the right answer
    public int GetCorrectAnswerIndex(){
        return rightAnswerIndex;
    }

   
}
