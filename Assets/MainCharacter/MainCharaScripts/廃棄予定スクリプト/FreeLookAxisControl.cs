/*using Cinemachine;
using R2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLookAxisControl : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    // Update is called once per frame
    public float GetAxisCustom(string axisName)
    {
        if(axisName == "mouse X")
        {
            return InputManager.SubHorizontal();
        }
        else if(axisName == "mouse Y")
        {
            return InputManager.SubVertical();
        }
        return 0;
    }
}
*/