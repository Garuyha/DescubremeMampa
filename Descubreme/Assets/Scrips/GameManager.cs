using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unansweredQuestions;
    private Question currentQuestion;

    public Button[] answerButtons;
    public Image questionImage, lupitaImage, victoryImage, defeatImage;
    public TextMeshProUGUI questionText;
    public Sprite corretAnswer, incorrectAnswer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip correctSound, incorrectSound;
    private int puntaje, preguntasRealizadas, bandIndexRta, realIndex;

    private void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        GetRandomQuestion();
        puntaje = 0;
        preguntasRealizadas = 0;
    }

    void GetRandomQuestion()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            if (puntaje == 10)
                victoryImage.gameObject.SetActive(true);
            else
                defeatImage.gameObject.SetActive(true);

            return;
        }

        if (preguntasRealizadas < 10)
        {
            preguntasRealizadas++;

            int randomQuestionIndex = UnityEngine.Random.Range(0, unansweredQuestions.Count);
            currentQuestion = unansweredQuestions[randomQuestionIndex];

            GetAnswerInfo();
        }
        else
        {
            if (puntaje >= 6)
                victoryImage.gameObject.SetActive(true);
            else
                defeatImage.gameObject.SetActive(true);
        }
    }

    void GetAnswerInfo()
    {
        // Asignar los textos de las respuestas a los botones de la escena
        questionImage.sprite = currentQuestion.questionImage;
        questionText.text = currentQuestion.questionText;
        lupitaImage.gameObject.SetActive(false);
        questionImage.gameObject.SetActive(true);

        int[] indices = Enumerable.Range(0, currentQuestion.answer.Length).ToArray();

        // Shuffle (Fisher-Yates)
        for (int i = indices.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
            int temp = indices[i];
            indices[i] = indices[j];
            indices[j] = temp;
        }

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (currentQuestion != null && i < currentQuestion.answer.Length)
            {
                int randomIndex = indices[i]; // ← clave
                var answer = currentQuestion.answer[randomIndex];
                if (currentQuestion.answer[randomIndex].isCorrect)
                    bandIndexRta = i;

                var textComp = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();

                if (textComp != null)
                    textComp.text = answer.answersText;

                answerButtons[i].interactable = true;
                answerButtons[i].onClick.RemoveAllListeners();

                int index = randomIndex; // importante usar el random
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));

                answerButtons[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                var textComp = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (textComp != null)
                    textComp.text = string.Empty;

                answerButtons[i].interactable = false;
                answerButtons[i].onClick.RemoveAllListeners();
            }
        }

    }

    // Maneja la selección de una respuesta (puedes ampliar la lógica aquí)
    public void GetRealIntButton0()
    {
        realIndex = 0;
    }
    public void GetRealIntButton1()
    {
        realIndex = 1;
    }
    public void GetRealIntButton2()
    {
        realIndex = 2;
    }
    public void GetRealIntButton3()
    {
        realIndex = 3;
    }
    void OnAnswerSelected(int index)
    {
        if (currentQuestion == null || index < 0 || index >= currentQuestion.answer.Length)
            return;

        bool isCorrect = currentQuestion.answer[index].isCorrect;
        Debug.Log(isCorrect ? "Respuesta correcta" : "Respuesta incorrecta");
        if (isCorrect)
        {
            lupitaImage.sprite = corretAnswer;
            questionText.text = "¡CORRECTO!";
            audioSource.PlayOneShot(correctSound);
            puntaje++;
        }
        else {             
            lupitaImage.sprite = incorrectAnswer;
            questionText.text = "¡INCORRECTO!";
            audioSource.PlayOneShot(incorrectSound);
            answerButtons[realIndex].GetComponent<Image>().color = Color.red;
        }
        for(int i = 0; i < answerButtons.Length; i++)
        {
            if (answerButtons[i] != null)
                answerButtons[i].interactable = false;
        }
        answerButtons[bandIndexRta].GetComponent<Image>().color = Color.green;

        lupitaImage.gameObject.SetActive(true);
        questionImage.gameObject.SetActive(false);
        SetAnswerButtonsInteractable(false);
        StartCoroutine(FeedbackAndNext());
    }

    IEnumerator FeedbackAndNext()
    {
        yield return new WaitForSeconds(2.5f);
        unansweredQuestions.Remove(currentQuestion);
        GetRandomQuestion();

    }
    void SetAnswerButtonsInteractable(bool value)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (answerButtons[i] != null)
            {
                answerButtons[i].interactable = value;
            }
        }
    }
}