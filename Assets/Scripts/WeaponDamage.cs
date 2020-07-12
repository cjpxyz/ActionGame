using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    public GameObject HitEffect;
    public float power = 10; //攻撃力

    [SerializeField]
    AudioClip[] HitSonud;
    private AudioSource audioSource;
    [SerializeField] 
    bool randomizePitch = true;
    [SerializeField] 
    float pitchRange = 0.1f;


    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }


    void OnTriggerEnter(Collider other)
    {
        generateEffect();
    }
    

    void generateEffect()
    {
        GameObject effect = Instantiate(HitEffect, transform.position, transform.rotation) as GameObject;
        if (randomizePitch)
        {
            audioSource.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);
            audioSource.PlayOneShot(HitSonud[Random.Range(0, HitSonud.Length)]);
        }
    }
}
