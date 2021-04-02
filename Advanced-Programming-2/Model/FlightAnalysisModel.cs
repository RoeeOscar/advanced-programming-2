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
namespace Advanced_Programming_2.Model
{
    class FlightAnalysisModel : IFlightAnalysisModel
    {
        Dictionary<string, List<double>> dictValues = new Dictionary<string, List<double>>();
        List<string> keys = new List<string>();
        List<List<double>> columns;
        List<byte[]> bytesValues = new List<byte[]>();
        private long totalTime;
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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

        private StreamWriter connectFG()
        {
            TcpClient client = new TcpClient();
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
            }
            totalTime = bytesValues.Count();
            NotifyPropertyChanged("totalTime");

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

        public void showFlight()
        {
            StreamWriter streamWriter = connectFG();
            for (int i = 0; i < keys.Count(); i++)
            {
                dictValues.Add(keys[i], columns[i]);
                streamWriter.WriteLine(bytesValues[i]);
                Thread.Sleep(100);
            }
            streamWriter.Close();
        }


    }

}
