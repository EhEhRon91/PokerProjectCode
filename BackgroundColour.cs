using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundColour : MonoBehaviour
{
    private GameObject background;
    // Start is called before the first frame update
    void Start()
    {
        background = GameObject.Find("Background");
        if (PlayerPrefs.HasKey("r") && gameObject.name == "Background")
        {
            SetColor();
        }
    }

    public void GetColour()
    {
        PlayerPrefs.SetFloat("r", gameObject.GetComponent<Image>().color.r);
        PlayerPrefs.SetFloat("g", gameObject.GetComponent<Image>().color.g);
        PlayerPrefs.SetFloat("b", gameObject.GetComponent<Image>().color.b);
        SetColor();
    }

    public void SetColor()
    {
        background.GetComponent<Image>().color = new Color(PlayerPrefs.GetFloat("r"), PlayerPrefs.GetFloat("g"), PlayerPrefs.GetFloat("b"));
    }
}
