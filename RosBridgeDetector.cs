/**
File: RosBridgeDetector.cs
Overview: The code is in testing currently to ensure that a object system can transport from Ros# to a Cesium 1:1 globe simulation
Project: AirPlane Simulation
Date: October 22th, 2026
Creator: Process Adhikari
*/


using UnityEngine;
using Unity.Mathematics;
using RosSharp.RosBridgeClient;
using NavSatFix = RosSharp.RosBridgeClient.MessageTypes.Sensor.NavSatFix;

public class RosBridgeDetector : UnitySubscriber<NavSatFix>
{
    //Class to recieve the data from RossBridgeClient into this code
    private double3 lastestPosition;

    //Lock is placed here to ensure later on that the only data being fetched by BodyUpdater.cs is only numerical coordinate data.
    private readonly object dataLock = new object();
    private bool NewCoordsFetched = false;
 
    public bool TryGetLatestPosition(out double3 position)
    {
        lock(dataLock)
        {
            //This is what we need to fetch.
            position = lastestPosition;
            if (NewCoordsFetched)
            {
                NewCoordsFetched = false;
                return true; 
            }
            return false;
        }
    }
protected override void ReceiveMessage(NavSatFix message) //This is where the actual data message will be recieved NavSatFix is refrenced as message
    {
    if (double.IsNaN(message.longitude) || double.IsNaN(message.latitude) || double.IsNaN(message.altitude))return;
    
    lock (dataLock){
        //Fetch the messages position coordinates
        lastestPosition = new double3(message.longitude, message.latitude, message.altitude);
        //If the data is recieved then it can be used in update
        NewCoordsFetched = true;
    }
    }
}