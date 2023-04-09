using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSurfaceLight : MonoBehaviour
{

    public Light GlobalDirectionalLight;
    public Transform PlayerTransform;

    public float SurfaceY;
    public float DeepY;
    public float MaxIntensity;
    public float MinIntensity;

    private void Update()
    {
        float y = PlayerTransform.position.y;

        float intensity = 0;
        if (y > SurfaceY)
        {
            intensity = MaxIntensity;
        }
        else if (y < DeepY)
        {
            intensity = MinIntensity;
        }
        else
        {
            float yF = (y - DeepY) / (SurfaceY - DeepY);
            intensity = (MaxIntensity - MinIntensity) * yF + MinIntensity;
        }
        GlobalDirectionalLight.intensity = intensity;

    }

}
