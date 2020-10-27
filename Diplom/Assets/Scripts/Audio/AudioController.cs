using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[AddComponentMenu("Audio/AudioController")]
public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    [SerializeField]
    List<AudioItem> clips;

    private void Awake()
    {
        if (instance != null)
            Debug.LogError("Обнаружен дубликат AudioController");
        instance = this;
    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play(string clipName)
    {
        var autoPlayGameObject = new GameObject();
        autoPlayGameObject.transform.SetParent(transform);
        AudioAutoplay.Create(autoPlayGameObject, GetItem(clipName));
    }

    AudioItem GetItem(string itemName)
    {
        return clips.Where(c => c.Name.Equals(itemName, System.StringComparison.InvariantCultureIgnoreCase)).Single();
    }
}
