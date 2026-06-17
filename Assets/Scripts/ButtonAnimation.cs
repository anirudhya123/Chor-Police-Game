using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Unity.VisualScripting;

public class ButtonAnimation : MonoBehaviour
{

    [SerializeField] private Vector2 minScale;
    [SerializeField] private Vector2 maxScale;

    [SerializeField] float scalingSpeed;
    [SerializeField] float scaleDuration;


    private IEnumerator Start()
    {
        while (true)
        {
            yield return Lerping(minScale,maxScale,scaleDuration);
            yield return Lerping(maxScale,minScale,scaleDuration);
        }
    }

    IEnumerator Lerping(Vector2 startScale, Vector2 endScale, float time)
    {
        float t = 0.0f;
        float rate = (1f/time) * scalingSpeed;
        while (t < 1f)
        {
            t += Time.deltaTime * rate;
            transform.localScale = Vector2.Lerp(startScale, endScale, t);
            yield return null;
        }
    }

}
