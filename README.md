# Flight-Inspection-App
A WPF application that co-responds to a Flight Gear Simulator app.</br>
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
<img src = "https://user-images.githubusercontent.com/56928005/114165321-fdc7a200-9934-11eb-8bcf-17ff7ebb48a8.png" width="650" height="350"></br>
After running the .exe file, home-page screen will show up (as shown above).
There are two blanks you need to fill up in order to connect to server : IP & Port.
After clicking connect, press the "Start Now" button and wait for the next screen to show up.</br>
<img src="https://user-images.githubusercontent.com/56928005/114229633-c383f200-9980-11eb-810e-1d96fb6e8c4c.png" width="650" height="350"></br>
1. Pick a csv file in order to start your flight. The csv should contain a flight information description.
2. Pick a dll file of the anomaly detector. You have variety of algorithms you can choose, and also you can add by yourself.
3. **Enter The 3D Model**
4. By clicking on a specific flight feature, the graphs will show the feature's progress during the flight.
5. The joystick exemplifies the aircraft movement. The left slider represents the throttle progression, and the buttom slider represents the rudder value.

#### Implementing your own dll 
- Make sure your dll file is inside the Plugin folder.
- Customize your dll file in a way that the main class there implements the IDetector interface.

### Project Structure
- Following the MVVM architectural pattern, there's one main View-Model and sub View-Models, one for each user story.
  As shown in the UML diagram above, the main View-Model (called "FGVM") implements the IViewModel interface, and the sub View-Models (e.g. "ControlBarVM", "JoyStickVM",..) are inheriting from him (as required in our project instructions).
- The Model is created in the MainWindow, and then passed as an argument to the main View-Model's constructor. Later on, the Model is passed to the rest of the sub View-Models.
- Our MainWindow initializes the main View-Model, and the Simulator screen initializes each sub View-Model in his constructor.
