using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public GameObject HitEffect;
    public float power = 10; //攻撃力
    public AudioClip HitSonud;
    private AudioSource audioSource;

    
	void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }


    void OnTriggerEnter(Collider other)
    {
        generateEffect();
    }

    // Update is called once per frame
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
        audioSource.PlayOneShot(HitSonud);
    }
}
