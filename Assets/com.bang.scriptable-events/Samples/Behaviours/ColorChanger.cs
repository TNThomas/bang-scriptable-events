using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bang.Events;

public class ColorChanger : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float hue;
    private float saturation;
    private float brightness;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    public void RotateHue(float amount)
    {
        Color.RGBToHSV(sprite.color, out hue, out saturation, out brightness);
        hue = (hue + amount) % 1f;
        sprite.color = Color.HSVToRGB(hue, saturation, brightness);
    }
}
