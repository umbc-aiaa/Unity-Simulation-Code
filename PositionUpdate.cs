/**
File: ImuDetection.cs
Overview: The code is used to update the positions of the plane
Project: AirPlane Simulation
Date: October 22th, 2026
Creator: Process Adhikari
*/

using UnityEngine;
using CesiumForUnity;
using Unity.Mathematics;
using ROS2;
using Unity.Collections;

public class PositionUpdate : MonoBehaviour
{
    private CesiumGlobeAnchor globeAnchor;
    private ROS2Node ros2Node;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        globeAnchor = GetComponent<CesiumGlobeAnchor>();
        double longitude = -76.71552978;
        double latitude = 39.26049307;
        double height = 100;
        globeAnchor.longitudeLatitudeHeight = new double3(longitude, latitude, height);
    }

    // Update is called once per frame
    void Update()
    {
        if (true)
        {
            //Read Coordinates given out of Cesium

            Unity.Mathematics.double3 currentCoordinates = globeAnchor.longitudeLatitudeHeight;
            double speed = 0.0001;
            currentCoordinates.x += speed * Time.deltaTime;
            currentCoordinates.y += speed * Time.deltaTime;

            globeAnchor.longitudeLatitudeHeight = currentCoordinates;
        }
    }
}
