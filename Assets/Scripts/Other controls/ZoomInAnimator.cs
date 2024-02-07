using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomInAnimator : MonoBehaviour
{

    private Vector3 desiredScale, initialScale;
    private float zoomInRate, zoomInFrequency;

    // Start is called before the first frame update
    void Start()
    {
        initialScale = Vector3.one.normalized;
        zoomInRate = 1.06f;
        zoomInFrequency = 0.03f;
    }

    private void OnEnable()
    {
        desiredScale = transform.localScale;
        transform.localScale = initialScale;

        InvokeRepeating("ZoomIn", 0.0f, zoomInFrequency);
    }

    private void OnDisable()
    {
        transform.localScale = desiredScale;
    }

    private void ZoomIn()
    {
        if(transform.localScale.magnitude < desiredScale.magnitude)
        {
            transform.localScale *= zoomInRate;
        }
        else
        {
            CancelInvoke();
        }
    }

   
}
