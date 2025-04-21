using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Tilia.Interactions.SnapZone;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

public class Fade : MonoBehaviour
{
    public static Fade Instance;
    private Animator animator;
    [HideInInspector] public int hashAnimBool = Animator.StringToHash("Enable");

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public void StartFade() => StartCoroutine(FadeTransition());

    private IEnumerator FadeTransition()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(hashAnimBool, true);
        yield return null;
    }

    public void StopFade()
    {
        animator = GetComponent<Animator>();
        animator.SetBool(hashAnimBool, false);
    }

}