using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleOptionsMenu : MonoBehaviour
{
    private GameObject optionsMenu;
    // Start is called before the first frame update
    void Awake()
    {
        optionsMenu = GameObject.Find("OptionMenu");
    }

    private void Start()
    {
        OptionMenuDeactivation();
    }

    public void OptionMenuActivation()
    {
        optionsMenu.SetActive(true);
    }

    public void OptionMenuDeactivation()
    {
        optionsMenu.SetActive(false);
    }
}
