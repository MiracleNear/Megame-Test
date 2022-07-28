using UnityEngine;

namespace DefaultNamespace.Tween
{
    public static class SpriteRendererExtension
    {
        public static Color BlinkAlpha(this SpriteRenderer spriteRenderer, float frequency, float time)
        {
            Color color = spriteRenderer.color;

            float cyclicFrequency = 2 * Mathf.PI * frequency;

            float fluctuatingAmount = Mathf.Cos(cyclicFrequency * time);

            color.a = fluctuatingAmount;

            return color;
        }
    }
}