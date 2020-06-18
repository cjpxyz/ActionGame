using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetConeDecoupler : MonoBehaviour
{
    public void Detach()
    {
        transform.parent = null;
    }
}
