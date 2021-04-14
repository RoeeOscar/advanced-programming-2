using Advanced_Programming_2.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Programming_2.ViewModel
{
    public class JoystickVM : INotifyPropertyChanged
    {
        private IFlightAnalysisModel model;
        public JoystickVM(IFlightAnalysisModel model)
        {
            this.model = model;
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);

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
        #region Properties
        volatile private float VM_aileron;
        public float VM_Aileron
        {
            get
            {
                // Mult 125 for view 
                return model.Aileron * 55;
            }
            set
            {
                VM_aileron = value;
            }
        }
        volatile private float VM_elevator;
        public float VM_Elevator
        {
            get
            {
                // Mult 125 for view 
                return model.Elevator * 55;
            }
            set
            {
                VM_elevator = value;
            }
        }
        volatile private float VM_rudder;
        public float VM_Rudder
        {
            get
            {
                return model.Rudder;
            }
            set
            {
                VM_rudder = value;
            }
        }
        volatile private float VM_throttle;
        public float VM_Throttle
        {
            get
            {
                return model.Throttle;
            }
            set
            {
                VM_throttle = value;
            }
        }
        #endregion
    }
}
