using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* class Enemy 
* child class of MonoBehavivour
*
* a component that represents a robot to destroy
* 
* author: Bradley Booth, Academy of Interactive Entertainment, 2017
* author: Aiden Sharp, Academy of Interactive Entertainment, 2017
*/
public class Enemy : MonoBehaviour
{

    //amount of damage the enemy can take before dying
    public float health = 20.0f;

    //minimum amount of damage required to actually affect the enemy
    public float minDamage = 1.0f;

    //reference to the gameobject to spawn when the enemy is killed
    public GameObject deathParticlePrefrab = null;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}


    /*
    * TakeDamage
    * 
    * inflicts damage to the enemy, destroying
    * it if the damage takes it's health below 0
    * 
    * @param float damage - the amount of damage to inflict
    * @returns void
    */
    public void TakeDamage(float damage)
    {
        //don't apply damage if it is above the threshold
        if (damage < minDamage)
        {
            return;
        }

        health -= damage;

        //check for death
        if (health <= 0)
        {
            //spawn death particles
            GameObject deathParticles = Instantiate(deathParticlePrefrab) as GameObject;
            deathParticles.transform.position = transform.position;

            //destroy it 1 second later
            Destroy(deathParticles, 1.0f);

            Destroy(gameObject);
            
        }
    }


    /*
    * OnCollisionEnter2D 
    * 
    * automatic call-back when the gameobject
    * enters a collision
    * 
    * @param Collision2D coll - collision object generated form the collision
    */
    public void OnCollisionEnter2D(Collision2D coll)
    {
        //get the rigidbody component
        Rigidbody2D body = coll.collider.GetComponent<Rigidbody2D>();

        if (body != null)
        {
            //kinetic energy
            TakeDamage(0.5f * coll.relativeVelocity.sqrMagnitude * coll.collider.GetComponent<Rigidbody2D>().mass);
        }
    }


    /*
    * OnCollisionStay2D 
    * 
    * automatic call-back when the gameobject
    * continues a collision
    * 
    * @param Collision2D coll - collision object generated form the collision
    */
    public void OnCollisionStay2D(Collision2D coll)
    {
        //get the rigidbody component
        Rigidbody2D body = coll.collider.GetComponent<Rigidbody2D>();

        if (body != null)
        {
            //kinetic energy
            TakeDamage(0.5f * coll.relativeVelocity.sqrMagnitude * coll.collider.GetComponent<Rigidbody2D>().mass);
        }
    }
}
