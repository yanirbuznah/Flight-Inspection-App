# Flight-Inspection-App
A WPF application that co-responds to a Flight Gear Simulator app.</br>
This app shows the user the real-time flight information and inspects the different parameters of the flight :shipit:.</br> *This app was written in .NET 5.0*


## Getting Started
After downloading the project and the prerequisties, enter the application and insert IP&Port in order to connect to the server and then press "Connect". Afterwards you should pick a csv file (with flight information) and an **Anomaly Detector** (dll file) to show the anomalies. As soon as you load a csv file, the flight will begin, and you will see the slider moving representing the csv lines.


### Prerequisties

* [Download](https://www.flightgear.org) FlightGear Simulator.
* .NET 5.0
### Prerequisties for Developers
* WPF
* C#
* C++


### Dependencies

* [OxyPlot](https://oxyplot.readthedocs.io/en/latest/getting-started/hello-wpf-xaml.html)
* [HelixToolKit](https://www.nuget.org/packages/HelixToolkit.Wpf/)
* [CsvHelper](https://joshclose.github.io/CsvHelper/)


### Installing
Download the zip for this repository or use git on the termianl. The terminal command is:
```
git clone https://www.github.com/yanirbuznah/Flight-Inspection-App
```
Build the project, afterwards go to the Debug folder. Navigate to net5.0-windows folder and then open the **Flight Inspection app.exe**<br/>
```Path: Flight Inspection App\bin\Debug\net5.0-windows```


### Running The Flight-Inspection-App

<img src = "https://user-images.githubusercontent.com/56928005/114501619-ff45e280-9c32-11eb-9c75-fab44f9576ab.png" width="650" height="350"></br>
After running the .exe file, home-page screen will show up (as shown above).
There are two blanks you need to fill up in order to connect to server: IP & Port.
After clicking connect, press the "Start Now" button and wait for the next screen to show up.</br>
<img src="https://user-images.githubusercontent.com/56928005/114501645-0bca3b00-9c33-11eb-9c77-f9df4d203f3c.png" width="650" height="350"></br>
1. Pick a csv file in order to start your flight. The csv should contain the flight information.
2. Pick a dll file of the anomaly detector. You have variety of algorithms you can choose, and also you can add by yourself.
3. **Enter The 3D Model**
4. By clicking on a specific flight feature, the graphs will show the feature's progress during the flight.
5. The joystick exemplifies the aircraft movement. The left slider represents the throttle value, and the buttom slider represents the rudder value.
6. Anomalies that have been detected will be shown in this box. You will see the name and the timestamp when the anomaly happens. If you double click the anomaly, the tick on the slide bar will jump to the specific timestamp.


#### Features

* Csv selector: As soon as you pick a csv flight-information, that flight will start by reading line by line.
* Dll selector: Option to choose your/default algorithm that comes with the application, in order to detect anomalies and will be shown upon the main graph.
* Flight-parameters graphs: You can choose a flight parameter and then the graphs (the buttom ones) will show the progression of its value through the flight.
* Joystick: The movement is determined by the `Elevator`(Y-pos) and the `Aileron`(X-pos) values from the CSV column.
* Media-Player tools:
  - Play button: In-case you pressed the `Pause` or `Stop` button, you will have to press the `Play` button in-order to resume or start the flight again.
  - Pause button: Freezes the flight. We stop recieving lines from the csv untill the `Play` button is pressed.
  - Stop button: Freezes the flight, but unlike the `Pause` button, by pressing the `Play` button, the flight will start from the beginning.
  - Flight slider: Represents the line progression. You can hold the **Tick** and move it to any place on the slider, and the flight will jump to the correct csv line according to its location upon the slider.
  - Increase speed button: You can speed-up the flight by pressing the `+` button. Each click adds 0.1 to the current speed.
  - Decrease speed button: You can slow-down the flight by pressing the `-` button. Each click decreases 0.1 to the current speed.


#### Implementing your own dll 

- Make sure your dll file is inside the Plugin folder.
- Customize your dll file in a way that the main class there implements the IDetector interface.


### Project Structure

<img src="https://user-images.githubusercontent.com/56928005/114267766-44390180-9a06-11eb-8362-ff63283b49d6.jpeg" width="650" height="300">


- Following the MVVM architectural pattern, there's one main View-Model and sub View-Models, one for each user story.
  As shown in the UML diagram above, the main View-Model (called `FGVM`) implements the IViewModel interface, and the sub View-Models (such as `ControlBarVM`, `JoyStickVM`,...) are inheriting from him (as required in our project instructions).
- The Model is created in the MainWindow, and then passed as an argument to the main View-Model's constructor. Later on, the Model is passed to the rest of the sub View-Models.
- Our MainWindow initializes the main View-Model, and the Simulator screen initializes each sub View-Model in his constructor.
