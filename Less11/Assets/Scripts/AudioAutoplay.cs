using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Audio/Autoplay")]
public class AudioAutoplay : MonoBehaviour
{
    [SerializeField]
    AudioItem audioItem;
    AudioSource audioSource = null;
    float duration;
    bool stopByDuration = false;

    public static AudioAutoplay Create(GameObject parent, AudioItem audioItem) 
    {
        var newComponent = parent.AddComponent<AudioAutoplay>();
        newComponent.audioItem = audioItem;
        return newComponent;
    }

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioItem.Clip;
        audioSource.loop = audioItem.Loop;
        if (audioSource.loop)
            audioSource.Play();
        else
            audioSource.PlayOneShot(audioSource.clip);
        if (audioItem.Duration != 0) {
            duration = audioItem.Duration;
            stopByDuration = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (stopByDuration)
        {
            duration -= Time.deltaTime;
            if (duration <= 0f)
                audioSource.Stop();
        }
        if (!audioSource.isPlaying)
            Destroy(gameObject);
    }
}
