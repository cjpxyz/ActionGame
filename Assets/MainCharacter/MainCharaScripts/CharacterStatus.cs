using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ActionGame
{
    public class CharacterStatus : MonoBehaviour
    {
        [SerializeField] private GameObject Self;
        [SerializeField]
        private float HP = 100;
        [SerializeField]
        private float GuardHP = 100;

        private Slider _slider;//Sliderの値を代入するスライダーを宣言
        public GameObject slider;//体力ゲージに指定するスライダー
        private Animator animator;

        public int Power = 10;
        public GameObject lastAttackTarget = null;
        //public bool attacking = false;
        //public bool died = false;

        void Start()
        {
            _slider = slider.GetComponent<Slider>();//スライダーを取得する
            animator = GetComponent<Animator>();
        }

        void Update()
        {
            _slider.value = HP;//スライダーとHPの紐づけ
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "EnemyWeapon")
            {
                HP -= other.gameObject.GetComponent<WeaponDamage>().power;
                animator.SetTrigger("damaged");
                if (HP <= 0)
                {
                    HP = 0;
                    animator.SetTrigger("dead");
                    Self.layer = LayerMask.NameToLayer("Default");
                }
            }
        }
    }

    
}
