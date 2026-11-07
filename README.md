# Unity-Simulation-Code
Code that helps initialize the Unity to Ros# connection. 

You have 4 script codes here's what you need to do.

Setup Ros Connector:
Seconds Timeout - 10
Serializer - Microsoft
Protocol - Web Socket Sharp
Ros Bridge Server Url - ws://localhost:9090
ROS Version - ROS2

Setup ImuDetection:
Topic - /mavros/imu/data
Time Step - 0 

Setup Ros Bridge Detector:
Topic - /mavros/global_position/global
Time Step - 0

Setup BodyUpdater:
Ros Output - Drag "Ros Bridge Detector (Script) onto this 
Imu output - Drag "Imu Detection (Script)" onto this

Done for now!
