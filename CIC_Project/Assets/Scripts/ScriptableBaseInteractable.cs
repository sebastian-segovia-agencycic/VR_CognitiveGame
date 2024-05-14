using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "DataBase Interactable", order = 1)]
public class ScriptableBaseInteractable : ScriptableObject
{
    public List<AudioClip> clips = new List<AudioClip>();

}
