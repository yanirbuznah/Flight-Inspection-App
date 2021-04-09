# Flight-Inspection-App
A WPF application that co-responds to the flight simulator app.</br>
This app shows the user the real-time flight information and inspects the different parameters of the flight :shipit:.</br> *This app was written in .NET 5.0*
## Getting Started
These instructions will get the flight simulator up and running along with the flight inspection application.
### Prerequisties

* [Download](https://www.flightgear.org) FlightGear Simulator.
* .NET 5.0
### Prerequisties for Developers
* WPF
* C#
* C++
### Installing
Download the zip for this repository or use git on the termianl. The terminal command is :
```
git clone https://www.github.com/yanirbuznah/Flight-Inspection-App
```
Build the project, afterwards go to the Debug folder. Navigate to net5.0-windows folder and then open the **Flight Inspection app.exe**<br/>
```Path: Flight Inspection App\bin\Debug\net5.0-windows```

### Running The Flight-Inspection-App
![openScreen](https://user-images.githubusercontent.com/56928005/114165321-fdc7a200-9934-11eb-8bcf-17ff7ebb48a8.png)<br/>
When you open the Flight Inspection app.exe you will see the home-page screen as shown above.<br/>
There are two blanks you need to fill up in order to connect to server : IP & Port.
After clicking connect, press the "Start now" button and wait for the next screen to show up.
![SimulatorScreen](https://user-images.githubusercontent.com/56928005/114172473-62d3c580-993e-11eb-9515-5976ba107106.png)<br/>

1. Pick a csv file in order to start your flight. The csv should contain a flight information description.
2. Pick a dll file of the anomaly detector. You have variety of algorithms you can choose, and also you can add by yourself.
3. **Enter The 3d Model**
4. By clicking on a specific flight information, the graphs will show the progress during the flight.
5. The joystick exemplifies the airplane movement. The left slider represents the throttle progression, and the buttom slider represents the rudder value.

#### Implementing your own dll 
- Make sure your dll file is inside the Plugin folder.
- Customize your dll file in a way that the main class there implements the IDetector interface.
