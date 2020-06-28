using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaberColController : MonoBehaviour
{
    Animator animator;
    public CapsuleCollider capsuleCollider;


    //攻撃判定を出す
    void AttackStart()
    {
        capsuleCollider.enabled = true;
    }

    //攻撃判定を消す
    void AttackEnd()
    {
        capsuleCollider.enabled = false;
    }
}
