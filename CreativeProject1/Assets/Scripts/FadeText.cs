using System.Collections;
using UnityEngine;
using TMPro;

public class FadeText : MonoBehaviour
{
    public TMP_Text welcomeText;
    public TMP_Text goal;

    public float visibleSeconds = 3f;
    public float fadeDuration = 0.5f;
    public bool playOnStart = true;
    public bool loop = false;

    private Coroutine sequenceCoroutine;

    void Start()
    {
        // ensure initial alpha = 0 (hidden)
        SetAlpha(0f);
        SetActive(false);
        if (playOnStart)
            sequenceCoroutine = StartCoroutine(ShowThenHideSequence());
    }

    public void PlayOnce()
    {
        if (sequenceCoroutine != null) StopCoroutine(sequenceCoroutine);
        sequenceCoroutine = StartCoroutine(ShowThenHideSequence());
    }

    public void StopPlayback()
    {
        if (sequenceCoroutine != null)
        {
            StopCoroutine(sequenceCoroutine);
            sequenceCoroutine = null;
        }
    }

    // Sequence shows welcomeText first, hides it, then shows goal, etc.
    private IEnumerator ShowThenHideSequence()
    {
        do
        {
            // Welcome fades in, stays, fades out
            yield return FadeTextElement(welcomeText, 0f, 1f, fadeDuration);
            yield return new WaitForSeconds(visibleSeconds);
            yield return FadeTextElement(welcomeText, 1f, 0f, fadeDuration);

            // small pause between texts
            yield return new WaitForSeconds(0.15f);

            // Goal fades in, stays, fades out
            yield return FadeTextElement(goal, 0f, 1f, fadeDuration);
            yield return new WaitForSeconds(visibleSeconds);
            yield return FadeTextElement(goal, 1f, 0f, fadeDuration);

            yield return new WaitForSeconds(0.01f);
        }
        while (loop);
        sequenceCoroutine = null;
    }

    // Fades a single TMP_Text element. Activates it for the fade-in and optionally deactivates after fade-out.
    private IEnumerator FadeTextElement(TMP_Text text, float from, float to, float duration)
    {
        if (text == null)
            yield break;

        // Ensure active while fading in
        if (to > 0f)
            text.gameObject.SetActive(true);

        float elapsed = 0f;
        Color baseColor = text.color;
        // start from requested 'from' alpha
        text.color = new Color(baseColor.r, baseColor.g, baseColor.b, from);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float a = Mathf.Lerp(from, to, duration <= 0f ? 1f : elapsed / duration);
            text.color = new Color(baseColor.r, baseColor.g, baseColor.b, a);
            yield return null;
        }

        text.color = new Color(baseColor.r, baseColor.g, baseColor.b, to);

        // If we've faded out completely, deactivate to avoid invisible UI blocking raycasts
        if (to <= 0f)
            text.gameObject.SetActive(false);
    }

    // Helpers to operate on both texts (used at Start)
    private void SetAlpha(float a)
    {
        if (welcomeText != null)
        {
            Color c = welcomeText.color;
            welcomeText.color = new Color(c.r, c.g, c.b, a);
        }
        if (goal != null)
        {
            Color c = goal.color;
            goal.color = new Color(c.r, c.g, c.b, a);
        }
    }

    private void SetActive(bool active)
    {
        if (welcomeText != null) welcomeText.gameObject.SetActive(active);
        if (goal != null) goal.gameObject.SetActive(active);
    }
}
