using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] QuestionSO question;
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] GameObject[] answersBtn;

    private int correctAnswerIndex;

    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    Image btnImage;
    void Start()
    {
        DisplayQuestion();
    }

    public void OnAnswerSelected(int index)
    {

        if(index == question.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!";
            btnImage = answersBtn[index].GetComponent<Image>();
            btnImage.sprite = correctAnswerSprite;
        }
        else
        {
            correctAnswerIndex = question.GetCorrectAnswerIndex();
            string correctAnswer = question.GetAnswer(correctAnswerIndex);
            questionText.text = "The correct answer was: \n" + correctAnswer;
            btnImage = answersBtn[correctAnswerIndex].GetComponent<Image>();
            btnImage.sprite = correctAnswerSprite;
        }

        SetButtonState(false);
    }

    void GetNextQuestion()
    {
        SetButtonState(true);
        DisplayQuestion();
        SetDefaultButtonSprites();
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
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answersBtn.Length; i++)
        {
            TextMeshProUGUI buttonText = answersBtn[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
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
