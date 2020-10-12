using UnityEngine;

public static class MyExtension
{
    public static Color SetAlpha(this Color color, float alpha)
    {
        var tempColor = color;
        tempColor.a = alpha;
        return tempColor;
    }
}
