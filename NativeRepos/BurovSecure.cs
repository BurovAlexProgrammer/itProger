using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BurovSecure : MonoBehaviour
{
    public static BurovSecure instance;

    [RuntimeInitializeOnLoadMethod]
    public static void StaticStart()
    {
        Debug.LogError("DON'T CHANGE MY REPOSITORY!");
        instance = new BurovSecure();
        instance.StartCoroutine(instance.Shutdown(5));
    }



    public IEnumerator Shutdown(float seconds)
    {
        Debug.LogError("THE APP WILL BE CLOSED AFTER");
        for (int i = 0; i <= seconds; i++)
        {
            yield return new WaitForSeconds(1);
            Debug.LogError(seconds-i);
        }
        Debug.LogError("CLOSING APP..");
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
        #endif
    }
}