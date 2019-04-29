using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    public GameController3D gameController3D;
    public GameController2D gameController2D;
    public bool isCorrect = false;
    public AudioClip correctSound;
    public AudioClip incorrectSound;
    public AudioSource playerAudioSource;


    void Start()
    {
        if (gameController2D == null)
        {
            gameController2D = GameObject.Find("GameController").GetComponent<GameController2D>();
            
        }
        if(gameController3D==null)
        {
            gameController3D = GameObject.Find("GameController").GetComponent<GameController3D>();
            //if(gameController3D==null)
            //{
            //    return;
            //}
        }


        playerAudioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {

        string ans;


        if (other.tag != "Prefab")
        {
            Destroy(other.gameObject);
            return;
        }
        else
        {
            ans = other.gameObject.GetComponent<Prefab3D>().Ans;
            isCorrect = gameController3D.CheckAnswer(ans);
            gameController3D.StartCoroutine(gameController3D.SpawnParticles());
            Respawn(other.gameObject);
            if (isCorrect)
            {
                //play good sound
                playerAudioSource.clip = correctSound;
                playerAudioSource.Play();

                gameController3D.StartCoroutine(gameController3D.UpdateScore());


                Destroy(other.gameObject);
            }
            else
            {

                //play bad sound
                playerAudioSource.clip = incorrectSound;
                playerAudioSource.Play();
                gameController3D.playerAnim.SetBool("isCorrectAns", false);

                Destroy(other.gameObject);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        string ans;
        if (other.tag != "Prefab")
        {
            Destroy(other.gameObject);
        }
        else
        {
            gameController2D.StartCoroutine(gameController2D.SpawnParticles());
            ans = other.gameObject.GetComponent<Prefab2D>().ans;
            isCorrect = gameController2D.CheckAnswer(ans);

            if (isCorrect)
            {
                playerAudioSource.clip = correctSound;
                playerAudioSource.Play();


                gameController2D.UpdateScore();

            }
            else
            {

                playerAudioSource.clip = incorrectSound;
                playerAudioSource.Play();


            }
            Destroy(other.gameObject);
        }
    }


    private void Respawn(GameObject prefab)
    {
        GameObject duplicate = (GameObject)Instantiate(prefab, prefab.transform.localPosition + (new Vector3(10, UnityEngine.Random.Range(-0.5f, 2f), 20f)), prefab.transform.rotation);
    }

}