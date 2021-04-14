# Flight Gear Desktop App
The project Flight Gear Desktop App is a Flight Simulator based on MVVM design pattern which is written in C#.  
There are 4 tabs in the App:  
Welcome Tab:  
![image](https://user-images.githubusercontent.com/71708182/114770316-fb6da980-9d73-11eb-875b-89736f11b945.png)  
**Welcome Window** - In the Welcome Window we can see a short explanation of the app, where is all the details of the flight, how to get the plane fly.   
Load Files Tab:  
**Load Files Window** - The load files window is used to load the CSV and XML file for the app.  
![image](https://user-images.githubusercontent.com/71708182/114770276-f27cd800-9d73-11eb-8f85-b83ad0fb50cc.png)
Data Tab:  
**Data Window** - On the data window we have all the details on the plane during the live flight - Altimete, Airspeed, Direction, Yaw, Roll and Pitch. In addition, we can see the mouvements of the plane which is displayed by a joystick.  
![image](https://user-images.githubusercontent.com/71708182/114770379-0f191000-9d74-11eb-8406-47f239e80d55.png)
Graph Tab:  
**Graph Window** - In the graph window we can see multiple graphs of the attributes and regression line and anomalies. In addition you have also a feature to load DLL file.
![image](https://user-images.githubusercontent.com/71708182/114770428-1cce9580-9d74-11eb-8ee5-d25d748291bd.png)  

The project is divided to 4 main parts:
**1. Model** - there is an interface, and one class that impliments it, but all the view models use the interface, in order to keep the encapsulation.
**2. View Model** - there are components that get notifications from the model about changes in data or get data.
**3. Controls**- the View - components that present the data that binding to the view model.
**4. Utilities** - additional classes to help calculate the regressions of the attributes.  

**The DLL must contain the methods:**  
**Constructor** - The Constructor input a FlightGear XML file path that has all the flight features inside.  
**LearnNormal** - needs to get a normal flight CSV file path. Your dll will learn this file and get the normal thresolds and bounaries.  
**Detect** - needs to get an abnormal flight CSV file path. The method should compare the data to the normal data that has learned. For each two correlated features find if there is any anomaly.   
**DrawShape** - nneds to return a List<DataPoint> so that the App can draw it.  
  
**Interface for the DLL file**  
public AnomalyDetector(string XMLFileName) - Constructor that input XML file and parser  
public void learnNormal(string CSVFileName) - Input CSV file and learn him  
public List<Tuple<string, string, int>> detect(string CSVFileName) - Input anomalie file and output anomalies in triplets - First feature , Second feature and TimeStep  
public List<DataPoint> drawShape(string graphName) - Input the display graph and output list of points of the shape that we want to test the anomalies  
  
**UML Diagrams**  
There is an interface of the model, that each item in the view model contains an intance of modal that impliments that interface.
Each view that has a conneactio to the model, contains an instance od view model.
![image](https://user-images.githubusercontent.com/71708182/114775890-7fc32b00-9d7a-11eb-9f44-0b623f6cb2ec.png)

There are components in the view, that only present but don't need to bo connected to the view model and the model, so they are not part of the MVVM structure:
![image](https://user-images.githubusercontent.com/71708182/114775814-65894d00-9d7a-11eb-89ad-a7e30da5a794.png)

There is a class that impliments the interface of the model:
![image](https://user-images.githubusercontent.com/71708182/114776014-a5e8cb00-9d7a-11eb-8531-9167a88e7a2e.png)

There are utilities that help in the present of the graphs:
![image](https://user-images.githubusercontent.com/71708182/114776135-cca70180-9d7a-11eb-8a88-458089b5d601.png)




**You can find a demo of the simulator here : https://www.youtube.com/watch?v=HTFPhecRl94**


