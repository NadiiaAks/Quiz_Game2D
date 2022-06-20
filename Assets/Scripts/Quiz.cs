using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] TextMeshProUGUI questionText;

    [Header("Answers")]
    [SerializeField] GameObject[] answersBtn;
    private int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Buttons Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    Image btnImage;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    Score scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<Score>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    private void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";

        if(progressBar.value == progressBar.maxValue)
        {
            isComplete = true;
        }
    }

    void DisplayAnswer(int index)
    {

        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            btnImage = answersBtn[index].GetComponent<Image>();
            btnImage.sprite = correctAnswerSprite;
            scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "The correct answer was: \n" + correctAnswer;
            btnImage = answersBtn[correctAnswerIndex].GetComponent<Image>();
            btnImage.sprite = correctAnswerSprite;
        }
    }

    void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementQuestionSeen();
        }

    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }

    }

    void SetDefaultButtonSprites()
    {
        for (int i = 0; i < answersBtn.Length; i++)
        {
            btnImage = answersBtn[i].GetComponent<Image>();
            btnImage.sprite = defaultAnswerSprite;
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for (int i = 0; i < answersBtn.Length; i++)
        {
            TextMeshProUGUI buttonText = answersBtn[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void SetButtonState(bool state)
    {
        for (int i = 0; i < answersBtn.Length; i++)
        {
            Button btn = answersBtn[i].GetComponent<Button>();
            btn.interactable = state;
        }
    }
}
