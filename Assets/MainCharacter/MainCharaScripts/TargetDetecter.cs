using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActionGame
{
    public class TargetDetecter : MonoBehaviour
    {

        [SerializeField]
        private GameObject target;

        protected void OnTriggerEnter(Collider c)
        {
            if (c.gameObject.tag == "Enemy")
            {
                //target = c.gameObject;
                target = c.gameObject;
                /*virtualCamera.Priority = 12;
                virtualCamera.LookAt = c.gameObject.transform;
                isSearch = true;
                lockOn = true;*/
            }
        }

        protected void OnTriggerExit(Collider c)
        {
            if (c.gameObject.tag == "Enemy")
            {
                target = null;
                /*virtualCamera.Priority = 8;
                isSearch = false;
                lockOn = false;*/
            }
        }

        public GameObject getTarget()
        {
            return this.target;
        }
    }
}