using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* class GameManager 
* child class of MonoBehavivour
*
* component that manages general settings and structures of the game
* 
* author: Bradley Booth, Academy of Interactive Entertainment, 2017
* author: Aiden Sharp, Academy of Interactive Entertainment, 2017
*/
public class GameManager : MonoBehaviour {

    //list of input channels
    public List<InputChannel> channels;

    //list of groups that correspond to the channels
    public List<Group> groups;

	// Use this for initialization
	void Start () {

        CustomInput.Reset();

        //iterate through all of the input channel
        foreach (InputChannel ic in channels)
        {
            CustomInput.channels.Add(ic);
            CustomInput.mask.Add(false);
        }

        CustomInput.mask[0] = true;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
