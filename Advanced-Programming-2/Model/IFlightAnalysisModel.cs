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

        void changeSpeed(float speed);    }
}
