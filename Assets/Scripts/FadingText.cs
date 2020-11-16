using System.Collections;
using UnityEngine;
using TMPro;

public class FadingText : MonoBehaviour
{
    public TextMeshProUGUI text;

    public void ShowToast(string message, int duration)
    {
        StopAllCoroutines();
        StartCoroutine(showToastCOR(message, duration));
    }

    private IEnumerator showToastCOR(string message, float duration)
    {
        Color orginalColor = text.color;

        text.text = message;
        text.enabled = true;

        //Fade in
        yield return fadeInAndOut(text, true, 0.5f);

        //Wait for the duration
        yield return new WaitForSecondsRealtime(duration);

        //Fade out
        yield return fadeInAndOut(text, false, 0.5f);

        text.enabled = false;
        text.color = orginalColor;
    }

    IEnumerator fadeInAndOut(TextMeshProUGUI targetText, bool fadeIn, float duration)
    {
        //Set Values depending on if fadeIn or fadeOut
        float a, b;
        if (fadeIn)
        {
            a = 0f;
            b = 1f;
        }
        else
        {
            a = 1f;
            b = 0f;
        }

        Color currentColor = new Color(1, 1, 1, 0);
        float counter = 0f;

        while (counter < duration)
        {
            counter += Time.deltaTime;
            float alpha = Mathf.Lerp(a, b, counter / duration);

            targetText.color = new Color(currentColor.r, currentColor.g, currentColor.b, alpha);
            yield return null;
        }
    }
}
