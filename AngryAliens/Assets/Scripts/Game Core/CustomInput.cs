using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//type for platforms
public enum PlatformType
{
    STANDALONE_WIN,
    ANDROID,
    IOS,
    XBOX_ONE,
    PS4,
}


//type for buttons
public enum ButtonState
{
    RELEASED,
    PRESSED_EDGE,
    PRESSED,
    RELEASE_EDGE,
}


/*
* static class CustomInput 
*
* singleton that deploys a subscriber-publisher pattern
* for input events, regardless of the platform
* 
* author: Bradley Booth, Academy of Interactive Entertainment, 2017
* author: Aiden Sharp, Academy of Interactive Entertainment, 2017
*/
public static class CustomInput
{

//directives used to determine the type
#if UNITY_STANDALONE_WIN
    public static PlatformType platform = PlatformType.STANDALONE_WIN;
#endif

#if UNITY_ANDROID
    public static PlatformType platform = PlatformType.ANDROID;
#endif

#if UNITY_IOS
    public static PlatformType platform = PlatformType.IOS;
#endif

#if UNITY_XBOX_ONE
    public static PlatformType platform = PlatformType.XBOX_ONE;
#endif

#if UNITY_PS4
    public static PlatformType platform = PlatformType.PS4;
#endif

    //position of the mouse, finger etc.
    private static Vector2 position = Vector2.zero;

    //movement of the mouse, finger etc.
    private static Vector2 movement = Vector2.zero;

    //states of buttons
    private static ButtonState button1 = ButtonState.RELEASED;
    private static ButtonState button2 = ButtonState.RELEASED;
    private static ButtonState button3 = ButtonState.RELEASED;

    private static float prevButton1Axis = 0.0f;
    private static float prevButton2Axis = 0.0f;
    private static float prevButton3Axis = 0.0f;

    //list of channels to activate/deactivate
    public static List<InputChannel> channels = new List<InputChannel>();

    //list of bools
    public static List<bool> mask = new List<bool>();

    //ratio between pixels and game units
    public static float pixelsPerUnit = 0.0f;

    /*
    * Reset 
    * 
    * called when the scene re-loads
    *  
    * @returns void
    */
    public static void Reset()
    {
        channels.Clear();
        mask.Clear();
    }


    /*
    * Update
    * 
    * called once per frame, checks for possible callback scenarios
    * 
    * @returns void
    */
    public static void Update()
    {
        //calculate the ratio between pixels and world units
        Vector3 zero = Camera.main.WorldToScreenPoint(Vector3.zero);
        Vector3 left = Camera.main.WorldToScreenPoint(Vector3.left);

        pixelsPerUnit = 1 / (zero - left).magnitude;

        //zero by default
        movement = Vector2.zero;

        if (platform == PlatformType.STANDALONE_WIN)
        {
            //get the change in position
            Vector2 deltaPosition = position;

            //get the mouse position
            position = Input.mousePosition;

            deltaPosition = position - deltaPosition;

            //get the movement of the mouse last frame
            movement = deltaPosition * pixelsPerUnit;

            //get the state of the button
            if (Input.GetMouseButtonDown(0))
            {
                button1 = ButtonState.PRESSED_EDGE;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                button1 = ButtonState.RELEASE_EDGE;
            }
            else if (Input.GetMouseButton(0))
            {
                button1 = ButtonState.PRESSED;
            }
            else
            {
                button1 = ButtonState.RELEASED;
            }

            //get the state of the button
            if (Input.GetMouseButtonDown(1))
            {
                button2 = ButtonState.PRESSED_EDGE;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                button2 = ButtonState.RELEASE_EDGE;
            }
            else if (Input.GetMouseButton(1))
            {
                button2 = ButtonState.PRESSED;
            }
            else
            {
                button2 = ButtonState.RELEASED;
            }

        }
        else if (platform == PlatformType.ANDROID || platform == PlatformType.IOS)
        {
            //only update if the user is touching the screen
            if (Input.touches.Length > 0)
            {
                //get the first touch
                Touch firstTouch = Input.touches[0];

                //get the change in position
                Vector2 deltaPosition = position;

                //get the first touch position
                position = firstTouch.position;

                deltaPosition = position - deltaPosition;

                //get the movement of the mouse last frame
                movement = deltaPosition * pixelsPerUnit;

                //get the state of the button
                if (firstTouch.phase == TouchPhase.Began)
                {
                    button1 = ButtonState.PRESSED_EDGE;
                }
                else if (firstTouch.phase == TouchPhase.Ended)
                {
                    button1 = ButtonState.RELEASE_EDGE;
                }
                else if (firstTouch.phase == TouchPhase.Stationary || firstTouch.phase == TouchPhase.Moved)
                {
                    button1 = ButtonState.PRESSED;
                }
            }
            else
            {
                button1 = ButtonState.RELEASED;
            }

        }
        else if (platform == PlatformType.XBOX_ONE || platform == PlatformType.PS4)
        {
            movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            //get the state of the buttons
            float button1Axis = Input.GetAxisRaw("Submit");
            float button2Axis = Input.GetAxisRaw("Cancel");

            //the axes are buttons, they snap quickly to 0 and 1 but aren't instant
            if (button1Axis < 0.05f && prevButton1Axis < 0.05f)
            {
                button1 = ButtonState.RELEASED;
            }
            else if (button1Axis < 0.05f && prevButton1Axis > 0.95f)
            {
                button1 = ButtonState.RELEASE_EDGE;
            }
            else if (button1Axis > 0.95f && prevButton1Axis > 0.95f)
            {
                button1 = ButtonState.PRESSED;
            }
            else if (button1Axis > 0.95f && prevButton1Axis < 0.05f)
            {
                button1 = ButtonState.PRESSED_EDGE;
            }

            prevButton1Axis = button1Axis;
            prevButton2Axis = button2Axis;
            prevButton3Axis = 0.0f;

            
        }

        //get the size of the channels list
        int channelCount = channels.Count;

        //iterate through all of the channels, updating them with the bitmask
        for (int i = 0; i < channels.Count; i++)
        {
            //store in a temporary value for readability and performance
            InputChannel inpChannel = channels[i];

            if (mask[i])
            { 
                //set the values
                inpChannel.position = position;
                inpChannel.movement = movement;
                inpChannel.button1 = button1;
                inpChannel.button2 = button2;
                inpChannel.button3 = button3;
            }
            else
            {
                //reset the channels
                inpChannel.movement = Vector2.zero;
                inpChannel.button1 = ButtonState.RELEASED;
                inpChannel.button2 = ButtonState.RELEASED;
                inpChannel.button3 = ButtonState.RELEASED;
            }
        }
    }
}
