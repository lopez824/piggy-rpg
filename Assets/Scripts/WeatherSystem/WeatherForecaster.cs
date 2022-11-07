using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherForecaster : MonoBehaviour
{
    [Serializable] 
    public class WeatherData
    { 
        public ParticleSystem effect;
        public float probability;
    }

    public List<WeatherData> weatherEffects;

    public Dictionary<string, ParticleSystem> weatherParticles;
    public Dictionary<string, float> weatherProbabilities;

    void Awake()
    {
        weatherParticles = new Dictionary<string, ParticleSystem>();
        weatherProbabilities = new Dictionary<string, float>();

        foreach (WeatherData data in weatherEffects)
        {
            weatherParticles.Add(data.effect.name, data.effect);
            weatherProbabilities.Add(data.effect.name, data.probability);
            Debug.Log(data.effect.name);
            Debug.Log(data.probability);
        }
    }
}
