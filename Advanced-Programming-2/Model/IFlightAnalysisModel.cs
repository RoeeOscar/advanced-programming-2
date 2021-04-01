using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Programming_2
{
    public interface IFlightAnalysisModel
    {
        void loadXMl(string fileName);
        void loadCSV(string fileName);
        DateTime getTotalTime();
    }
}
