using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;

/// <summary>
/// Data structure of the JSON file I created.
/// </summary>
[System.Serializable]
public class Question
{
    public string question;
    public string answer;
}

[System.Serializable]
public class Questions
{
    public Question[] Easy;
    public Question[] Medium;
    public Question[] Hard;
}

/// <summary>
/// Class to load JSON data and handle time scales
/// </summary>
public class JsonLoader : MonoBehaviour
{
    string path;
    string jsonString;
    public string[] recievedQuestions;
    public string newQues;
    public string correctAns;
    private GameObject Equation;
    public Text equationText;
    TextMesh quesText;
    public Text timeText;
    int difficultyLevel = 0;
    float timeLeft = 30.0f;
    float deathTime;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
    bool is2D = false;

    Questions jsonData;

    void Start()
    {
        path = Application.streamingAssetsPath + "/Question.json";
        jsonString = File.ReadAllText(path);
        Equation = GameObject.Find("Equation3DText");
        jsonData = JsonUtility.FromJson<Questions>(jsonString);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        if (Equation!=null)
        {
            is2D = false;
            quesText = Equation.GetComponent<TextMesh>();
           
            newQues = GetRandomQuestion(jsonData, difficultyLevel);
            quesText.text = newQues;
        }
        else if(equationText!=null)
        {
            is2D = true;
            timeLeft = 120.0f;
            newQues = GetRandomQuestion(jsonData, difficultyLevel);
            equationText.text = newQues;
        }
       

        
    }

    private void Update()
    {
        timeText.text = timeLeft.ToString();

        timeLeft -= Time.deltaTime;
        deathTime -= Time.deltaTime;
            if (timeLeft < 0)
            {

            if (deathTime <= 0)
            {
                if (is2D)
                {
                    return;
                }
                else
                {
                    GameOver();
                }
            }
             IncreaseDifficulty();
            }
    }



    public IEnumerator GetQuestion()
    {
        if (is2D==false)
        {
            if (quesText.text.Contains("?"))
            {
                int index = quesText.text.IndexOf("?");
                quesText.text = quesText.text.Remove(index);
                quesText.text = quesText.text.Insert(index, correctAns);
            }
        }
        else
        {
            if (equationText.text.Contains("?"))
            {
                int index = equationText.text.IndexOf("?");
                equationText.text = equationText.text.Remove(index);
                equationText.text = equationText.text.Insert(index, correctAns);
                
            }
        }
        yield return new WaitForSeconds(2f);
        newQues = GetRandomQuestion(jsonData,difficultyLevel);
        if (is2D)
        { equationText.text = newQues; }
        else { quesText.text = newQues; }
        

    }

    private string GetRandomQuestion(Questions data,int difficultyLevel)
     {
         string newQuestion;
         Question recievedQuestion;
         Question[] recievedQuestionType;

        switch (difficultyLevel)
        {
            case 0:
                deathTime = 30.0f;
                recievedQuestionType = data.Easy;

                break;
            case 1:
                deathTime = 50.0f;
                recievedQuestionType = data.Medium;
                break;
            case 2:
                deathTime = 80.0f;
                recievedQuestionType = data.Hard;
                break;
            default:
                deathTime = 30.0f;
                recievedQuestionType = data.Easy;
                break;
        }

         int i = UnityEngine.Random.Range(0, recievedQuestionType.Length - 1);

         recievedQuestion =recievedQuestionType[i];
         newQuestion = recievedQuestion.question;
         correctAns = recievedQuestion.answer;

         return newQuestion;

     }

    public void GetNewQues()
    {
        StartCoroutine(GetQuestion());
    }
    public void IncreaseDifficulty()
    {
        if (difficultyLevel != 2)
        {
            difficultyLevel += 1;
        }

        timeLeft += 50f;
        deathTime = timeLeft;
    }

    public void RestartGame()
    {
        if (is2D)
        {
            Time.timeScale = 1.0f;
            UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        }
        else
        {
            Time.timeScale = 1.0f;
            UnityEngine.SceneManagement.SceneManager.LoadScene(3);
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0.0f;

        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        StartCoroutine(GetQuestion());
        pausePanel.SetActive(false);

    }

    public void QuitGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void GameOver()
    {

        Time.timeScale = 0.0f;
        gameObject.GetComponent<AudioSource>().Stop();
        gameOverPanel.SetActive(true);
    }

}