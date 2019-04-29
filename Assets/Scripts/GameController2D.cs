using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController2D : MonoBehaviour
{

    public GameObject prefab;
    public GameObject particle;
    public Transform[] SpawnPoints;
    public string[] prefabTexts;
    public Text answerText;
    public Text livesText;
    public Text scoreText;
    public Text currentScoreText;
    public int score;
    public int lives;
    public string answer;
    private JsonLoader jsonLoaderInstance;
    private int highScore;
    public Text highScoreText;

    
    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore2D"))
        {
            highScore = PlayerPrefs.GetInt("HighScore2D");
        }
        else
        {
            PlayerPrefs.SetInt("HighScore2D",0);
        }
        jsonLoaderInstance = GetComponent<JsonLoader>();
        score = 0;
        answerText.text = "Collected:";
        highScoreText.text = "HighScore: " + highScore.ToString();
        StartCoroutine(SpawnAnswerPrefabs());
        lives = 3;
        score = 0;
        livesText.text = "Lives:" + lives;
        scoreText.text = "Score:" + score;
        currentScoreText.text = scoreText.text;
    }


    IEnumerator SpawnAnswerPrefabs()
    {
        
        int i = UnityEngine.Random.Range(0,SpawnPoints.Length-1);
        GameObject droplet = (GameObject)Instantiate(prefab,SpawnPoints[i].position,SpawnPoints[i].rotation) as GameObject;
        TextMesh textMesh = droplet.GetComponent<TextMesh>();
        int j = UnityEngine.Random.Range(0,prefabTexts.Length-1);
        textMesh.text = prefabTexts[j];
    
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(SpawnAnswerPrefabs());

    }

   public IEnumerator SpawnParticles()
    {
        GameObject part = (GameObject)Instantiate(particle, GameObject.Find("Player").transform.localPosition, GameObject.Find("Player").transform.localRotation ) as GameObject;
        part.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(5f);
        Destroy(part);

    }

    public void OnResetClick()
    {
        answer = "";
        answerText.text = "Collected:" + answer;
        UpdateLives();
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score:" + score.ToString();
        if (highScore < score)
        {
            PlayerPrefs.SetInt("HighScore2D", score);
        }
        currentScoreText.text = scoreText.text;
        highScoreText.text = "HighScore: " + highScore.ToString();
    }
    public void UpdateLives()
    {
        lives--;
        if(lives<0)
        {
            jsonLoaderInstance.GameOver();
            return;
        }
        livesText.text = "Lives:" + lives.ToString();
    }

    public bool CheckAnswer(string text)
    {
       
        answer += text;
        answerText.text = "Collected:" + answer;
        if (jsonLoaderInstance.correctAns == answer)
        {
            jsonLoaderInstance.GetNewQues();
            answer = "";
            answerText.text = "Collected:";
            return true;

        }
        else
        {
            bool isCorrect = false;
            if (answer.Length > 1)
            {
                isCorrect = CheckForMultipleDigits(answer, jsonLoaderInstance.correctAns);
            }

            return isCorrect;
        }
    

    }

    private bool CheckForMultipleDigits(string toCheck, string rightAns)
    {
        int checkIndex = toCheck.Length;
        int correctIndex = rightAns.Length;
        bool isCorrect = false;

        int elementIndex = checkIndex - correctIndex;
        if (elementIndex <0)
        {
            elementIndex = -1*elementIndex;
        }
        string[] testString = new string[2];

        testString = toCheck.Split(toCheck[elementIndex]);

        if (testString[1].Equals(rightAns))
        {
            isCorrect = true;
            answerText.text = "";
          
        }
        else
        {
            UpdateLives();
        }



        return isCorrect;
    }
}
