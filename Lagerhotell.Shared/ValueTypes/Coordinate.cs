﻿namespace LagerhotellAPI.Models.ValueTypes;

public class Coordinate
{
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public Coordinate(double latitude, double longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public Coordinate() { }
}
