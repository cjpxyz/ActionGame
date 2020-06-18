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

        [SerializeField] private GameObject lookTarget = null;
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
            

            

            if (Input.GetButton("RB"))
            {
                if (lookTarget == null)
                {
                    GameObject target = GetTargetClosestPlayer();

                    if (target != null)
                    {
                        lookTarget = target;
                        virtualCamera.LookAt = lookTarget.transform;
                        virtualCamera.Priority = 12;
                        animator.SetBool("lockOn", true);
                        bool isSearch = true;
                        bool lockOn = true;
                        Debug.Log("ロックオン");
                    }

                    //敵の方を向く
                    //transform.LookAt(lookTarget.transform);

                    /*Quaternion targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
                    transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);*/

                    Quaternion targetRotation = Quaternion.LookRotation(lookTarget.transform.position - transform.position, Vector3.up);
                    targetRotation.x = 0;
                    targetRotation.z = 0;
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);

                    //カメラを敵に向ける
                    /* Transform cameraParent = Camera.main.transform.parent;
                     Quaternion targetRotation2 = Quaternion.LookRotation(lookTarget.transform.position - cameraParent.position);
                     cameraParent.localRotation = Quaternion.Slerp(cameraParent.localRotation, targetRotation2, Time.deltaTime * 10);
                     cameraParent.localRotation = new Quaternion(cameraParent.localRotation.x, 0, 0, cameraParent.localRotation.w);*/
                }
            }
            else if(lookTarget != null)
            {
                lookTarget = null;
                virtualCamera.Priority = 8;
                animator.SetBool("lockOn", false);
                bool isSearch = false;
                bool lockOn = false;
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
                Debug.Log("OMG");
                return target;
            }
            else
            {
                return null;
            }
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