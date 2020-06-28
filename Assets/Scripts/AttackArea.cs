using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ActionGame
{
    
    public class AttackArea : MonoBehaviour
    {

        public GameObject HitEffect;
        CharacterStatus status;
        void Start()
        {
            status = transform.root.GetComponent<CharacterStatus>();
        }

        // Update is called once per frame
        public class AttackInfo
        {
            public int attackPower;
            public Transform attacker;
        }

        AttackInfo GetAttackInfo()
        {
            AttackInfo attackInfo = new AttackInfo();
            //攻撃力の計算
            attackInfo.attackPower = status.Power;
            attackInfo.attacker = transform.root;

            return attackInfo;
        }

        void OnTriggerEnter(Collider other)
        {
            other.SendMessage("Damage", GetAttackInfo());
            status.lastAttackTarget = other.transform.root.gameObject;
            Debug.Log("ATTACK!");
            generateEffect();
        }

        void OnAttack()
        {
            GetComponent<Collider>().enabled = true;
        }

        void OnAttackTermination()
        {
            GetComponent<Collider>().enabled = false;
        }

        void generateEffect()
        {
            GameObject effect = Instantiate(HitEffect, transform.position, transform.rotation) as GameObject;
            
        }

    }
}