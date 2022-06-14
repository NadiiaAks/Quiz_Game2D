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
    void Start()
    {
        questionText.text = question.GetQuestion();

        for (int i = 0; i < answersBtn.Length; i++)
        {
            TextMeshProUGUI buttonText = answersBtn[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);
        }

    }

    public void OnAnswerSelected(int index)
    {
        Image btnImage;

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
    }

}
