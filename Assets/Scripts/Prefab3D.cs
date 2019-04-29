using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Prefab3D script
/// </summary>


public class Prefab3D : MonoBehaviour
{
    public string Ans;
    public Text prefabText;
    
    void Start()
    {

        prefabText = GetComponent<Text>();
      
        if (prefabText)
        {
            Ans = prefabText.text;
        }

    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 20*Time.deltaTime*10f, 10 * Time.deltaTime * 4f));   
    }
}