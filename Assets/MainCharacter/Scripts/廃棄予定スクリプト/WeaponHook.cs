using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace R2
{
    public class WeaponHook : MonoBehaviour
    {
        public GameObject damageCollider;

        public void Start()
        {

        }

        // Update is called once per frame
        public void DamegeColliderStatus(bool status)
        {
            damageCollider.SetActive(status);
        }
    }
}
