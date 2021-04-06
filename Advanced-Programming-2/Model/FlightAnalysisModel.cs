using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;

//using System.Windows.Forms;
namespace Advanced_Programming_2.Model
{
    class FlightAnalysisModel : IFlightAnalysisModel
    {
        List<string> records = new List<string>();
        Dictionary<string, List<double>> dictValues = new Dictionary<string, List<double>>();
        List<string> keys = new List<string>();
        List<List<double>> columns;
        public event PropertyChangedEventHandler PropertyChanged;

        // Default constructor.
        public FlightAnalysisModel()
        {
        }

        // Sending notifications function.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Connecting the client to FlightGear.
        private StreamWriter connectFG(TcpClient client)
        {
            client.Connect("localhost", 5400);
            return new StreamWriter(client.GetStream());
        }

        // Loading the CSV File.
        public void loadCSV(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            foreach (var line in lines)
            {
               List<string> words = line.Split(',').ToList();
                int i = 0;
                foreach (var word in words)
                { 
                        columns[i].Add(double.Parse(word));
                        i++;
                }
                records.Add(line);
            }

            for (int i = 0; i < keys.Count(); i++)
            {
                string s = keys[i];
                // Differentiate same keys.
                if (dictValues.ContainsKey(s))
                {
                    s += "1";
                }
                dictValues.Add(s, columns[i]);
            }
            // There are 10 frames per second.
            TotalTime = records.Count() / 10;
        }

        // Loading the XML File.
        public void loadXMl(string fileName)
        {
            columns = new List<List<double>>();
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlElement root = doc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("name");
            for (int i = 0; i < elemList.Count/2; i++)
            {
                keys.Add(elemList[i].InnerXml);
                columns.Add(new List<double>());
            }
        }

        // Total video time data member & property (in seconds).
        private long totalTime = 0;
        public long TotalTime
        {
            get
            {
                return totalTime;
            }
            set
            {
                totalTime = value;
                NotifyPropertyChanged("TotalTime");
            }
        }

        // Is playing boolean data member and property. True if the video is playing, otherwise false.
        volatile private bool isPlaying = false;
        public bool IsPlaying
        {
            set
            {
                isPlaying = value;
                NotifyPropertyChanged("IsPlaying");
            }
            get
            {
                return isPlaying;
            }
        }

        // Current time data member & property (in seconds).
        volatile private int currentTime = 0;
        public int CurrentTime
        {
            set
            {
                currentTime = value;
                NotifyPropertyChanged("CurrentTime");
            }
            get
            {
                return currentTime;
            }
        }

        // Starting video function.
        public void startVideo()
        {
            IsPlaying = true;
            showFlight();
        }
        
        // Pause video function.
        public void pauseVideo()
        {
            IsPlaying = false;
        }

        // Stopping video function.
        public void stopVideo()
        {
            IsPlaying = false;
            currentIndex = 0;
            CurrentTime = 0;
        }

        // Changing current time function.
        public void changeCurrentTime(int newTime)
        {
            CurrentTime = newTime;
            currentIndex = newTime * 10;
        }

        // Data member for the current index in the records lists (the line to send to the server).
        private int currentIndex =0;

        // Updating the data after sending a data line to the server.
        private void updateData()
        {
            Altimeter = (float)dictValues["altimeter_indicated-altitude-ft"][currentIndex];
            Airspeed = (float)dictValues["airspeed-indicator_indicated-speed-kt"][currentIndex];
            Direction = (float)dictValues["heading-deg"][currentIndex];
            Pitch = (float)dictValues["pitch-deg"][currentIndex];
            Roll = (float)dictValues["roll-deg"][currentIndex];
            Yaw = (float)dictValues["side-slip-deg"][currentIndex];
            Aileron = (float)dictValues["aileron"][currentIndex];
            Elevator = (float)dictValues["elevator"][currentIndex];
            Rudder = (float)dictValues["rudder"][currentIndex];
            Throttle = (float)dictValues["throttle"][currentIndex];
        }

        // Showing the flight video.
        private void showFlight()
        {
            Thread t = new Thread(delegate ()
            {
                TcpClient client = new TcpClient();
                StreamWriter streamWriter = connectFG(client);
                while (isPlaying)
                {
                 streamWriter.WriteLine(records[currentIndex]);
                    updateData();
                    Thread.Sleep((int)(100 / speed));
                    currentIndex++;
                    if (currentIndex % 10 == 0)
                    {
                        CurrentTime = currentIndex / 10;
                    }
                }
                streamWriter.Close();
                client.Close();

            });
            t.Start();

        }

        // Data member and property for the video speed.
        volatile private float speed = 1;
        public float Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
                NotifyPropertyChanged("Speed");
            }
        }

        // Changing video speed function.
        public void changeSpeed(float speed)
        {
            this.Speed = speed;
        }

        // Data members and properties for flight and joystick data.

        private float altimeter;
        public float Altimeter 
        {
            get
            {
                return altimeter;
            }
            set
            {
                altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
        }
        private float airspeed;
        public float Airspeed 
        { 
            get
            {
                return airspeed;
            }
            set
            {
                airspeed = value;
                NotifyPropertyChanged("Airspeed");
            }
                }
        private float direction;
        public float Direction 
        {
            get
            {
                return direction;
            }
            set
            {
                direction = value;
                NotifyPropertyChanged("Direction");
            }
        }
        private float pitch;
        public float Pitch
        {
            get
            {
                return pitch;
            }
            set
            {
                pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }
        private float roll;
        public float Roll
        {
            get
            {
                return roll;
            }
            set
            {
                roll = value;
                NotifyPropertyChanged("Roll");
            }
        }
        private float yaw;
        public float Yaw
        {
            get
            {
                return yaw;
            }
            set
            {
                yaw = value;
                NotifyPropertyChanged("Yaw");
            }
        }

        private float aileron;
        public float Aileron {
            set
            {
                aileron = value;
                NotifyPropertyChanged("Aileron");
            }
            get
            {
                return aileron;
            }
        }
        private float elevator;
        public float Elevator
        {
            set
            {
                elevator = value;
                NotifyPropertyChanged("Elevator");
            }
            get
            {
                return elevator;
            }
        }
        private float rudder;
        public float Rudder
        {
            set
            {
                rudder = value;
                NotifyPropertyChanged("Rudder");
            }
            get
            {
                return rudder;
            }
        }
        private float throttle;
        public float Throttle
        {
            set
            {
                throttle = value;
                NotifyPropertyChanged("Throttle");
            }
            get
            {
                return throttle;
            }
        }
    }
}
