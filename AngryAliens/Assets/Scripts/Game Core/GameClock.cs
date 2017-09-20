using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
* class GameClock 
*
* component that regularly updates all singletons
* that required updates at regular intervals
* 
* author: Bradley Booth, Academy of Interactive Entertainment, 2017
* author: Aiden Sharp, Academy of Interactive Entertainment, 2017
*/
public class GameClock : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        CustomInput.Update();
    }

    void LateUpdate()
    {
        
    }
}
