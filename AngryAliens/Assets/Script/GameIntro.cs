using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
* class GameIntro 
* child class of MonoBehavivour
*
* component that calls upon the camera to display the introductory
* movement sequence of the game before enabling the input
* 
* author: Bradley Booth, Academy of Interactive Entertainment, 2017
* author: Aiden Sharp, Academy of Interactive Entertainment, 2017
*/
public class GameIntro : MonoBehaviour
{
    //reference to the camera movement object to send commands to
    public CameraMovement mover = null;

    //references to the two places to look lerp between
    public Transform launcherFocus = null;
    public Transform enemyFocus = null;
  

    // Use this for initialization
    void Start ()
    {
        mover.Goto(launcherFocus.position, launcherFocus.position, 3.0f, null); //wait 3 seconds
        mover.Goto(launcherFocus.position, enemyFocus.position, 2.0f, null); //goto enemy focus in 2 seconds
        mover.Goto(enemyFocus.position, enemyFocus.position, 2.0f, null); //wait 2 seconds
        mover.Goto(enemyFocus.position, launcherFocus.position, 2.0f, null); //goto launcher focus in 2 seconds
    }
	

	// Update is called once per frame
	void Update ()
    {
		
	}
}
