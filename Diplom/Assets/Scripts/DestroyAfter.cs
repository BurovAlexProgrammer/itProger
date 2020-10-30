//rev 1
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Behaviour/DestroyAfter")]
public class DestroyAfter : MonoBehaviour
{
    enum Cases { timeOut, animationEnded, collisionEnter, particlesEnded}

    [SerializeField]
    float timer = 1f;

    public static DestroyAfter CreateComponent(GameObject gameObject, float timer)
    {
        var component = gameObject.AddComponent<DestroyAfter>();
        component.timer = timer;
        return component;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
            Destroy(gameObject);
    }
}
