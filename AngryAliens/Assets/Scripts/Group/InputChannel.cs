using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
* class InputChannel 
* child class of MonoBehavivour
*
* component that stores a group of game-objects that can recieve input
* 
* author: Bradley Booth, Academy of Interactive Entertainment, 2017
* author: Aiden Sharp, Academy of Interactive Entertainment, 2017
*/
public class InputChannel : MonoBehaviour
{

    //position of the mouse, finger etc.
    public Vector2 position = Vector2.zero;

    //movement of the mouse, finger etc.
    public Vector2 movement = Vector2.zero;

    //states of buttons
    public ButtonState button1 = ButtonState.RELEASED;
    public ButtonState button2 = ButtonState.RELEASED;
    public ButtonState button3 = ButtonState.RELEASED;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
