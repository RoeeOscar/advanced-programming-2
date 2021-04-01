using Advanced_Programming_2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace Advanced_Programming_2.ViewModel
{

    class FlightFilesVM
    {
        string csvFile, xmlFile;
        IFlightAnalysisModel model;
        public string VM_csvFileName
        {
            get
            {
                return csvFile;
            }
            set
            {
                csvFile = value;
                model.loadCSV(csvFile);
            }
        }
        public string VM_xmlFileName
        {
            get
            {
                return xmlFile;
            }
            set
            {
                xmlFile = value;
                model.loadXMl(xmlFile);
            }
        }
    }
}