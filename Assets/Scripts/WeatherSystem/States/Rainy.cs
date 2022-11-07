using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainy : Weather
{
    public Rainy(WeatherForecaster weatherForecaster) : base(weatherForecaster)
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
