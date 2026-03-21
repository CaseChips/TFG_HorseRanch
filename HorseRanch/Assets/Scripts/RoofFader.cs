using UnityEngine;
using System.Collections;

public class RoofFader : MonoBehaviour
{
    [Header("Items To Fade")]
    public SpriteRenderer[] renderersToFade;

    [Header("Settings")]
    public float fadeSpeed = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeTo(0f)); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(FadeTo(1f)); 
        }
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        if (renderersToFade.Length == 0) yield break;

        float currentAlpha = renderersToFade[0].color.a;

        while (Mathf.Abs(currentAlpha - targetAlpha) > 0.01f)
        {
            currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, fadeSpeed * Time.deltaTime);

            foreach (SpriteRenderer renderer in renderersToFade)
            {
                if (renderer != null)
                {
                    Color newColor = renderer.color;
                    newColor.a = currentAlpha;
                    renderer.color = newColor;
                }
            }

            yield return null;
        }

        foreach (SpriteRenderer renderer in renderersToFade)
        {
            if (renderer != null)
            {
                Color finalColor = renderer.color;
                finalColor.a = targetAlpha;
                renderer.color = finalColor;
            }
        }
    }
}