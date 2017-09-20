using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* class GameWinLose 
* child class of MonoBehavivour
*
* component that checks for a win or lose scenario
* 
* author: Bradley Booth, Academy of Interactive Entertainment, 2017
* author: Aiden Sharp, Academy of Interactive Entertainment, 2017
*/
public class GameWinLose : MonoBehaviour
{
    //reference to the menu callbacks
    public GameMenuCallbacks menuCallbacks = null;

    //timer variables
    public float transitionDuration = 2.0f;
    private float transitionTimer = 0.0f;

	// Use this for initialization
	void Start ()
    {
        transitionTimer = transitionDuration;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //arrays of the remaining projectile of enemies
        Projectile[] projectiles = GameObject.FindObjectsOfType<Projectile>();
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

        //check for the count down
        if (projectiles.Length == 0 || enemies.Length == 0)
        {
            
            //has the timer counted down
            if (transitionTimer <= 0.0f)
            {
                //check that the game is active
                if (CustomInput.mask[0])
                {
                    //losing case
                    if (enemies.Length > 0)
                    {
                        menuCallbacks.EnableMask(3);
                    }
                    //winning case
                    else
                    {
                        menuCallbacks.EnableMask(2);
                    }
                }
            }
            else
            {
                //count-down the timer
                transitionTimer -= Time.deltaTime;
            }
        }
	}
}
