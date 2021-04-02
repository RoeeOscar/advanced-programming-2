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
                        NotifyPropertyChanged("VM_TotalTimeFormat");
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
            }


        }
    }
}
