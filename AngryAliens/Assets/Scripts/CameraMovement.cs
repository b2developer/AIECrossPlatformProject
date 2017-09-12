using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
* class CameraPacket 
*
* data structure that describes
* a movement action for the camera
* 
* author: Bradley Booth, Academy of Interactive Entertainment, 2017
* author: Aiden Sharp, Academy of Interactive Entertainment, 2017
*/
public class CameraPacket
{
    //type for function references
    public delegate void DefaultCallback();

    //vector to start at
    public Vector3 start = new Vector3();

    //vector to travel to
    public Vector3 target = new Vector3();

    //amount of seconds to travel to
    public float travelTime = 0.0f;

    //callback to execute when the destination is reached
    public DefaultCallback callback = null;

    /*
    * CameraPacket() 
    * constructor, accepts values for the member variables
    * 
    * @param Vector3 start_ - the input start
    * @param Vector3 target_ - the input target
    * @param float travelTime_ the input travelTime
    * @param DefaultCallback callback_ - the input callback
    */
    public CameraPacket(Vector3 start_, Vector3 target_, float travelTime_, DefaultCallback callback_)
    {
        start = start_;
        target = target_;
        travelTime = travelTime_;
        callback = callback_;
    }
}


/*
* class CameraMovement 
* child class of MonoBehaviour 
*
* component that clamps the camera to 
* a specified region in 2D space
* 
* author: Bradley Booth, Academy of Interactive Entertainment, 2017
* author: Aiden Sharp, Academy of Interactive Entertainment, 2017
*/
public class CameraMovement : MonoBehaviour
{

    //reference to the box collider that the camera is allowed to move around in
    public BoxCollider2D clamp = null;

    //reference to the camera component
    public Camera cam = null;

    //list of camera travel destinations
    public List<CameraPacket> packets = null;

    //time used to track the current packet
    public float packetTimer = 0.0f;

    //boolean flag that dictates whether the camera can take user input or not
    public bool takesInput = false;

	// Use this for initialization
	void Start ()
    {
        cam = GetComponent<Camera>();
        packets = new List<CameraPacket>();
	}


    /*
    * Goto 
    * 
    * places a command in a queue that tells the camera how to move
    * 
    * @param Vector3 start - the starting position
    * @param Vector3 target - the stopping position
    * @param float time - the time it takes to travel between the two points
    * @param CameraPacker.DefaultCallback callback - the function invoked when the movement finishes
    * @returns void
    */
    public void Goto(Vector3 start, Vector3 target, float time, CameraPacket.DefaultCallback callback)
    {
        packets.Add(new CameraPacket(start, target, time, callback));
    }
	

	// Update is called once per frame
	void LateUpdate () 
    {
        //get the size of the packets list
        int packetSize = packets.Count;

        //if there is a packet to travel to
        if (packetSize > 0)
        {
            //get the next packet to travel to
            CameraPacket nextPacket = packets[0];

            //tick the timer
            packetTimer += Time.smoothDeltaTime;

            //check that the next packet isn't an instantaneous journey
            if (nextPacket.travelTime != 0)
            {
                //lerp the position between start and target using the time passed as a ratio
                transform.position = Vector3.Lerp(nextPacket.start, nextPacket.target, packetTimer / nextPacket.travelTime);

                //remove the packet if the target has been reached
                if (packetTimer >= nextPacket.travelTime)
                {
                    //only invoke the callback if one is referenced
                    if (nextPacket.callback != null)
                    {
                        nextPacket.callback.Invoke();
                    }

                    packets.RemoveAt(0);
                    packetTimer = 0.0f;
                }
            }
            else
            {
                //instantly jump to target and remove the packet
                transform.position = nextPacket.target;

                //only invoke the callback if one is referenced
                if (nextPacket.callback != null)
                {
                    nextPacket.callback.Invoke();
                }

                packets.RemoveAt(0);
                packetTimer = 0.0f;
            }
        }
       

        if (takesInput)
        {
            if (CustomInput.fire == ButtonState.PRESSED)
            {
                transform.position -= (Vector3)CustomInput.movement;
            }
        }

        //get the size of the game screen in units
        float verticalSize = cam.orthographicSize;
        float horizontalSize = verticalSize * cam.aspect;

        //get the corners of the camera
        float minX = clamp.transform.position.x - clamp.transform.lossyScale.x * clamp.size.x * 0.5f + horizontalSize;
        float maxX = clamp.transform.position.x + clamp.transform.lossyScale.x * clamp.size.x * 0.5f - horizontalSize;
        float minY = clamp.transform.position.y - clamp.transform.lossyScale.y * clamp.size.y * 0.5f + verticalSize;
        float maxY = clamp.transform.position.y + clamp.transform.lossyScale.y * clamp.size.y * 0.5f - verticalSize;

        //clamp the position
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minX, maxX), Mathf.Clamp(transform.position.y, minY, maxY), transform.position.z);
	}
}
