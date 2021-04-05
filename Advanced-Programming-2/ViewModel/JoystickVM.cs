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
    }
}
