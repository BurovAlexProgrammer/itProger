using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField]
    Camera[] cameras;

    private void OnGUI()
    {
        var styleBox = new GUIStyle(GUI.skin.box) { fontSize = 30 };
        var styleButton = new GUIStyle(GUI.skin.button) { 
            fontSize = 25
        };

        GUI.Box(new Rect(10, 10, 350, 600), "Панель управления", styleBox);

        if (GUI.Button(new Rect(32,60,310,40), "Камера 1", styleButton))
            SetCamera(0);

        if (GUI.Button(new Rect(32, 110, 310, 40), "Камера 2", styleButton))
            SetCamera(1);

        if (GUI.Button(new Rect(32, 160, 310, 40), "Камера 3", styleButton))
            SetCamera(2);
    }

    void SetCamera(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].enabled = (i == index);
        }
    }

}
