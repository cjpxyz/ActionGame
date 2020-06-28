using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

namespace ActionGame
{

    public class LockOnTarget : MonoBehaviour
    {
        public GameObject player;
        public GameObject target;
        public CinemachineVirtualCamera virtualCamera;
        private Animator animator;
        public float rotateSpeed = 0.1f;
        public float search_radius = 10f;
        public bool isSearch;
        public bool lockOn;
        public Transform lookPosition;

        [SerializeField] public GameObject lookTarget = null;
        [SerializeField] Transform lockOnTarget = null;

        CinemachineFreeLook freeLookCamera = null;
       // CinemachineVirtualCamera virtualCamera = null;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            animator = GetComponent<Animator>();
            /*freeLookCamera = FindObjectOfType<CinemachineFreeLook>();
            foreach (var vcam in FindObjectsOfType<CinemachineVirtualCamera>())
            {
                if (vcam.name == "CM vcam1")
                {
                    virtualCamera = vcam;
                }
            }*/

            bool isSearch = false;
            bool lockOn = false;
        }

        void Update()
        {
            transform.position = player.transform.position;
            

            
            //ロックオン
            if (Input.GetButton("RB"))
            {
                if (lookTarget == null)
                {
                    GameObject target = GetTargetClosestPlayer();

                    if (target != null)
                    {
                        lookTarget = target;
                        lookPosition = target.transform.Find("LookPosition");
                        virtualCamera.LookAt = lookTarget.transform;
                        virtualCamera.Priority = 12;
                        animator.SetBool("lockOn", true);
                        isSearch = true;
                        lockOn = true;
                        Debug.Log("ロックオン");
                    }
                    Quaternion targetRotation = Quaternion.LookRotation(lookTarget.transform.position - transform.position, Vector3.up);
                    targetRotation.x = 0;
                    targetRotation.z = 0;
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

                }
            }
            else if(lookTarget != null)
            {
                lookTarget = null;
                virtualCamera.Priority = 8;
                animator.SetBool("lockOn", false);
                isSearch = false;
                lockOn = false;
                Debug.Log("ロックオフ");
            }

        }

        protected GameObject GetTargetClosestPlayer()
        {
            

            var hits = Physics.SphereCastAll(player.transform.position, 
                                             search_radius, 
                                             player.transform.forward, 
                                             0.01f,
                                             LayerMask.GetMask("Enemy")).Select(h => h.transform.gameObject).ToList();

            if (0 < hits.Count())
            {
                float min_target_distance = float.MaxValue;
                GameObject target = null;

                foreach (var hit in hits)
                {
                    float target_distance = Vector3.Distance(player.transform.position, hit.transform.position);

                    if (target_distance < min_target_distance)
                    {
                        min_target_distance = target_distance;
                        target = hit.transform.gameObject;
                    }
                }
                return target;
            }
            else
            {
                return null;
            }
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if(lookTarget != null)
            this.animator.SetLookAtWeight(1.0f, 0.3f, 0.8f, 0.0f, 0.4f);
            this.animator.SetLookAtPosition(lookPosition.transform.position);
        }

        /*protected List<GameObject> FilterTargetObject(List<GameObject> hits)
        {
            return hits
                .Where(h => {
                    Vector3 screenPoint = Camera.main.WorldToViewportPoint(h.transform.position);
                    return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
                })
                .Where(h => h.tag == "Enemy")
                .ToList();
        }*/


        /* protected void OnTriggerEnter(Collider c)
         {
             if (c.gameObject.tag == "Enemy")
             {
                 //target = c.gameObject;
                 lookTarget = c.gameObject;
                 virtualCamera.Priority = 12;
                 virtualCamera.LookAt = c.gameObject.transform;
                 isSearch = true;
                 lockOn = true;
             }
         }

         protected void OnTriggerExit(Collider c)
         {
             if (c.gameObject.tag == "Enemy")
             {
                 lookTarget = null;
                 virtualCamera.Priority = 8;
                 virtualCamera.LookAt = null;
                 isSearch = false;
                 lockOn = false;
             }
         }

         public GameObject getTarget()
         {
             return this.lookTarget;
         }

         /*public void ChangeLockOnCamera()
         {
             if (lockOn)
             {
                 freeLookCamera.m_BindingMode = CinemachineTransposer.BindingMode.LockToTarget;
                 virtualCamera.Priority = 12;
             }
             else
             {
                 freeLookCamera.m_BindingMode = CinemachineTransposer.BindingMode.SimpleFollowWithWorldUp;
                 virtualCamera.Priority = 8;
             }
         }*/
    }
}