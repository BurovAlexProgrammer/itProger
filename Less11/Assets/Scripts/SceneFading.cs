using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFading : MonoBehaviour
{
    [SerializeField]
    Texture2D fadingTexture;
    [SerializeField]
    float fadeSpeed = 0.8f,
        alpha = 1f,
        delay = 1f;
    [SerializeField]
    int drawDepth = -1000;
    public enum Directions { None, FadeIn, FadeOut}
    [SerializeField]
    Directions direction;

    [SerializeField]
    bool StartWithFadeIn = true;

    private void Start()
    {
        if (StartWithFadeIn)
            direction = Directions.FadeIn;
    }

    private void OnGUI()
    {
        var fadeDirection = -1f;
        if (direction == Directions.FadeOut)
            fadeDirection = 1f;
        alpha += fadeDirection * fadeSpeed * Time.deltaTime;
        alpha = Mathf.Clamp01(alpha);
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha);
        GUI.depth = drawDepth;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadingTexture);
        //Отключаем вычисления по окончанию эффекта
        if (direction == Directions.FadeIn && alpha == 0f)
            delay -= Time.deltaTime;
        if (direction == Directions.FadeOut && alpha == 1f)
            delay -= Time.deltaTime;
        if (delay <= 0f) 
            this.enabled = false;
    }

    public void FadeIn()
    {
        this.enabled = true;
        delay = 2f;
        direction = Directions.FadeIn;
    }

    public void FadeOut()
    {
        this.enabled = true;
        delay = 2f;
        direction = Directions.FadeOut;
    }


    public float GetFadeTime()
    {
        return 1f/fadeSpeed;
    }
}
