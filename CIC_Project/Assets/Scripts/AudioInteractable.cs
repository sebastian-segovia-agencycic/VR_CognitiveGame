using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioInteractable : MonoBehaviour
{
    public List<AudioClip> clips = new List<AudioClip>();
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Count)]);
    }
}
