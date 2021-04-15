# Flight Inspection App
The project Flight Gear Desktop App is a Flight Simulator based on MVVM design pattern which is written in C#. 
The App shows data and indices about yhe flight that analyzed from the files of the flight.

There are 4 tabs in the App:  
**Welcome Window** - In the Welcome Window we can see a short explanation of the app, where is all the indices of the flight, how to get the plane fly.  
![image](https://user-images.githubusercontent.com/71708182/114770316-fb6da980-9d73-11eb-875b-89736f11b945.png)   

**Load Files Window** - The load files window is used to load the CSV and XML file for the app.  
![image](https://user-images.githubusercontent.com/71708182/114770276-f27cd800-9d73-11eb-8f85-b83ad0fb50cc.png)   

**Data Window** - On the data window we have all the details on the plane during the live flight - Altimeter, Airspeed, Direction, Yaw, Roll and Pitch. In addition, we can see the mouvements of the plane which is displayed by a joystick and sliders.   
![image](https://user-images.githubusercontent.com/71708182/114770379-0f191000-9d74-11eb-8406-47f239e80d55.png)   

**Graph Window** - In the graph window we can see multiple graphs of the attributes and their correlatives, the regression line and anomalies. In addition you have also a feature to load DLL file for detect anomalies and get the anomaly time steps during the flight.   
![image](https://user-images.githubusercontent.com/71708182/114905383-92446f80-9e21-11eb-8ac5-c215c7d2f2ec.png)   


The project is divided to 4 main parts:  
**1. Model** - There is an interface, and one class that implements it, but all the view models use the interface, in order to keep the encapsulation.   
**2. View Model** - There are components that get notifications from the model about changes in data or get data.   
**3. Controls**- The View - components that present the data that binding to the view model.   
**4. Utilities** - Additional classes to help calculate the regressions of the attributes.   
The first three parts are the implement of the MVVM design pattern, and the last part is helper and external.   

In the fourth tab, the user can upload dll file for detect anomalies. The dll need to implement some methods so that the data will show in the App.   
**The DLL must contain the methods:**  
**Constructor** -  needs to get a FlightGear XML file path that has all the flight attributes inside.  
**LearnNormal** - needs to get a normal flight CSV file path. The dll will learn this file and get the normal thresolds and bounaries.  
**Detect** - needs to get an abnormal flight CSV file path. The method should compare the data to the normal data that has learned. For each two correlated features find if there is any anomaly.   
**DrawShape** - needs to return a List<DataPoint> so that the App can draw it.  
  
**Interface for the DLL file**  
```c#
public AnomalyDetector(string XMLFileName); // The constructor, get a XML file and parser it.   
public void learnNormal(string CSVFileName); // get CSV file of a normal flight and learns it to know what data is considered to normal.  
public List<Tuple<string, string, int>> detect(string CSVFileName); // get anomaly flight file and return anomalies in triples - First feature , Second feature (which is relative to the first) and TimeStep of the anomaly  
public List<DataPoint> drawShape(string graphName); // get the display graph and output list of points of the shape that we want to test the anomalies
```  

## UML Diagrams  
To see more information about the UML in this project you can click [here](https://github.com/RoeeOscar/advanced-programming-2/blob/master/UML%20Diagrams.pdf).

## Requirements Downloads and Installation
1. [FlightGear](https://www.flightgear.org/download/)  
1.1 After downloading, Open FlightGear    
1.2 Go to Settings  
1.3 Go to Additional Settings  
1.4 In the Text box write the following commands:  
    ```
    --generic=socket,in,10,127.0.0.1,5400,tcp,playback_small  
    --fdm=null
    ```  
2. [Oxyplot](https://www.nuget.org/packages/OxyPlot.Wpf/2.1.0-Preview1)  
3. [CircularGauge](https://www.nuget.org/packages/CircularGauge)  




**You can find a demo of the simulator here : https://www.youtube.com/watch?v=HTFPhecRl94**


