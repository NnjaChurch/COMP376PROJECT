using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CopLights : MonoBehaviour
{
    public Light2D light;
    public int offset;
    public int frequency;

    // Update is called once per frame
    void Update()
    {
        light.intensity = (Mathf.Sin((frequency * Time.time) - ((offset/360) * Mathf.PI)) + 1) / 4;
    }
}
