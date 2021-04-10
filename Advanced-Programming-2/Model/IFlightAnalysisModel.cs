using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.ComponentModel;
using OxyPlot;

namespace Advanced_Programming_2.Model
{
    public interface IFlightAnalysisModel : INotifyPropertyChanged
    {
        // Loading flight files functions.
        void loadXMl(string fileName);
        void loadCSV(string fileName);

        // Media player properties.
        long TotalTime{ set; get; }
        bool IsPlaying { set; get; }
        int CurrentTime { set; get; }
        float Speed { get; set; }

        // Media player functions.
        void startVideo();
        void pauseVideo();
        void stopVideo();
        void changeCurrentTime(int newTime);
        void changeSpeed(float speed);

        // Flight data properties.
        float Altimeter { set; get; }
        float Airspeed { set; get; }
        float Direction { set; get; }
        float Pitch { get; set; }
        float Roll { get; set; }
        float Yaw { get; set; }

        // Joystick properties.
        float Aileron { set; get; }
        float Elevator { set; get; }
        float Rudder { set; get; }
        float Throttle { set; get; }

        // Graphs propreties and methods.
        List<string> Attributes { set; get; }
        List<DataPoint> GraphPoints { set; get; }
        void changeGraph(string attribute);
        string GraphName { set; get; }
        List<DataPoint> CorrelatedGraphPoints { get; set; }
        string CorrelatedGraphName { get; set; }
        List<DataPoint> RegressionLine { get; set; }
        List<DataPoint> Last30Points { get; set; }
    }



}
