using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController3D : MonoBehaviour
{

    private JsonLoader jsonLoaderInstance;

    public Text scoreText;
    public Text currentScoreText;
    public Text answerText;
    private int score;
    public Animator playerAnim;
    private string answer;
    public GameObject particle;

    private int highScore;
    public Text highScoreText;

    void Start()
    {
        score = 0;
        if (PlayerPrefs.HasKey("HighScore3D"))
        {
            highScore = PlayerPrefs.GetInt("HighScore3D");
        }
        else
        {
            PlayerPrefs.SetInt("HighScore3D", 0);
        }
        highScoreText.text = "HighScore: " + highScore.ToString();
        scoreText.text = "Score: 0";
        answerText.text = "Collected: ";
        currentScoreText.text = scoreText.text;
        jsonLoaderInstance = GetComponent<JsonLoader>();

    }


    public IEnumerator SpawnParticles()
    {
        GameObject part = (GameObject)Instantiate(particle, GameObject.Find("Player").transform.localPosition, GameObject.Find("Player").transform.localRotation) as GameObject;
        
        yield return new WaitForSeconds(2f);

        Destroy(part);

    }
    
    public bool CheckAnswer(string text)
    {
        
        answer+= text;
        answerText.text = "Collected:"+ answer;
        if (jsonLoaderInstance.correctAns == answer)
        {
            jsonLoaderInstance.GetNewQues();
            answer = "";
            
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

    private bool CheckForMultipleDigits(string toCheck,string rightAns)
    {
        int checkIndex=toCheck.Length;
        int correctIndex = rightAns.Length;
        bool isCorrect=false;
        
        int elementIndex = checkIndex - correctIndex;
        if(elementIndex<0)
        {
            elementIndex =-1*elementIndex ;
        }
        string[] testString=new string[2];

        testString = toCheck.Split(toCheck[elementIndex]);

        if(testString[1].Equals(rightAns))
        {
            isCorrect = true;
            answerText.text = "";
        }
        
        

        return isCorrect;
    }

    public IEnumerator UpdateScore()
    {
        playerAnim.SetBool("isCorrectAns", true);

        score += 10;
        scoreText.text = "Score: " + score.ToString();

        if(highScore<score)
        {
            PlayerPrefs.SetInt("HighScore3D",score);
        }
        currentScoreText.text = scoreText.text;
        highScore = PlayerPrefs.GetInt("HighScore3D");
        highScoreText.text= "HighScore: "+ highScore.ToString();
        yield return new WaitForSeconds(1.5f);
        playerAnim.SetBool("isCorrectAns", false);
    }

    public void OnResetClick()
    {
        answer = "";
        answerText.text = "Collected:" + answer;
    }
}

//IEnumerator SpawnAnswerPrefabs()
//{
//    yield return new WaitForSeconds(1f);
//    GameObject box;
//    for (int i=0;i<spawnPoints.Length;i++)
//    {
//        for(int j=0;j<spawnPoints[i].childCount;j++)
//        {
//            spawnPoints[i].GetChild(j);
//            int k = UnityEngine.Random.Range(0, spawnPoints.Length);
//            box = (GameObject)Instantiate(prefabs[k], spawnPoints[i].position, spawnPoints[i].rotation) as GameObject;

//            yield return new WaitForSeconds(5f);
//            Destroy(box);
//        }

//    }

//    yield return new WaitForSeconds(11f);
//    StartCoroutine(SpawnAnswerPrefabs());
//}