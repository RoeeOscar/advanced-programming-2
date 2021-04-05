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

namespace Advanced_Programming_2.Model
{
    public interface IFlightAnalysisModel : INotifyPropertyChanged
    {
        void loadXMl(string fileName);
        void loadCSV(string fileName);

        // Properties
        long TotalTime{ set; get; }
        bool IsPlaying { set; get; }
        int CurrentTime { set; get; }

        void startVideo();
        void pauseVideo();
        void stopVideo();
        void changeCurrentTime(int newTime);

        float Speed { get; set; }
        void changeSpeed(float speed);


        float Altimeter { set; get; }
        float Airspeed { set; get; }
        float Direction { set; get; }
        float Pitch { get; set; }
        float Roll { get; set; }
        float Yaw { get; set; }

        ///
        float Aileron { set; get; }
        float Elevator { set; get; }
        float Rudder { set; get; }
        float Throttle { set; get; }

    }



}
