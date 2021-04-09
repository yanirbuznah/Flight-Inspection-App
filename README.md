# Flight-Inspection-App
A WPF application that co-responds to the flight simulator app.
This app shows the user the real-time flight information. *This app was written in .NET 5.0*
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
![מסך אפליקציה ראשי](https://user-images.githubusercontent.com/56928005/113912864-4d4a8880-97e4-11eb-875f-d2ad7d022f12.png)<br/>

Now the features window popped up.In order to start the flight, you will have to insert a csv file with flight information by clicking on the folder and then choosing the correct path to your file.
Furthermore, there are multiplie features that can be shown on screen by pressing the feature name.
* The futures list (next to the blank of the chosen csv file).In this window you can *pick a flight information*, and you will see it upon the top left graph showing the information .
* The joystick, exemplifies the airplane movement (accordignly to x&y coordinates).The left slider to the joystick represents the throttle and the slider that is lcoated under the joystick represents the rudder.
* Below the joystick features, you can see more flight information such as: Altitude,Speed,Flight direction etc..
* 3D Model // TODO
* And ofc the slider bar that you can speed up the csv line inputs, or lower the speed.And all the normal media-buttons we are used to.
