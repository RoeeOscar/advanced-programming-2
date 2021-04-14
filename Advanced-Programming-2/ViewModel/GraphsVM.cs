using Advanced_Programming_2.Model;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advanced_Programming_2.ViewModel
{
    public class GraphsVM : INotifyPropertyChanged
    {
        private IFlightAnalysisModel model;
        public GraphsVM(IFlightAnalysisModel model)
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
        private List<string> VM_attributes;
        public List<string> VM_Attributes
        {
            get
            {
                return model.Attributes;
            }
            set
            {
                VM_attributes = value;
            }
        }

        private List<DataPoint> VM_graphPoints;
        public List<DataPoint> VM_GraphPoints
        {
            get
            {
                return model.GraphPoints;
            }
            set
            {
                VM_graphPoints = value;
            }
        }
        public void changeGraph (string attribute)
        {
            model.changeGraph(attribute);
        }

        private string VM_graphName;
        public string VM_GraphName
        {
            get
            {
                return model.GraphName;
            }
            set
            {
                VM_graphName = value;
            }
        }

                   private List<DataPoint> VM_correlatedGraphPoints;
        public List<DataPoint> VM_CorrelatedGraphPoints
        {
            get
            {
                return model.CorrelatedGraphPoints;
            }
            set
            {
                VM_correlatedGraphPoints = value;
            }
        }

        private string VM_correlatedGraphName;
        public string VM_CorrelatedGraphName
        {
            get
            {
                return model.CorrelatedGraphName;
            }
            set
            {
                VM_correlatedGraphName = value;
            }
        }

        private List<DataPoint> VM_regressionLine;

        public List<DataPoint> VM_RegressionLine
        {
            get
            {
                return model.RegressionLine;
            }
            set
            {
                VM_regressionLine = value;
            }
        }

        private List<DataPoint> VM_last30Points;

        public List<DataPoint> VM_Last30Points
        {
            get
            {
                return model.Last30Points;
            }
            set
            {
                VM_last30Points = value;
            }
        }

        public void loadDLL(string DLLfile)
        {
            model.loadDLL(DLLfile);
        }

        private List<DataPoint> VM_shape;
        public List<DataPoint> VM_Shape
        {
            get
            {
                return model.Shape;
            }
            set
            {
                VM_shape = value;
            }
        }

        private List<DataPoint> VM_lastAnomalies;
        public List<DataPoint> VM_LastAnomalies
        {
            get
            {
                return model.LastAnomalies;
            }
            set
            {
                VM_lastAnomalies = value;
            }
        }

        private ObservableCollection<int> VM_currentAnomalies;
        public ObservableCollection<int> VM_CurrentAnomalies
        {
            get
            {
                return model.CurrentAnomalies;
            }
            set
            {
                VM_currentAnomalies = value;
            }
        }

        public void changeCurrentTime(int newTime)
        {
            model.changeCurrentTime(newTime);
        }
    }
        
    
}
