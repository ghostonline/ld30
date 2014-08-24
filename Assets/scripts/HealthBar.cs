using UnityEngine;
using System.Collections;

[ExecuteInEditMode()]
public class HealthBar : MonoBehaviour {

    public Transform background;
    public Transform bar;
    public SpriteRenderer barSprite;

    public Color fullColor;
    public Color emptyColor;

    public float value;

    static float colorValueEase(float input)
    {
        return input*(2-input);
    }

	void Update () {
        if (bar == null || background == null) { return; }

        var barScale = bar.localScale;
        var normalizedValue = Mathf.Clamp01(value);
        var targetScale = background.localScale.x * normalizedValue;
        if (!Mathf.Approximately(barScale.x, targetScale))
        {
            barScale.x = targetScale;
            bar.localScale = barScale;

            if (barSprite != null)
            {
                var colorValue = colorValueEase(normalizedValue);
                var color = Color.Lerp(emptyColor, fullColor, colorValue);
                barSprite.color = color;
            }
        }
	}
}
