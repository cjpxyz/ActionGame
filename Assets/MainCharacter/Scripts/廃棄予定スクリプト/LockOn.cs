using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

namespace ActionGame
{

    public class LockOn : MonoBehaviour
    {
        //ターゲットのリスト
        [SerializeField]
        private List<GameObject> enemyList;
        //標的
        [SerializeField] private GameObject lookTarget;
        [SerializeField] Transform lockOnTarget = null;
        //キャラ操作のスクリプト
        private PlayerMove playerMove;

        public CinemachineVirtualCamera virtualCamera;
        private Animator animator;

        public float rotateSpeed = 0.1f;

        public bool isSearch;
        public bool lockOn;

        
        

        CinemachineFreeLook freeLookCamera = null;
        // CinemachineVirtualCamera virtualCamera = null;

        void Start()
        {
            animator = GetComponent<Animator>();
            enemyList = new List<GameObject>();
            lookTarget = null;
            playerMove = GetComponent<PlayerMove>();
            bool isSearch = false;
            bool lockOn = false;
        }

        void Update()
        {
            //ターゲットの切り替え
            TargetOthers();
        }

        void TargetOthers()
        {
            if (enemyList.Count == 0)
            {
                lookTarget = null;
                return;
            }

            if(playerMove.GetState() != PlayerMove.State.Combat)
            {
                return;
            }

            float inputHorizontal = Input.GetAxis("Horizontal");

            //ターゲットのゲームオブジェクト
            GameObject nearTarget = null;

            //キャラクターターゲットとの角度
            float nearTargetAngle = 360f;

            foreach(var enemy in enemyList)
            {
                //　この敵が現在のターゲットの時、または主人公と敵との間に壁があれば何もしない
                if (enemy == lookTarget || 
                    Physics.Linecast(transform.parent.transform.position
                    +
                    Vector3.up, enemy.transform.position
                    + 
                    Vector3.up, LayerMask.GetMask("Field")))
                {
                    continue;
                }

                //　今調べている敵と主人公との角度を設定
                float targetAngle = Vector3.SignedAngle(transform.parent.forward, enemy.transform.position - transform.parent.position, Vector3.up);

                //　現在ターゲットにしている敵と主人公との角度を設定（設定されていない時はエラーになるので回避）
                if (nearTarget != null)
                {
                    nearTargetAngle = Vector3.SignedAngle(transform.parent.forward, nearTarget.transform.position - transform.parent.position, Vector3.up);
                }

                //　左を押した時でターゲットが真後ろから左側の角度が返ってきた時、または
                //　右を押した時でターゲットが真後ろから右側の角度が返ってきた時
                if((inputHorizontal < 0f && -180f <= targetAngle && targetAngle <= 0f)
                    || (inputHorizontal > 0f && 0f <= targetAngle && targetAngle <= 180f))
                {
                    if (nearTarget == null)
                    {
                        nearTarget = enemy;
                    }else if (Math.Abs(targetAngle)< Mathf.Abs(nearTargetAngle))
                    {
                        nearTarget = enemy;
                    }
                }
            }

            //　近くのターゲットがいれば設定
            if (nearTarget != null)
            {
                lookTarget = nearTarget;
            }
        }



        protected void OnTriggerStay(Collider c)
        {
            //サーチエリア
            Debug.DrawLine(transform.parent.position + Vector3.up, c.gameObject.transform.position + Vector3.up, Color.blue);
            if (c.tag == "Enemy" && !enemyList.Contains(c.gameObject))
            {
                //敵をリストに登録
                enemyList.Add(c.gameObject);
                /*virtualCamera.Priority = 12;
                virtualCamera.LookAt = c.gameObject.transform;*/
                isSearch = true;
                lockOn = true;
            }
        }

        protected void OnTriggerExit(Collider c)
        {
            //敵がサーチエリアを抜けたらリストから削除
            if (c.gameObject.tag == "Enemy" && !enemyList.Contains(c.gameObject))
            {
                if(c.gameObject == lookTarget)
                {
                    lookTarget = null;
                }
                enemyList.Remove(c.gameObject);
                /*virtualCamera.Priority = 8;
                isSearch = false;
                lockOn = false;*/
            }
        }

        public GameObject getNowTarget()
        {
            return lookTarget;
        }

        //死んだ敵をリストから除外
        void DeleteEnemyList(GameObject obj)
        {
            if (lookTarget == obj)
            {
                lookTarget = null;
            }
            enemyList.Remove(obj);
        }

        //ターゲットを設定
        public void SetNowTarget()
        {
            //　一番近い敵を標的に設定する
            foreach(var enemy in enemyList)
            {
                //　ターゲットがいなくて敵との間に壁がなければターゲットにする
                if (lookTarget == null)
                {
                    if (!Physics.Linecast(transform.parent.position 
                        + 
                        Vector3.up, enemy.transform.position
                        + 
                        Vector3.up, LayerMask.GetMask("Field")))
                    {
                        lookTarget = enemy;
                    }
                    //　ターゲットがいる場合で今の敵の方が近ければ今の敵をターゲットにする
                }else if (Vector3.Distance(transform.parent.position, enemy.transform.position) < Vector3.Distance(transform.parent.position, 
                    lookTarget.transform.position)
                    && 
                    !Physics.Linecast(transform.parent.position + Vector3.up, enemy.transform.position + Vector3.up, LayerMask.GetMask("Field")))
                {
                    lookTarget = enemy;
                }
            }
        }
    }
}