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
        private long totalTime;
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

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

        public FlightAnalysisModel()
        {
        }


        private StreamWriter connectFG(TcpClient client)
        {
            client.Connect("localhost", 5400);
            return new StreamWriter(client.GetStream());

        }

        public void loadCSV(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            //  var lines = File.ReadAllLines("C:/Users/hoday/Downloads/reg_flight.csv");

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
                if (dictValues.ContainsKey(s))
                {
                    s += "1";
                }
                dictValues.Add(s, columns[i]);
            }
            

            TotalTime = records.Count() / 10;
        }

        public void loadXMl(string fileName)
        {
            columns = new List<List<double>>();

            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            // doc.Load("C:/Users/hoday/Downloads/playback_small.xml");
            XmlElement root = doc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("name");
            for (int i = 0; i < elemList.Count/2; i++)
            {
                keys.Add(elemList[i].InnerXml);
                columns.Add(new List<double>());

            }
        }

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

        public void startVideo()
        {
            IsPlaying = true;
            showFlight();
        }
        public void pauseVideo()
        {
            IsPlaying = false;
        }
        public void stopVideo()
        {
            IsPlaying = false;
            currentIndex = 0;
            CurrentTime = 0;
        }
        public void changeCurrentTime(int newTime)
        {
            CurrentTime = newTime;
            currentIndex = newTime * 10;
        }


        private int currentIndex =0;
        private void showFlight()
        {
            Thread t = new Thread(delegate ()
            {
                TcpClient client = new TcpClient();
                StreamWriter streamWriter = connectFG(client);
                while (isPlaying)
                {
                 streamWriter.WriteLine(records[currentIndex]);
                    //////////////////////////////
                    // airspeed - airspeed-indicator_indicated-speed-kt
                    // altimeter - altimeter_indicated-altitude-ft
                    // direction - heading-deg
                    // pitch - pitch_deg
                    // roll - roll_deg
                    // yaw - side-slip-deg
                    Altimeter = (float)dictValues["altimeter_indicated-altitude-ft"][currentIndex];
;                   Airspeed = (float)dictValues["airspeed-indicator_indicated-speed-kt"][currentIndex];
                    Direction = (float)dictValues["heading-deg"][currentIndex];
                    Pitch = (float)dictValues["pitch-deg"][currentIndex];
                    Roll = (float)dictValues["roll-deg"][currentIndex];
                    Yaw = (float)dictValues["side-slip-deg"][currentIndex];
                    /////////////////////////////////
                    Aileron = (float)dictValues["aileron"][currentIndex];
                    Elevator = (float)dictValues["elevator"][currentIndex];
                    Rudder = (float)dictValues["rudder"][currentIndex];
                    Throttle = (float)dictValues["throttle"][currentIndex];
                    /////////////////////////////////
                    int newSleep = (int)(100 / speed);
                    Thread.Sleep(newSleep);
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

        public void changeSpeed(float speed)
        {
            this.Speed = speed;
        }
        ///////////////////////////////////////////////////////////////////
        ///
        volatile private float altimeter;
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
        volatile private float airspeed;
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
        volatile private float direction;
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
        volatile private float pitch;
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
        volatile private float roll;
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
        volatile private float yaw;
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
        /// <summary>
        /// 
        /// </summary>
        volatile private float aileron;
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
        volatile private float elevator;
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
        volatile private float rudder;
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
        volatile private float throttle;
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
