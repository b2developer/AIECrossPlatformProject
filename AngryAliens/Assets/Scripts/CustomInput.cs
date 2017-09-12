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
    public static Vector2 position = Vector2.zero;

    //movement of the mouse, finger etc.
    public static Vector2 movement = Vector2.zero;

    //states of buttons
    public static ButtonState fire = ButtonState.RELEASED;
    public static ButtonState altFire = ButtonState.RELEASED;

    //ratio between pixels and game units
    public static float pixelsPerUnit = 0.0f;

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

        if (platform == PlatformType.STANDALONE_WIN)
        {
            //get the change in position
            Vector2 deltaPosition = position;

            //get the mouse position
            position = Input.mousePosition;

            deltaPosition = position - deltaPosition;

            //get the movement of the mouse last frame
            movement = deltaPosition * pixelsPerUnit;

            Debug.Log(pixelsPerUnit);

            //get the state of the button
            if (Input.GetMouseButtonDown(0))
            {
                fire = ButtonState.PRESSED_EDGE;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                fire = ButtonState.RELEASE_EDGE;
            }
            else if (Input.GetMouseButton(0))
            {
                fire = ButtonState.PRESSED;
            }
            else
            {
                fire = ButtonState.RELEASED;
            }


            //get the state of the button
            if (Input.GetMouseButtonDown(1))
            {
                altFire = ButtonState.PRESSED_EDGE;
            }
            else if (Input.GetMouseButtonUp(1))
            {
                altFire = ButtonState.RELEASE_EDGE;
            }
            else if (Input.GetMouseButton(1))
            {
                altFire = ButtonState.PRESSED;
            }
            else
            {
                altFire = ButtonState.RELEASED;
            }

        }
    }
}
