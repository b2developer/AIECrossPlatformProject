using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
* class GameMenuCallbacks 
* child class of MonoBehavivour
*
* component that contains all callbacks that buttons can call
* 
* author: Bradley Booth, Academy of Interactive Entertainment, 2017
* author: Aiden Sharp, Academy of Interactive Entertainment, 2017
*/
public class GameMenuCallbacks : MonoBehaviour
{

    //reference to the game manager in this game-object
    public GameManager manager = null;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    
    /*
    * EnableMask
    * 
    * custom call back to enable one mask
    * 
    * @param maskID - the index of the mask
    * @returns void
    */
    public void EnableMask(int maskID)
    {
        //set all masks to false
        for (int i = 0; i < CustomInput.mask.Count; i++)
        {
            CustomInput.mask[i] = false;
            manager.groups[i].SetActive(false);
        }

        CustomInput.mask[maskID] = true;
        manager.groups[maskID].SetActive(true);
    }


    /*
    * LoadScene 
    * 
    * custom call back to load a new scene
    * 
    * @param string sceneName - the name of the scene to load
    * @returns void
    */
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


    /*
    * Reset 
    * 
    * custom call back to reset the scene
    * 
    * @returns void
    */
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
