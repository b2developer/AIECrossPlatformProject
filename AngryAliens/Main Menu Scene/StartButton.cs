using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    //Uses The index of the build order to load a scene when the start button is clicked
  public void LoadSceneIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);

    }
	

}
