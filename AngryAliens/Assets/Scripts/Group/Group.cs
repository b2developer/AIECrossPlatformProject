using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* class Group 
* child class of MonoBehavivour
*
* a collection of game-objects
* 
* author: Bradley Booth, Academy of Interactive Entertainment, 2017
* author: Aiden Sharp, Academy of Interactive Entertainment, 2017
*/
public class Group : MonoBehaviour
{
    //list of group objects
    public List<GroupObject> groupObjects;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    /*
    * SetActive
    * 
    * sets the state of all gameobjects
    * 
    * @param bool state - the new state to set
    * @returns void
    */
    public void SetActive(bool state)
    {
        //get the size of the group list
        int groupSize = groupObjects.Count;

        //iterate through all group objects, removing nulls
        for (int i = 0; i < groupSize; i++)
        {
            //store in a temp value for readability and performance
            GroupObject g = groupObjects[i];

            //remove the game-object
            if (g == null)
            {
                groupObjects.RemoveAt(i);
                i--;
                groupSize--;
            }
        }

        //iterate through all group objects
        foreach (GroupObject g in groupObjects)
        {
            g.SetActive(state);
        }
    }
}
