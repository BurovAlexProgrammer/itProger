using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AudioItem
{
    [SerializeField]
    string name;
    public string Name { get { return name; } }
    [SerializeField]
    AudioClip clip;
    public AudioClip Clip { get { return clip; } }
    [Tooltip("Duration = 0 as once")]
    [SerializeField]
    float duration = 0f;
    public float Duration { get { return duration; } }
    [SerializeField]
    bool loop = false;
    public bool Loop { get { return loop; } }
}
