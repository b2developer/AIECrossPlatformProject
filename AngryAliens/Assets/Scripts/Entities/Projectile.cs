using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* class Projectile 
* child class of MonoBehavivour
*
* a component that represents an entity to fire at enemies
* 
* author: Bradley Booth, Academy of Interactive Entertainment, 2017
* author: Aiden Sharp, Academy of Interactive Entertainment, 2017
*/
public class Projectile : MonoBehaviour
{
    //flag indicating if the projectile was fired
    public bool fired = false;

    //automatic reference to the rigidbody
    private Rigidbody2D body = null;

	// Use this for initialization
	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //check if the body is sleeping, and destroy it
        if (fired && body.IsSleeping())
        {
            Destroy(gameObject);
        }
	}
}
