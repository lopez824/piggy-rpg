using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sunny : Weather
{
    public Sunny(WeatherForecaster weatherForecaster) : base (weatherForecaster)
    {
        forecaster = weatherForecaster;
    }

    public override void enter()
    {

    }

    public override Weather transition()
    {
        return this;
    }

    public override void exit()
    {

    }
}
