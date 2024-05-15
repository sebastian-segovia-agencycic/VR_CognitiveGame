using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class InteractableButton : MonoBehaviour
{
    public bool doOnce, cloneButton, playOnlyOnce = true, playOnlyOnce1;
    public Spawner spawner;
    private AudioSource audioSource;
    public ScriptableBaseInteractable baseInteractable;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
    }

    public void ButtonReciver()
    {
        playOnlyOnce1 = true;
        if (cloneButton)
        {
            spawner.SetBurstingEnabled(true);
            spawner.StartBurst();
        }
        else
        {
            if (doOnce)
            {
                doOnce = false;
                GeneratorGame.Instance.CalculateFinalGame();
            }
        }
        AudioButtonReciver();
    }

    public void ButtonReciverOff()
    {
        playOnlyOnce1 = false;
        playOnlyOnce = true;
    }

    public void AudioButtonReciver()
    {
        if (playOnlyOnce1)
        {
            if (playOnlyOnce)
            {
                playOnlyOnce = false;
                audioSource.PlayOneShot(baseInteractable.clips[0]);
            }
        }
    }
}

