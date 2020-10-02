using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RandomColor : MonoBehaviour
{
    [SerializeField]
    Color colorInEditor = Color.white;

    void Start()
    {
        OnChangeColorInEditor();
    }

    private void OnValidate()
    {
        OnChangeColorInEditor();
    }

    void OnChangeColorInEditor()
    {
        if (Application.isPlaying)
        {
            GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
        }
        else
        {
            var sharedMaterial = GetComponent<MeshRenderer>().sharedMaterial;
            if (sharedMaterial != null)
                GetComponent<MeshRenderer>().sharedMaterial.color = colorInEditor;
        }
    }
}
