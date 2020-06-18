/*using R2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager 
{
    public static float MainHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("Horizontal");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static float MainVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("Vertical");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }

    public static float SubHorizontal()
    {
        float r = 0.0f;
        r += Input.GetAxis("moue X");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static float SubVertical()
    {
        float r = 0.0f;
        r += Input.GetAxis("moue Y");
        return Mathf.Clamp(r, -1.0f, 1.0f);
    }
    public static bool Jump()
    {
        return Input.GetButton("A");
    }


    public static bool Dodge()
    {
        return Input.GetButton("B");
    }

    public static bool CameraButton()
    {
        return Input.GetButton("RightStickClick");
    }


}
*/