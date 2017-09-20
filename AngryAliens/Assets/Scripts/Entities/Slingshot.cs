using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
* class Slingshot 
* child object of MonoBehaviour
* 
* component that manages the firing mechanism of the game, places
* the aliens to fire in a stack and handles input
* 
* author: Bradley Booth, Academy of Interactive Entertainment, 2017
* author: Aiden Sharp, Academy of Interactive Entertainment, 2017
*/
public class Slingshot : MonoBehaviour
{

    //list of objects to fire
    public List<GameObject> stack;

    //bounds of the slingshot
    public CircleCollider2D bounds;

    //reference to the camera movement to disable
    public CameraMovement mover;

    //multiplier for outgoing velocity of aliens
    public float firingMultiplier = 5.0f;

    //reference to the line that represents the sling
    public LineRenderer sling;

    //references to the hinge transforms for the line
    public Transform leftHinge;
    public Transform rightHinge;

    //transforms of focal points for the camera
    public Transform launcherFocus = null;
    private Transform enemyFocus = null;

    //flag indicating if the slingshot was loaded
    private bool slingshotLoaded = false;

    //reference to the input channel that the camera belongs to
    public InputChannel channel;


    // Use this for initialization
    void Start ()
    {
        //assign the references to camera positions
        GameIntro gameIntro = FindObjectOfType<GameIntro>();

        launcherFocus = gameIntro.launcherFocus;
        enemyFocus = gameIntro.enemyFocus;

    }
	
	// Update is called once per frame
	void Update ()
    {
        //grabbing the sling
        if (channel.button1 == ButtonState.PRESSED_EDGE)
        {

            //calculate the true radius of the bounds
            float trueRadius = bounds.GetComponent<CircleCollider2D>().radius * Mathf.Max(bounds.transform.lossyScale.x, bounds.transform.lossyScale.y);

            //the position of the mouse in world space
            Vector3 finalPos = MouseInWorld();

            Vector3 relative = finalPos - bounds.transform.position;
            relative.z = 0;
            
            //check if the mouse is touching the slingshot
            if (relative.magnitude < trueRadius)
            {
                //don't move an alien into the slingshot if it doesn't exist
                if (stack.Count > 0)
                {
                    stack[0].transform.position = finalPos;
                    stack[0].GetComponent<Rigidbody2D>().simulated = true;

                    //sling shot with an additional middle segment
                    sling.SetPositions(new Vector3[] { leftHinge.transform.localPosition, transform.InverseTransformPoint(stack[0].transform.position), rightHinge.transform.localPosition });

                    //stop the intro animation
                    mover.packets.Clear();
                    mover.packetTimer = 0.0f;

                    //destroy the game intro object
                    Destroy(FindObjectOfType<GameIntro>());

                    slingshotLoaded = true;
                }

                mover.takesInput = false;
            }
        }

        //don't run the incoming code blocks if there isn't an alien to fire
        if (!slingshotLoaded)
        {
            //set the default line segments of the sling
            sling.SetPositions(new Vector3[] { leftHinge.transform.localPosition, rightHinge.transform.localPosition });

            mover.takesInput = true;

            return;
        }


        //still holding down the sling
        if (channel.button1 == ButtonState.PRESSED)
        {
            //calculate the true radius of the bounds
            float trueRadius = bounds.GetComponent<CircleCollider2D>().radius * Mathf.Max(bounds.transform.lossyScale.x, bounds.transform.lossyScale.y);

            //the position of the mouse in world space
            Vector3 finalPos = MouseInWorld();

            Vector3 relative = finalPos - bounds.transform.position;
            relative.z = 0;

            if (relative.magnitude < trueRadius)
            {
                stack[0].transform.position = finalPos;
            }
            else
            {
                stack[0].transform.position = bounds.transform.position + relative.normalized * trueRadius;
            }

            //sling shot with an additional middle segment
            sling.SetPositions(new Vector3[] { leftHinge.transform.localPosition, transform.InverseTransformPoint(stack[0].transform.position), rightHinge.transform.localPosition });
        }


        //firing the alien
        if (channel.button1 == ButtonState.RELEASE_EDGE)
        {
            stack[0].GetComponent<Rigidbody2D>().simulated = true;

            //calculate the true radius of the bounds
            float trueRadius = bounds.GetComponent<CircleCollider2D>().radius * Mathf.Max(bounds.transform.lossyScale.x, bounds.transform.lossyScale.y);

            //the position of the mouse in world space
            Vector3 finalPos = MouseInWorld();

            Vector3 relative = finalPos - bounds.transform.position;

            relative = relative.normalized * Mathf.Min(relative.magnitude, trueRadius);
            relative.z = 0;

            stack[0].GetComponent<Rigidbody2D>().velocity = -relative * firingMultiplier;

            //set the fired flag to true
            stack[0].GetComponent<Projectile>().fired = true;

            //remove the alien from the stack
            stack.RemoveAt(0);

            slingshotLoaded = false;

            mover.Goto(mover.transform.position, enemyFocus.transform.position, 1.0f, null);
        }


    }


    /*
    * MouseInWorld 
    * 
    * gets the position of the mouse in the world
    * 
    * @returns Vector3 the position of the mouse in the world
    */
    public Vector3 MouseInWorld()
    {
        //get the camera position
        Vector3 cameraPos = Camera.main.transform.position;

        //get the mouse position relative to the middle of the screen in pixels
        Vector3 mousepos = ((Vector3)channel.position - new Vector3(Screen.width * 0.5f, Screen.height * 0.5f)) * CustomInput.pixelsPerUnit;

        //the position of the mouse in world space
        Vector3 finalPos = cameraPos + mousepos;
        finalPos.z = 0;

        return finalPos;
    }
}
