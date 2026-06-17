using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAnimation : MonoBehaviour
{
    [Header("Animation Factors")]
    public float scaleSpeed = 1f; // Speed of scaling
    public float minScale = 0.5f; // Minimum scale factor
    public float maxScale = 2f; // Maximum scale factor

    private bool scalingUp = true;

    void Start()
    {
        //StartCoroutine(ScaleImage());
    }

    private void OnEnable()
    {
        StartCoroutine(ScaleImage());
    }

    IEnumerator ScaleImage()
    {
        while (true)
        {
            if (scalingUp)
            {
                transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
                if (transform.localScale.x >= maxScale)
                {
                    scalingUp = false;
                }
            }
            else
            {
                transform.localScale -= Vector3.one * scaleSpeed * Time.deltaTime;
                if (transform.localScale.x <= minScale)
                {
                    scalingUp = true;
                }
            }

            yield return null;
        }
    }
}
