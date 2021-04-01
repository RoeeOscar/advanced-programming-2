using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace Advanced_Programming_2.Model
{
    public interface IFlightAnalysisModel
    {
        void loadXMl(string fileName);
        void loadCSV(string fileName);
        DateTime getTotalTime();
    }
}
