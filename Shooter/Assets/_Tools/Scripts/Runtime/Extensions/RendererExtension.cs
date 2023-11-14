using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace _Tools.Extensions
{
    public static class RendererExtension
    {
        public static void MaterialSplashEffect(this Renderer renderer, Color32 originalColor, Color32 targetColor, float duration = .1f)
        {
            renderer.material.DOColor(targetColor, duration).OnComplete(() => { renderer.material.DOColor(originalColor, duration); });
        }
        public static void Reset(this TrailRenderer trail)
        {
            trail.Clear();
        }
    }
}