using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Collections.Generic;


namespace Advanced_Programming_2.Model
{
    class FlightAnalysisModel : IFlightAnalysisModel
    {
        Dictionary<string, List<double>> dictValues = new Dictionary<string, List<double>>();
        List<List<byte>> bytesValues = new List<List<byte>>();
        long totalTime;

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

        public long getTotalTime()
        {
            return totalTime;
        }

        public void loadCSV(string fileName)
        {
            
        }

        public void loadXMl(string fileName)
        {
            
        }
    }

}
