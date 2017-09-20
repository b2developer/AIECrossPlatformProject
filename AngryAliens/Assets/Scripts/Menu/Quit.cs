using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour {


    //public Button QuitButton;
	// Use this for initialization
    public void QuitButton()
    {
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

    #else
        Application.Quit();
    #endif
    }
}
