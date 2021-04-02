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

    public class FlightFilesVM
    {
        // members of csv and xml
        string csvFile, xmlFile;
        IFlightAnalysisModel model;
        // Total time of the vm
        long vm_totalTime;
        FlightFilesVM()
        {
            model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "totalTime")
            {
                this.VM_totalTime = model.getTotalTime();
            }
        }
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
        public long VM_totalTime
        {
            get { return model.getTotalTime(); }
            set { }
        }
    }
}