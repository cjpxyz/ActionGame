using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDelete : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("BreakEffect", 1.0f);
    }

    // Update is called once per frame
    void BreakEffect()
    {
        Destroy(gameObject);
    }
}
