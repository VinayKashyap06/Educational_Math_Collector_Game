using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prefab2D : MonoBehaviour
{
    private TextMesh textMesh;
    public string ans;
    

    void Start()
    {       
        textMesh = GetComponent<TextMesh>();
        ans = textMesh.text;
    }
 
}
