using Advanced_Programming_2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Advanced_Programming_2.ViewModel
{
    public class MediaPlayerVM : INotifyPropertyChanged
    {
        private IFlightAnalysisModel model;
        // Total time of the vm
        private string VM_totalTimeFormat;
        public MediaPlayerVM(IFlightAnalysisModel model)
        {
            this.model = model;
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                {
                    NotifyPropertyChanged("VM_"+e.PropertyName);

                    if (e.PropertyName=="TotalTime")
                    {
                        TimeSpan time = TimeSpan.FromSeconds(model.TotalTime);
                        VM_TotalTimeFormat = String.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
                    }

                    if (e.PropertyName == "CurrentTime")
                    {

                        TimeSpan time = TimeSpan.FromSeconds(model.CurrentTime);
                        VM_CurrentTimeFormat = String.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
                    }

                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName="")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public long VM_TotalTime
        {
            get
            {
                return model.TotalTime;
            }
        }
        public string VM_TotalTimeFormat
        {
            get
            {
                return VM_totalTimeFormat;
            }
            set
            {
                VM_totalTimeFormat = value;
                NotifyPropertyChanged("VM_TotalTimeFormat");

            }

        }

        private bool VM_isPlaying;
        public bool VM_IsPlaying
        {
            get
            {
                return model.IsPlaying;
            }
            set
            {
                VM_isPlaying = value;
            }
        }

        private int VM_currentTime;
        public int VM_CurrentTime
        {
            get
            {
                return model.CurrentTime;
            }
            set
            {
                VM_currentTime = value;
                model.changeCurrentTime(VM_currentTime);
            }
        }
        public void startVideo()
        {
            model.startVideo();
        }

        public void pauseVideo()
        {
            model.pauseVideo();
        }

        public void stopVideo()
        {
            model.stopVideo();
        }

        private string VM_currentTimeFormat;
        public string VM_CurrentTimeFormat
        {
            get
            {
                return VM_currentTimeFormat;
            }
            set
            {
                VM_currentTimeFormat = value;
                NotifyPropertyChanged("VM_CurrentTimeFormat");
            }

        }

        public void changeCurrentTime(int newTime)
        {
            model.changeCurrentTime(newTime);
        }

        private float VM_speed = 1;
        public float VM_Speed
        {
            get
            {

                return VM_speed;
            }
            set
            {
                VM_speed = value;
                model.changeSpeed(VM_speed);
            }
        }


    }
}
