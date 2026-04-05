using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unansweredQuestions;
    private Question currentQuestion;

    public Button[] answerButtons;
    public Image questionImage, lupitaImage;
    public TextMeshProUGUI questionText;
    public Sprite corretAnswer, incorrectAnswer;

    private void Start()
    {
        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            unansweredQuestions = questions.ToList<Question>();
        }

        GetRandomQuestion();
    }

    void GetRandomQuestion()
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];
        GetAnswerInfo();
    }

    void GetAnswerInfo()
    {
        // Asignar los textos de las respuestas a los botones de la escena
        questionImage.sprite = currentQuestion.questionImage;
        questionText.text = currentQuestion.questionText;
        lupitaImage.gameObject.SetActive(false);
        questionImage.gameObject.SetActive(true);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            // Si existe respuesta para este índice, se asigna texto y listener
            if (currentQuestion != null && i < currentQuestion.answer.Length)
            {
                var answer = currentQuestion.answer[i];

                var textComp = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (textComp != null)
                    textComp.text = answer.answersText;

                answerButtons[i].interactable = true;
                answerButtons[i].onClick.RemoveAllListeners();
                int index = i; // captura segura para el closure
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
            }
            else // Si no hay respuesta para el botón, se limpia y desactiva
            {
                var textComp = answerButtons[i].GetComponentInChildren<Text>();
                if (textComp != null)
                    textComp.text = string.Empty;

                answerButtons[i].interactable = false;
                answerButtons[i].onClick.RemoveAllListeners();
            }
        }

    }

    // Maneja la selección de una respuesta (puedes ampliar la lógica aquí)
    void OnAnswerSelected(int index)
    {
        if (currentQuestion == null || index < 0 || index >= currentQuestion.answer.Length)
            return;

        bool isCorrect = currentQuestion.answer[index].isCorrect;
        Debug.Log(isCorrect ? "Respuesta correcta" : "Respuesta incorrecta");
        if (isCorrect)
        {
            lupitaImage.sprite = corretAnswer;
        }
        else {             
            lupitaImage.sprite = incorrectAnswer;
        }

        lupitaImage.gameObject.SetActive(true);
        questionImage.gameObject.SetActive(false);
        SetAnswerButtonsInteractable(false);
        StartCoroutine(FeedbackAndNext());
    }

    IEnumerator FeedbackAndNext()
    {
        yield return new WaitForSeconds(5f);
        unansweredQuestions.Remove(currentQuestion);
        GetRandomQuestion();

    }

    void SetAnswerButtonsInteractable(bool value)
    {
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (answerButtons[i] != null)
                answerButtons[i].interactable = value;
        }
    }
}