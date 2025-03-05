using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TestManager : MonoBehaviour
{
    [System.Serializable]
    public class Answer
    {
        public string text; 
        public int points; 
    }

    [System.Serializable]
    public class Question
    {
        public string text; 
        public List<Answer> answers; 
    }

    [System.Serializable]
    public class Test
    {
        public string name; 
        public Sprite image; 
        public List<Question> questions; 
        public List<Result> results; 
    }

    [System.Serializable]
    public class Result
    {
        public string name; 
        public string description;
        public int minPoints; 
    }

    public List<Test> tests; 
    public Image testImage; 
    public TextMeshProUGUI testName; 
    public TextMeshProUGUI resultTestName;
    public Image resultTestImage;
    public TextMeshProUGUI questionText; 
    public TextMeshProUGUI progressText; 
    public Button[] answerButtons; 
    public Sprite neutralSprite; 
    public Sprite selectedSprite; 
    public GameObject resultPopup; 
    public TextMeshProUGUI resultName;
    public TextMeshProUGUI resultDescription;

    private int currentTestIndex; 
    private int currentQuestionIndex; 
    private int totalPoints; 
    private Test currentTest;

  

    public void LoadTest(int testIndex)
    {
        currentTestIndex = testIndex;
        currentTest = tests[testIndex];
        currentQuestionIndex = 0;
        totalPoints = 0;

        testImage.sprite = currentTest.image;
        testName.text = currentTest.name;
        resultTestImage.sprite = currentTest.image;
        resultTestName.text = currentTest.name;
        resultPopup.SetActive(false);

        ShowQuestion();
    }

    private void ShowQuestion()
    {
        if (currentQuestionIndex >= currentTest.questions.Count)
        {
            ShowResult();
            return;
        }

        Question question = currentTest.questions[currentQuestionIndex];
        questionText.text = question.text;

        progressText.text = $"{currentQuestionIndex + 1}/{currentTest.questions.Count}";

        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < question.answers.Count)
            {
                answerButtons[i].gameObject.SetActive(true);

                TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                buttonText.text = question.answers[i].text;

                int points = question.answers[i].points; 
                int buttonIndex = i; 

               
                answerButtons[i].onClick.RemoveAllListeners();

             
                answerButtons[i].onClick.AddListener(() => OnAnswerSelected(buttonIndex, points));

                answerButtons[i].image.sprite = neutralSprite;
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnAnswerSelected(int buttonIndex, int points)
    {
        totalPoints += points;

        
        answerButtons[buttonIndex].image.sprite = selectedSprite;

      
        StartCoroutine(NextQuestionWithDelay());
    }

    private IEnumerator NextQuestionWithDelay()
    {
        yield return new WaitForSeconds(1f);

        currentQuestionIndex++;
        ShowQuestion();
    }

    private void ShowResult()
    {
        resultPopup.SetActive(true);

      
        if (totalPoints >= 15 && totalPoints <= 25)
        {
            resultName.text = currentTest.results[0].name; 
            resultDescription.text = currentTest.results[0].description;
        }
        else if (totalPoints >= 26 && totalPoints <= 35)
        {
            resultName.text = currentTest.results[1].name; 
            resultDescription.text = currentTest.results[1].description;
        }
        else if (totalPoints >= 36 && totalPoints <= 45)
        {
            resultName.text = currentTest.results[2].name;
            resultDescription.text = currentTest.results[2].description;
        }
        else if (totalPoints >= 46 && totalPoints <= 60)
        {
            resultName.text = currentTest.results[3].name; 
            resultDescription.text = currentTest.results[3].description;
        }
    }
    public void ReloadTest()
    {
        LoadTest(currentTestIndex);
    }
}
