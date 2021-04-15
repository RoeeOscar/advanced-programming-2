# Flight Gear Desktop App
The project Flight Gear Desktop App is a Flight Simulator based on MVVM design pattern which is written in C#. 
The App shows data and indices about yhe flight that analyzed from the files of the flight.

There are 4 tabs in the App:  
**Welcome Window** - In the Welcome Window we can see a short explanation of the app, where is all the indices of the flight, how to get the plane fly.  
![image](https://user-images.githubusercontent.com/71708182/114770316-fb6da980-9d73-11eb-875b-89736f11b945.png)   

**Load Files Window** - The load files window is used to load the CSV and XML file for the app.  
![image](https://user-images.githubusercontent.com/71708182/114770276-f27cd800-9d73-11eb-8f85-b83ad0fb50cc.png)   

**Data Window** - On the data window we have all the details on the plane during the live flight - Altimeter, Airspeed, Direction, Yaw, Roll and Pitch. In addition, we can see the mouvements of the plane which is displayed by a joystick and sliders.   
![image](https://user-images.githubusercontent.com/71708182/114770379-0f191000-9d74-11eb-8406-47f239e80d55.png)   

**Graph Window** - In the graph window we can see multiple graphs of the attributes and their corelatives, the regression line and anomalies. In addition you have also a feature to load DLL file for detect anomalies and get the anomaly time steps during the flight.   
![image](https://user-images.githubusercontent.com/71708182/114905383-92446f80-9e21-11eb-8ac5-c215c7d2f2ec.png)   


The project is divided to 4 main parts:  
**1. Model** - there is an interface, and one class that impliments it, but all the view models use the interface, in order to keep the encapsulation.   
**2. View Model** - there are components that get notifications from the model about changes in data or get data.   
**3. Controls**- the View - components that present the data that binding to the view model.   
**4. Utilities** - additional classes to help calculate the regressions of the attributes.   
The first three parts are the impliment of the MVVM design pattern, and the last part is helper and external.   

In the forth tab, the user can upload dll file for detect anomalies. The dll need to impliment some methods so that the data will show in the App.   
**The DLL must contain the methods:**  
**Constructor** -  needs to get a FlightGear XML file path that has all the flight attributes inside.  
**LearnNormal** - needs to get a normal flight CSV file path. The dll will learn this file and get the normal thresolds and bounaries.  
**Detect** - needs to get an abnormal flight CSV file path. The method should compare the data to the normal data that has learned. For each two correlated features find if there is any anomaly.   
**DrawShape** - needs to return a List<DataPoint> so that the App can draw it.  
  
**Interface for the DLL file**  
```c#
public AnomalyDetector(string XMLFileName) - The constructor, get a XML file and parser it.   
public void learnNormal(string CSVFileName) - get CSV file of a normal flight and learns it to know what data is considered to normal.     
public List<Tuple<string, string, int>> detect(string CSVFileName) - get anomaly flight file and return anomalies in triples - First feature , Second feature (which is correlative to thr first) and TimeStep of the anomaly  
public List<DataPoint> drawShape(string graphName) - get the display graph and output list of points of the shape that we want to test the anomalies
```  

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


