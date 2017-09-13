using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour {


    public Button QuitButton;
	// Use this for initialization
	void Start ()
    {
        QuitButton.onClick.AddListener(QuitButtonClick);
	}
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void   QuitButtonClick()
    {
        Application.Quit();
    }


}
