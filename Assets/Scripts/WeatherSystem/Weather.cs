using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weather
{
    protected WeatherForecaster forecaster;

    public Weather(WeatherForecaster weatherForecaster)
    {
        forecaster = weatherForecaster;
    }

    public virtual void enter() { }
    public virtual Weather transition() { return this; }
    public virtual void exit() { }
}
