using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//type for how the object gets deactivated
public enum ActiveType
{
    PHYSICAL,
    VISUAL,
    CAMERA,
}

/*
* class GroupObject 
* child class of MonoBehavivour
*
* component that represents that the game-object belongs to a group
* 
* author: Bradley Booth, Academy of Interactive Entertainment, 2017
* author: Aiden Sharp, Academy of Interactive Entertainment, 2017
*/
public class GroupObject : MonoBehaviour
{
    //type for the group project
    public ActiveType type;

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
        //get the state and the type of the game-object
        if (state)
        {
            if (type == ActiveType.PHYSICAL)
            {
                gameObject.SetActive(true);
            }
            else if (type == ActiveType.VISUAL)
            {
                //get all components in the game-object
                Component[] components = GetComponents<Component>();

                //each component has it's own unique enabled flag

                //iterate through all of the components, disabling all of them
                for (int i = 0; i < components.Length; i++)
                {
                    //store in a temp value for readability for performance
                    Component comp = components[i];

                    //up-cast the component
                    if (comp as Collider != null)
                    {
                        (comp as Collider).enabled = true;
                    }
                    else if (comp as Renderer != null)
                    {
                        (comp as Renderer).enabled = true;
                    }
                    else if (comp as Rigidbody2D != null)
                    {
                        (comp as Rigidbody2D).simulated = true;
                    }
                    else if (comp as Behaviour != null)
                    {
                        (comp as Behaviour).enabled = true;
                    }

                }
            }
            else if (type == ActiveType.CAMERA)
            {
                //get all scripts in the game-object
                MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

                //iterate through all of the scripts, enabling them
                for (int i = 0; i < scripts.Length; i++)
                {
                    //store in a temp value for readability for performance
                    MonoBehaviour script = scripts[i];

                    script.enabled = true;
                }
            }

        }
        else
        {
            if (type == ActiveType.PHYSICAL)
            {
                gameObject.SetActive(false);
            }
            else if (type == ActiveType.VISUAL)
            {
                //get all components in the game-object              
                Component[] components = GetComponents<Component>();

                //each component has it's own unique enabled flag

                //iterate through all of the components, disabling all of them
                for (int i = 0; i < components.Length; i++)
                {
                    //store in a temp value for readability for performance
                    Component comp = components[i];

                    //up-cast the component
                    if (comp as Collider != null)
                    {
                        (comp as Collider).enabled = false;
                    }
                    else if (comp as Renderer != null)
                    {
                        (comp as Renderer).enabled = false;
                    }
                    else if (comp as Rigidbody2D != null)
                    {
                        (comp as Rigidbody2D).simulated = false;
                    }
                    else if (comp as Behaviour != null)
                    {
                        (comp as Behaviour).enabled = false;
                    }

                }

                //set the renderer as enabled
                GetComponent<Renderer>().enabled = true;
            }
            else if (type == ActiveType.CAMERA)
            {
                //get all scripts in the game-object
                MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();

                //iterate through all of the scripts, enabling them
                for (int i = 0; i < scripts.Length; i++)
                {
                    //store in a temp value for readability for performance
                    MonoBehaviour script = scripts[i];

                    script.enabled = false;
                }
            }

        }
    }
}
