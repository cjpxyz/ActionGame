using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ActionGame
{
    public class CharacterStatus : MonoBehaviour
    {
        [SerializeField]
        private float HP = 100;
        [SerializeField]
        private float GuardHP = 100;

        public int Power = 10;
        public GameObject lastAttackTarget = null;
        //public bool attacking = false;
        //public bool died = false;

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "EnemyWeapon")
            {
                HP -= other.gameObject.GetComponent<WeaponDamage>().power;
                if (HP <= 0)
                {
                    HP = 0;
                }
            }
        }
    }

    
}
