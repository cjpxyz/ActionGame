using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSE : MonoBehaviour
{
    [SerializeField] AudioClip[] clips;
    [SerializeField] bool randomizePitch = true;
    [SerializeField] float pitchRange = 0.1f;

    protected AudioSource source;

    private void Awake()
    {
        source = GetComponents<AudioSource>()[0];
    }

    public void PlaySwingSE()
    {
        if (randomizePitch)
            source.pitch = 1.0f + Random.Range(-pitchRange, pitchRange);

        source.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }
}
