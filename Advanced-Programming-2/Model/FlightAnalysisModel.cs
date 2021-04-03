﻿using System;
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
        List<string> lines1 = new List<string>();

        Dictionary<string, List<double>> dictValues = new Dictionary<string, List<double>>();
        List<string> keys = new List<string>();
        List<List<double>> columns;
        List<byte[]> bytesValues = new List<byte[]>();
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

        public static void communicateWithFG()
        {
            TcpClient client = new TcpClient();
            client.Connect("localhost", 5400);

            StreamWriter streamWriter = new StreamWriter(client.GetStream());
            StreamReader streamReader = new StreamReader("C:/Users/roee0/source/repos/Advanced-Programming-2/Advanced-Programming-2/Model/reg_flight.csv");

            string strline = "";


            while (!streamReader.EndOfStream)
            {
                strline = streamReader.ReadLine();
                byte[] bytesData = System.Text.Encoding.ASCII.GetBytes(strline);
                streamWriter.WriteLine(strline);
                Thread.Sleep(100);
            }
            streamWriter.Close();
            streamReader.Close();

        }

        private StreamWriter connectFG(TcpClient client)
        {
            client.Connect("localhost", 5400);
            return new StreamWriter(client.GetStream());

        }

        public long getTotalTime()
        {
            return totalTime;
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
                }
                i++;

                byte[] bytesData = Encoding.ASCII.GetBytes(line);
                bytesValues.Add(bytesData);

                lines1.Add(line);

            }
            //totalTime = bytesValues.Count() / 10;
            //NotifyPropertyChanged("totalTime");
            CurrentTime = 0;
            TotalTime = bytesValues.Count() / 10;
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

        volatile private int currentTime;
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
                    streamWriter.WriteLine(lines1[currentIndex]);

                    Thread.Sleep(100);
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
    }

}
