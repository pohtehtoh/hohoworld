using UnityEngine;
using UnityEngine.UI;

public class Rain : MonoBehaviour
{
    [SerializeField] private ParticleSystem fogParticleSys;
    [SerializeField] private Toggle toggleNoRain;
    [SerializeField] private Toggle toggleLessRain;
    [SerializeField] private Toggle toggleMediumRain;
    [SerializeField] private Toggle toggleHeavyRain;
    [SerializeField] private AudioClip lessRain;
    [SerializeField] private AudioClip mediumRain;
    [SerializeField] private AudioClip heavyRain;
    private ParticleSystem particleSys;
    private AudioSource audioSource;
    ParticleSystem.EmissionModule emissionModule;
    ParticleSystem.EmissionModule fogEmissionModule;

    private void Awake()
    {
        particleSys = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        emissionModule = particleSys.emission;
        fogEmissionModule = fogParticleSys.emission;

        if (emissionModule.rateOverTime.constant <= 0f)
        {
            audioSource.clip = null;
        }
        else if (emissionModule.rateOverTime.constant <= 100f)
        {
            audioSource.clip = lessRain;
        }
        else if (emissionModule.rateOverTime.constant <= 500f)
        {
            audioSource.clip = mediumRain;
        }
        else if (emissionModule.rateOverTime.constant <= 2000f)
        {
            audioSource.clip = heavyRain;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
    public void NoRain()
    {
        if(toggleNoRain.isOn)
        {
            toggleLessRain.isOn = false;
            toggleMediumRain.isOn = false;
            toggleHeavyRain.isOn = false;
            emissionModule.rateOverTime = 0f;
            fogEmissionModule.rateOverTime = 0f;
        }

    }
    public void LessRain()
    {
        if(toggleLessRain.isOn)
        {
            toggleNoRain.isOn = false;
            toggleMediumRain.isOn = false;
            toggleHeavyRain.isOn = false;
            emissionModule.rateOverTime = 500f;
            fogEmissionModule.rateOverTime = 50f;
        }
        
    }
    public void MediumRain()
    {
        if(toggleMediumRain.isOn)
        {
            toggleLessRain.isOn = false;
            toggleNoRain.isOn = false;
            toggleHeavyRain.isOn = false;
            emissionModule.rateOverTime = 1000f;
            fogEmissionModule.rateOverTime = 100f;
        }
        
    }
    public void HeavyRain()
    {
        if(toggleHeavyRain.isOn)
        {
            toggleLessRain.isOn = false;
            toggleMediumRain.isOn = false;
            toggleNoRain.isOn = false;
            emissionModule.rateOverTime = 3000f;
            fogEmissionModule.rateOverTime = 200f;
        }
        
    }
}
