// FogEvent.cs
using UnityEngine;

public class FogEvent : MonoBehaviour
{
    [Header("Settings")]
    public ParticleSystem fogParticles;
    public float fogDensity = 0.5f;

    private void OnEnable()
    {
        if (fogParticles != null)
        {
            fogParticles.Play();
            RenderSettings.fogDensity = fogDensity;
            Debug.Log("Fog event started!");
        }
    }

    private void OnDisable()
    {
        if (fogParticles != null)
        {
            fogParticles.Stop();
            RenderSettings.fogDensity = 0f;
            Debug.Log("Fog event ended!");
        }
    }
}