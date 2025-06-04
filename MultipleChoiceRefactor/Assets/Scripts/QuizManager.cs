using UnityEngine;

using UnityEngine;
using UnityEngine.UI;
using TMPro; // If using TextMeshPro
using System.Runtime.InteropServices;

public class QuizManager : MonoBehaviour
{
    public TextMeshPro questionText; // Assign in inspector
    public GameObject[] answerCubes;     // Assign 4 cubes in inspector
    public QuizQuestion[] questions;

    private int currentQuestionIndex = 0;

    private int score = 0;
    private bool quizFinished = false;

    [DllImport("__Internal")]
    private static extern void SendScoreToBackEnd(int score, string gameName);


    void Start()
    {
        questions = new QuizQuestion[]
        {
            new QuizQuestion
            {
                question = "Which ocean is the largest in the world?",
                answers = new string[] { "Pacific Ocean", "Indian Ocean", "Atlantic Ocean", "Arctic Ocean" },
                correctAnswerIndex = 0
            },
            new QuizQuestion
            {
                question = "What is the main ingredient in guacamole?",
                answers = new string[] { "Tomato","Avocado","Cucumber","Zucchini" },
                correctAnswerIndex = 1
            },
            new QuizQuestion
            {
                question = "How many continents are there on Earth?",
                answers = new string[] { "5", "6", "7", "8" },
                correctAnswerIndex = 2
            },
            new QuizQuestion
            {
                question = "What year did the Berlin Wall fall?",
                answers = new string[] { "1979", "1985", "1989", "1991" },
                correctAnswerIndex = 2
            },
            new QuizQuestion
            {
                question = "Who painted 'The Scream'?",
                answers = new string[] { "Edvard Munch", "Gustav Klimt", "Pablo Picasso", "Salvador Dali" },
                correctAnswerIndex = 0
            }
        };
        DisplayQuestion();
    }

    public void DisplayQuestion()
    {
        if (currentQuestionIndex >= questions.Length)
        {
            Debug.Log("Quiz Finished!");
            return;
        }

        QuizQuestion q = questions[currentQuestionIndex];
        questionText.text = q.question;

        for (int i = 0; i < 4; i++)
        {
            answerCubes[i].GetComponentInChildren<TextMeshPro>().text = q.answers[i];

            WallHit wallScript = answerCubes[i].GetComponent<WallHit>();
            if (wallScript != null)
            {
                wallScript.answerIndex = i;
                wallScript.quizManager = this;
            }
        }

    }

    public void CheckAnswer(int index)
    {
        QuizQuestion q = questions[currentQuestionIndex];

        if (index == q.correctAnswerIndex)
        {
            Debug.Log("Correct!");
            score++;
        }
        else
        {
            Debug.Log("Wrong!");
        }

        currentQuestionIndex++;
        if (currentQuestionIndex >= questions.Length)
        {
            quizFinished = true;
            ShowFinalScore();
        }
        else
        {
            Invoke("DisplayQuestion", 1f); // short delay before next question
        }
    }

    public void ShowFinalScore()
    {
        string resultText = $"You got {score}/{questions.Length} correct!";
        Debug.Log(resultText);

        if (questionText != null)
        {
            questionText.text = resultText;
        }

        var trueScore = score * 20;
        SendScoreToBackEnd(trueScore, "MultipleChoice");

        // You could also trigger some animation or sound here
    }

}


[System.Serializable]
public class QuizQuestion
{
    public string question;
    public string[] answers; // Length should be 4
    public int correctAnswerIndex; // 0 to 3
}