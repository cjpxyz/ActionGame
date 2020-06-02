using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTest : MonoBehaviour
{
    //public GameObject SaberMark;

    //rayの長さ
    public float maxDistance = 30;

    [SerializeField]
    private GameObject SaberMark;

    void Start()
    {
        InvokeRepeating("lp", 0.0f, 0.000015f);

    }

    void lp()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        int layerMask = 1 << 8 | 1 << 9;

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            var parent = hit.collider.gameObject.transform;

            var decalPos = hit.point + hit.normal * 0.001f;
            var decalRot = Quaternion.FromToRotation(Vector3.up, hit.normal);
            Instantiate(SaberMark, decalPos, decalRot, parent);
        }
        Debug.DrawRay(ray.origin, ray.direction * maxDistance, Color.red, 1.0f);
    }
}