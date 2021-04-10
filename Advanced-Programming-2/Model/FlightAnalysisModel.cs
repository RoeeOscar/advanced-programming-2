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
//using System.Windows.Forms;
using OxyPlot;
using Advanced_Programming_2.Utilities;

//using System.Windows.Forms;
namespace Advanced_Programming_2.Model
{
    class FlightAnalysisModel : IFlightAnalysisModel
    {
        List<string> records = new List<string>();
        Dictionary<string, List<double>> dictValues = new Dictionary<string, List<double>>();
        List<string> keys = new List<string>();
        List<List<double>> columns;
        Correlations correlations;
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
        #region Loading files
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

            correlations = new Correlations(dictValues);
        }

        // Loading the XML File.
        public void loadXMl(string fileName)
        {
            columns = new List<List<double>>();
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            XmlElement root = doc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("name");
            for (int i = 0; i < elemList.Count / 2; i++)
            {
                keys.Add(elemList[i].InnerXml);
                columns.Add(new List<double>());
            }
            Attributes = keys;
        }
        #endregion  
        #region Properties
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
        // Data members and properties for airspeed.
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
        // Data members and properties for direction.
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
        // Data members and properties for pitch.
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
        // Data members and properties for roll.
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
        // Data members and properties for yaw.
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
        // Data members and properties for aileron.
        private float aileron;
        public float Aileron
        {
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
        // Data members and properties for elevator.
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
        // Data members and properties for rudder.
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
        // Data members and properties for throttle.
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


        #endregion
        #region Videos
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
        #endregion
        // Data member for the current index in the records lists (the line to send to the server).
        private int currentIndex = 0;

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
                while (isPlaying && currentIndex < records.Count())
                {
                    streamWriter.WriteLine(records[currentIndex]);
                    updateData();
                    GraphPoints = new List<DataPoint>();
                    CorrelatedGraphPoints = new List<DataPoint>();
                    RegressionLine = new List<DataPoint>();
                    Last30Points = new List<DataPoint>();

                    if (graphName != null)
                    {
                        for (int i = 0; i < currentIndex; i++)
                        {
                            GraphPoints.Add(new DataPoint(((double)i / 10), dictValues[graphName][i]));
                            CorrelatedGraphPoints.Add(new DataPoint(((double)i / 10), dictValues[correlatedGraphName][i]));
                        }
                    }
                    List<CorrelatedFeatures> cf = correlations.getCorrelations();
                    foreach (CorrelatedFeatures x in cf)
                    {
                        if (x.getFeature1() == GraphName)
                        {
                            RegressionLine.Add(new DataPoint(x.getMinXY().getX(), x.getMinXY().getY()));
                            RegressionLine.Add(new DataPoint(x.getMaxXY().getX(), x.getMaxXY().getY()));

                        }
                    }
                    if (graphName != null)
                    {
                        for (int i = currentIndex - 300; i <= currentIndex; i++)
                        {
                            if (i >= 0)
                            {
                                Last30Points.Add(new DataPoint(dictValues[graphName][i], dictValues[correlatedGraphName][i]));
                            }

                        }
                    }

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

        private List<string> attributes;
        public List<string> Attributes
        {
            get
            {
                return attributes;
            }
            set
            {
                attributes = value;
                NotifyPropertyChanged("Attributes");

            }

        }

        private List<DataPoint> graphPoints;
        public List<DataPoint> GraphPoints
        {
            get
            {
                return graphPoints;
            }
            set
            {
                graphPoints = value;
                NotifyPropertyChanged("GraphPoints");

            }
        }

        volatile string graphName;

        public string GraphName
        {
            get
            {
                return graphName;
            }
            set
            {
                graphName = value;
                NotifyPropertyChanged("Graph");
            }
        }
        public void changeGraph(string attribute)
        {

            GraphName = attribute;
            NotifyPropertyChanged("GraphName");
            List<CorrelatedFeatures> cf = correlations.getCorrelations();
            foreach (CorrelatedFeatures x in cf)
            {
                if (x.getFeature1() == GraphName)
                {
                    CorrelatedGraphName = x.getFeature2();
                }
            }
        }
        /// <summary>
        /// /////////////////////////
        /// </summary>

        private List<DataPoint> correlatedGraphPoints;
        public List<DataPoint> CorrelatedGraphPoints
        {
            get
            {
                return correlatedGraphPoints;
            }
            set
            {
                correlatedGraphPoints = value;
                NotifyPropertyChanged("CorrelatedGraphPoints");

            }
        }

        volatile string correlatedGraphName;

        public string CorrelatedGraphName
        {
            get
            {
                return correlatedGraphName;
            }
            set
            {
                correlatedGraphName = value;
                NotifyPropertyChanged("CorrelatedGraphName");
            }
        }
        volatile List<DataPoint> regressionLine;
        public List<DataPoint> RegressionLine
        {
            get
            {
                return regressionLine;
            }
            set
            {
                regressionLine = value;
                NotifyPropertyChanged("RegressionLine");

            }
        }

        volatile List<DataPoint> last30Points;
        public List<DataPoint> Last30Points
        {
            get
            {
                return last30Points;
            }
            set
            {
                last30Points = value;
                NotifyPropertyChanged("Last30Points");

            }
        }
    }
    
}
