using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelFader : MonoBehaviour
{
    // Start is called before the first frame update
    private bool mFaded = false;
    public float Duration = 1.6f;
    public void FadeIn()
    {
        var canvGroup = GetComponent<CanvasGroup>();
       
        StartCoroutine(DoFade(canvGroup, 0, 1));
        canvGroup.interactable = true;
    }
    public void FadeOut()
    {
        var canvGroup = GetComponent<CanvasGroup>();

        StartCoroutine(DoFade(canvGroup, 1, 0));
        canvGroup.interactable = false;
    }

    public IEnumerator DoFade(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;

        while(counter < Duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            yield return null;
        }
    }
}
