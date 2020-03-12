using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class TorchLightFlicker : MonoBehaviour
{
    [SerializeField] private Light2D torchLight;
    [SerializeField] private float minFlickerIntensity = 0.25f;
    [SerializeField] private float maxFlickerIntensity = 0.95f;
    [SerializeField] private float flickerSpeed = 0.035f;
    private int randomizer = 1;

    private void Start()
    {
        StartCoroutine(TorchFlickerRoutine());
    }

    private IEnumerator TorchFlickerRoutine()
    {
        while(true)
        {
            if(randomizer == 0)
            {
                torchLight.intensity = (Random.Range(minFlickerIntensity, maxFlickerIntensity));
            }
            else
            {
                torchLight.intensity = (Random.Range(minFlickerIntensity, maxFlickerIntensity));
            }

            randomizer = (int)Random.Range(0f, 1.1f);
            yield return new WaitForSeconds(flickerSpeed);
        }
    }
}
