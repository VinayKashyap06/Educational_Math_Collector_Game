using System;
using UnityEngine;
using UnityEngine.UI;

public class Movement2D : MonoBehaviour
{
    float x;
    public int lives = 3;
    public Text livesText;
    private bool isDead;
   
    void FixedUpdate()
    {
        x = Input.GetAxis("Horizontal");
        float xValue = Mathf.Clamp(transform.localPosition.x, -14f, 14f);
        
        
        transform.localPosition = new Vector3(xValue,-5f,0)+ new Vector3(x*Time.deltaTime*40f, 0, 0);
    }

}
