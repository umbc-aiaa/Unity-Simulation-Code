/**
File: ImuDetection.cs
Overview: The code is in testing currently to ensure that a object system can fetch the pitch roll and yaw of the plane 
Project: AirPlane Simulation
Date: October 22th, 2026
Creator: Process Adhikari
*/


using UnityEngine;
using Unity.Mathematics;
using RosSharp.RosBridgeClient;
using Imu = RosSharp.RosBridgeClient.MessageTypes.Sensor.Imu;

public class ImuDetection : UnitySubscriber<Imu>
{
    //Class to recieve the data from RossBridgeClient into this code
    private quaternion latestAngles = quaternion.identity;

    //A lock is needed to ensure that Update() is not reading changing data from RecieveMessage() aswell as making sure CesiumPosition is initialized before Ros#
    private readonly object dataLock = new object();
    private bool NewAnglesFetched = false;
 
    public bool TryGetLatestAngles(out quaternion angles)
    {
        lock(dataLock)
        {
            
            angles = latestAngles;

            if (NewAnglesFetched)
            {
                NewAnglesFetched = false;
                return true; 
            }
            return false;
        }
    }
protected override void ReceiveMessage(Imu message) //This is where the actual data message will be recieved NavSatFix is refrenced as message
    {
    if (message.orientation == null) return;
    
    lock (dataLock){
        //Fetch the messages position coordinates
        latestAngles = new quaternion((float)message.orientation.x, (float)message.orientation.y, (float)message.orientation.z, (float)message.orientation.w);
        //If the data is recieved then it can be used in update
        NewAnglesFetched = true;
    }
    }
}