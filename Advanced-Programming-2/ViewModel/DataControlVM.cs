using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Advanced_Programming_2.Model;

namespace Advanced_Programming_2.ViewModel
{
    public class DataControlVM : INotifyPropertyChanged
    {
        private IFlightAnalysisModel model;
        public DataControlVM(IFlightAnalysisModel model)
        {
            this.model = model;
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                {
                    NotifyPropertyChanged("VM_"+e.PropertyName);
                }
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

private void NotifyPropertyChanged(string propertyName = "")
{
    if (PropertyChanged != null)
    {
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
}
volatile private float VM_altimeter;
        public float VM_Altimeter
        {
            get
            {
                return model.Altimeter;
            }
            set
            {
                VM_altimeter = value;
            }
        }
        volatile private float VM_airspeed;
        public float VM_Airspeed
        {
            get
            {
                return model.Airspeed;
            }
            set
            {
                VM_airspeed = value;
            }
        }
        volatile private float VM_direction;
        public float VM_Direction
        {
            get
            {
                return model.Direction;
            }
            set
            {
                VM_direction = value;
            }
        }
        volatile private float VM_pitch;
        public float VM_Pitch
        {
            get
            {
                return model.Pitch;
            }
            set
            {
                VM_pitch = value;
            }
        }
        volatile private float VM_roll;
        public float VM_Roll
        {
            get
            {
                return model.Roll;
            }
            set
            {
                VM_roll = value;
            }
        }
        volatile private float VM_yaw;
        public float VM_Yaw
        {
            get
            {
                return model.Yaw;
            }
            set
            {
                VM_yaw = value;
            }
        }


    }

}

