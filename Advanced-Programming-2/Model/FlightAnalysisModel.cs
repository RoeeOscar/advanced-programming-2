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
            columns = new List<List<double>>(lines.Length);
            foreach (var line in lines)
            {
                columns.Add(new List<double>());
                List<string> words = line.Split(',').ToList();
                int i = 0;
                foreach (var word in words)
                {
                    columns[i].Add(double.Parse(word));
                    i++;

                }
                records.Add(line);

            }

            TotalTime = records.Count() / 10;
        }

        public void loadXMl(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            // doc.Load("C:/Users/hoday/Downloads/playback_small.xml");
            XmlElement root = doc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName("name");
            for (int i = 0; i < elemList.Count; i++)
            {
                keys.Add(elemList[i].InnerXml);
                dictValues.Add(keys[i], columns[i]);
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
       


    }

}
