using UnityEngine;
using System.Collections; 

public class RoofFader : MonoBehaviour
{
    [Header("What are we fading?")]
    public SpriteRenderer roofRenderer;

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
        Color currentColor = roofRenderer.color;

        while (Mathf.Abs(currentColor.a - targetAlpha) > 0.01f)
        {
            currentColor.a = Mathf.MoveTowards(currentColor.a, targetAlpha, fadeSpeed * Time.deltaTime);
            roofRenderer.color = currentColor;

            yield return null;
        }

        currentColor.a = targetAlpha;
        roofRenderer.color = currentColor;
    }
}